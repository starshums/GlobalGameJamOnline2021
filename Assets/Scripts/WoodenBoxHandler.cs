using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WoodenBoxHandler : MonoBehaviour
{
    [SerializeField] GameObject[] pieces;
    [SerializeField] GameObject[] collectables;

    ///<summary>
    /// Method to break the box and split the pieces
    ///</summary>
    public void Break()
    {
        foreach(GameObject piece in pieces)
        {
            //Remove the pieces from box and make it fall
            piece.transform.parent = null;
            piece.AddComponent<Rigidbody>();
            piece.GetComponent<Rigidbody>().mass = 50f;
            piece.GetComponent<Rigidbody>().isKinematic = false;
     
            //Add teh destroyer in each piece
            piece.AddComponent<ObjectDestroyer>().DestroyTimer = 2f;

        }

        //Instantiate a random collectable
        RandomCollectable();
        
        //Destroy the box
        Destroy(this.gameObject);
    }

    ///<summary>
    /// Method to generate a random collectable to be spawned
    ///</summary>
    void RandomCollectable()
    {
        int rand = Random.Range(0,collectables.Length);
        if (collectables.Length > 0)
        {
            GameObject drop = Instantiate(collectables[rand], transform.position, Quaternion.identity);
            Bomb bombScript = drop.GetComponent<Bomb>();
            if(bombScript != null)
            {
                bombScript.isBombActive = false;
            }
        }
    }
}
