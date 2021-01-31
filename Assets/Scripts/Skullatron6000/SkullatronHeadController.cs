using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullatronHeadController : MonoBehaviour {

    public Transform skullatron6000HeadModel = null;
    public Transform player;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start() {
         player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {
        skullatron6000HeadModel.LookAt(player);

        if (isDead) Destroy(this.gameObject);
    }
}
