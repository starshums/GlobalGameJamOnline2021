using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    public Inventory inventory;

    public void OnTriggerEnter(Collider collision)
    {
      
       var item = collision.GetComponent<ItemObject>();
       if(item)
       {
            inventory.AddItem(item.itemObj, 1);
            Destroy(collision.gameObject);
       }
           
       
     }
}
