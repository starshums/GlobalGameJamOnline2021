using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class EnemyBombman : MonoBehaviour
{
    Rigidbody rb;
    PlayerController player;
    Animator animController;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject targetDecal;

    public float ReactDistance = 25f;
    public float JumpForce = 10f;
    public bool canExplode = false;
    public bool targetLocked = true;

    public float damageRadius = 7f;
    public float explosionForce = 500f;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip explosionSound;

    bool playOnce = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animController = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        playOnce = true;
    }

    void Update()
    {
        CheckPlayerDistance();
    }


    void CheckPlayerDistance()
    {
        if (Vector3.Magnitude(player.gameObject.transform.position - transform.position) < ReactDistance && !canExplode)
        {
            animController.SetBool("Attack", true);

            GetComponent<NavMeshAgent>().enabled = false;
            rb.AddForce(Vector3.up.normalized * JumpForce, ForceMode.VelocityChange);
            StartCoroutine(BombDive());
        }
    }

    IEnumerator BombDive()
    {
        AudioSource audioS = GetComponent<AudioSource>();
        if (audioS && playOnce) audioS.PlayOneShot(jumpSound);
        playOnce = false;
        yield return new WaitForSeconds(2f);
        rb.isKinematic = true;
        yield return new WaitForSeconds(1f);
        rb.isKinematic = false;

        Vector3 direction = player.gameObject.transform.position - transform.position;
        LockTarget(player.gameObject.transform.position);
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(direction.normalized * 250f, ForceMode.Impulse);
        canExplode = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canExplode)
        {
            Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), explosionEffect.transform.rotation);

            //apply damage to nearby players
            DamageAfterExplosion();

            CameraShaker.instance.ShakeCamera(1, 3);
            AudioSource audioS = GetComponent<AudioSource>();
            if (audioS) audioS.PlayOneShot(explosionSound);
            Destroy(this.gameObject);
        }
    }

    private void LockTarget(Vector3 position)
    {
        if (targetLocked) Instantiate(targetDecal, new Vector3(position.x, position.y, position.z), targetDecal.gameObject.transform.rotation);
        targetDecal.GetComponent<ObjectDestroyer>().DestroyTimer = 2f;

        targetLocked = false;
    }

    void DamageAfterExplosion()
    {
        bool isPlayerHit = false;
        Collider[] nearbyObjectsColliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider nearbyObject in nearbyObjectsColliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {

                //add force
                rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
                //damage
                Health healthScript = nearbyObject.GetComponent<Health>();
                if (healthScript != null)
                {
                    if (nearbyObject.CompareTag("Player") && isPlayerHit == false)
                    {
                        isPlayerHit = true;
                        healthScript.ChangeHealth(-2);
                    }
                    else
                    {
                        healthScript.ChangeHealth(-2);

                    }
                }

                //TO BREAK THE WOODENBOXES
                if (nearbyObject.CompareTag("Woodenbox"))
                {
                    WoodenBoxHandler box = nearbyObject.GetComponent<WoodenBoxHandler>();
                    if (box) box.Break();
                }
            }
        }
    }
}