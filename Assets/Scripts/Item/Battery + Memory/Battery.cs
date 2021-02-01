using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private Vector3 currPosition;
    private Vector3 spawnlocation;

    [SerializeField] GameObject assetModel;

    [SerializeField] GameObject player;

    [Header("Sin Battery Movement")]
    [SerializeField] float speed = 5f;

    [SerializeField]float magnitude = 1f;

    [SerializeField]float offset = 0f;

    [Header("Collision of Battery")]
    [SerializeField] Collider battery_collider;

    [SerializeField]GameObject b_particle;

    public int recoverHP = 10;


    void Start()
    {
        currPosition = transform.position;
        battery_collider = GetComponent<Collider>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

 
    void Update()
    {
        //using sine , make the battery move up and down 
        assetModel.transform.position = currPosition + transform.up * Mathf.Sin(Time.time * speed + offset) * magnitude;

        //Find the player to spawn on it
         spawnlocation = player.transform.position;
    }
    void pickUp()
    {
        // animation 
        //Instantiate(b_particle, spawnlocation, Quaternion.identity);

        // more time? 

        // gives player health for now in the player control script!
    }
    void OnTriggerEnter(Collider other) {
    {
        if (other.tag == "Player")
        {
            Health hp = other.GetComponent<Health>();
            if (hp) hp.ChangeHealth(50);

            // playerhp.health += 50f;

            Destroy(gameObject);
 
        }
      else
        {
            Debug.Log("Not a player");
        }
    }
    }
}
