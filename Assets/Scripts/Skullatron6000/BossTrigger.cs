using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    void Start() { }

    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
#if UNITY_EDITOR
        Debug.Log("Boss Fight Starts");
#endif

        GameObject.FindObjectOfType<SkullatronHandController>().isBossFightStarted = true;
    }
}
