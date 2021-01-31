using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batter_GO : MonoBehaviour
{
    public Health playerHealthScript;

    void OnCollisionEnter(Collision collision)
    {
       

            playerHealthScript.ChangeHealth(5);
            Destroy(gameObject);

        
    }
 
}
