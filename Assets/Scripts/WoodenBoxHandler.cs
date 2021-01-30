using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WoodenBoxHandler : MonoBehaviour
{
    [SerializeField] GameObject[] pieces;

    public void Break()
    {
        foreach(GameObject piece in pieces)
        {
            piece.transform.parent = null;
            piece.AddComponent<Rigidbody>();
            piece.GetComponent<Rigidbody>().mass = 50f;
            piece.GetComponent<Rigidbody>().isKinematic = false;
        }
        Destroy(this.gameObject);
    }

    //JUST A COMMENT FOR GITHUB / DISCORD INTEGRATION TEST
}
