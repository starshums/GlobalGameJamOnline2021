using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisionTrigger : MonoBehaviour {
    void Start() { }

    void Update() { }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.gameObject.GetComponent<Health>().ChangeHealth(-50);
            FindObjectOfType<SkullatronHandController>().CollisionDetected(this);
        }
    }
}
