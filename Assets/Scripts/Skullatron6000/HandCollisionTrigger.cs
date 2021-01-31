using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisionTrigger : MonoBehaviour {
    void Start() { }

    void Update() { }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Health playerHp = other.gameObject.GetComponent<Health>(); 
            if (playerHp) playerHp.ChangeHealth(-50);
            
            FindObjectOfType<SkullatronHandController>().CollisionDetected(this);
        }
    }
}
