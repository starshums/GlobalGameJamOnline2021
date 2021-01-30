using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class EnemyBombman : MonoBehaviour
{
    Rigidbody rb;
    PlayerController player;
    Animator animController;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject targetDecal;

    public float ReactDistance = 10f;
    public float JumpForce = 100f;
    public bool canExplode = false;
    public bool targetLocked = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animController = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        CheckPlayerDistance();
    }


    void CheckPlayerDistance()
    {
        if (Vector3.Magnitude(player.gameObject.transform.position - transform.position) < ReactDistance)
        {
            animController.SetBool("Attack", true);
            rb.AddForce(Vector3.up.normalized * JumpForce, ForceMode.Force);
            StartCoroutine(BombDive());
        }
    }

    IEnumerator BombDive()
    {
        yield return new WaitForSeconds(2f);
        rb.isKinematic = true;
        yield return new WaitForSeconds(1f);
        rb.isKinematic = false;

        Vector3 direction = player.gameObject.transform.position - transform.position;
        LockTarget(player.gameObject.transform.position);

        yield return new WaitForSeconds(0.5f);
        rb.AddForce(direction.normalized * 150f, ForceMode.Impulse);
        canExplode = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canExplode)
        {
            Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), explosionEffect.transform.rotation);

            CameraShaker.instance.ShakeCamera(1, 3);
            Destroy(this.gameObject);
        }
    }

    private void LockTarget(Vector3 position)
    {
        if (targetLocked) Instantiate(targetDecal, new Vector3(position.x, position.y, position.z), targetDecal.gameObject.transform.rotation);
        targetDecal.GetComponent<ObjectDestroyer>().DestroyTimer = 2f;
        
        targetLocked = false;
    }
}