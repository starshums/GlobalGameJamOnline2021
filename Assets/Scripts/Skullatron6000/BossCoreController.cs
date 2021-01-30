using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreController : MonoBehaviour {
    void Start() { }

    void Update() { }
    
    private void OnCollisionEnter(Collision other) {
        Debug.Log("Core hit");
        gameObject.GetComponent<Health>().ChangeHealth(-50);
    }
    
}
