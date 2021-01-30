using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header ("Player Movement Settings")]
    private Vector3 moveDirection;
    [SerializeField] float speed = 10f;
    private CharacterController controller;
    Animator animator;
    public GameObject playerModel;

    [Header("Dash move Settings")]
    float maxDashEnergy = 3f;
    float dashEnergy;
    bool canDash = false;
    bool isDashing = false;
    public ParticleSystem dashPS;

    [Header ("Bomb Settings")]
    public GameObject fireBombPrefab;
    public GameObject freezeBombPrefab;
    public GameObject sleepBombPrefab;


    public Transform bombSpawnLocation;
    [SerializeField] float bombThrowForce = 350f;

    [Header("Health settings")]
    public Health healthScript;

    // Start is called before the first frame update
    void Start () 
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator> ();

        dashEnergy = maxDashEnergy;
    }

    // Update is called once per frame
    void Update () 
    {
        if (!isDashing)
        {
            dashEnergy += Time.deltaTime;
            if (dashEnergy > maxDashEnergy)
            {
                dashEnergy = maxDashEnergy;
                canDash = true;
            }
        }
        
        Movement ();
        ThrowBomb ();
    }

    void Jump () {
        if (Input.GetButtonDown ("Jump")) {

        }
    }

    void Movement () {
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
		moveDirection = moveDirection.normalized * speed;

        //moveDirection.y += Physics.gravity.y * Time.deltaTime;
        moveDirection.y -= 980f * Time.deltaTime;
        controller.Move( moveDirection * Time.deltaTime );

        // Move player in different directions
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {
			Quaternion rotatePlayer = Quaternion.LookRotation (new Vector3 (moveDirection.x, 0f, moveDirection.z));
			playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, rotatePlayer, 0.3f);
		}

        if (Input.GetKey(KeyCode.F) && canDash==true)
        {
            Dash();
            isDashing = true;
        }
        else
        {
            if (dashPS.isPlaying)
            {
                dashPS.Stop();
            }
            isDashing = false;
        }
        // Running & Idle Animations
        animator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis ("Vertical")) + Mathf.Abs(Input.GetAxis ("Horizontal"))));
    }

    void Dash()
    {
        controller.Move(moveDirection * 7f * Time.deltaTime); 
        dashEnergy -= (Time.deltaTime * 12);
        dashPS.Play();

        if (dashEnergy <= 0.5)
        {
            canDash = false;
        }
    }
    void ThrowBomb () {

        GameObject bombToThrow = null;

        if (Input.GetMouseButtonDown(0))
        {
            bombToThrow = fireBombPrefab;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            bombToThrow = freezeBombPrefab;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            bombToThrow = sleepBombPrefab;
        }

        if (bombToThrow != null)
        {
            animator.SetTrigger("Attacking");
            GameObject bomb = Instantiate(bombToThrow, bombSpawnLocation.position, transform.rotation);
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(bombSpawnLocation.forward * bombThrowForce);
            }
        }
    }

    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Battery"))
        {
            healthScript.ChangeHealth(1);
            Destroy(hit.gameObject);
        }
        if (hit.transform.CompareTag("Enemy"))
        {
            if (isDashing)
            {
                hit.gameObject.GetComponent<Health>().ChangeHealth(-1);
            }
            else
            {
                healthScript.ChangeHealth(-1);
            }
        }
    }
}