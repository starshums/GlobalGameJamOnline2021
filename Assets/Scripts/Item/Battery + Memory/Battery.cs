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

    public Health playerhp;


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
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            playerhp.health += 5f;

            Destroy(gameObject);
 
        }
      else
        {
            Debug.Log("Not a player");
        }
    }
}
