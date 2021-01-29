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

    [Header ("Bomb Settings")]
    public GameObject fireBombPrefab;
    public GameObject freezeBombPrefab;

    public Transform bombSpawnLocation;
    [SerializeField] float bombThrowForce = 350f;

    // Start is called before the first frame update
    void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator> ();
    }

    // Update is called once per frame
    void Update () {
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
        
		moveDirection.y += Physics.gravity.y * Time.deltaTime;
		controller.Move( moveDirection * Time.deltaTime );

        // Move player in different directions
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {
			Quaternion rotatePlayer = Quaternion.LookRotation (new Vector3 (moveDirection.x, 0f, moveDirection.z));
			playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, rotatePlayer, 0.3f);
		}

        // Running & Idle Animations
        animator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis ("Vertical")) + Mathf.Abs(Input.GetAxis ("Horizontal"))));
    }

    void ThrowBomb () {

        GameObject bombToThrow = null;

        if (Input.GetButtonDown ("Bomb")) 
        {
            bombToThrow = fireBombPrefab;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            bombToThrow = freezeBombPrefab;
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
}