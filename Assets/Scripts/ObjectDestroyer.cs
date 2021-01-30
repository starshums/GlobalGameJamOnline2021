using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float DestroyTimer = 2f;

    private void Start() {
        Destroy(this.gameObject, DestroyTimer);
    }
}
