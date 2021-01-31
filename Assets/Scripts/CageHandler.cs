using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageHandler : MonoBehaviour
{
    bool canBeOpen = false; 

    public void CanBeOpen()
    {
        canBeOpen = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (canBeOpen && other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
