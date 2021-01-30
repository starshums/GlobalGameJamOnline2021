using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    void Start() { }

    void Update() { }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Boss Fight Starts");
        GameObject.FindObjectOfType<SkullatronHandController>().isBossFightStarted = true;
    }
}
