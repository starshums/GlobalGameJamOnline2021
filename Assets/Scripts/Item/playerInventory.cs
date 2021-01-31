using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerInventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    public Inventory inventory;

    private void Update()
    {
        //enterScene2();
        //enterScene3();
    }

    //in order for the player to enter into "scene 2" they will need 1 memory shard
    public void enterScene2()
    {
        //checks the inventory for a Memory shard
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].item.itemName == "shards" && inventory.Container[i].stackAmount == 1)
            {
                //player uses the memory to remember --> so remove 1 stack
                inventory.Container[i].removeStack(1);

                SceneManager.LoadScene("Scene2");

            }
        }
    }

    public void enterScene3()
    {
        //checks the inventory for a Memory shard
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].item.itemName == "shards" && inventory.Container[i].stackAmount == 2)
            {
                //player uses the memory to remember --> so remove 2 stack
                inventory.Container[i].removeStack(2);

                SceneManager.LoadScene("Scene3");

            }
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
      
       var item = collision.GetComponent<ItemObject>();
       if(item)
       {
            inventory.AddItem(item.itemObj, 1);
            Destroy(collision.gameObject);
       }
        //if (item.itemObj.itemName == "shards")
        //{
        //    Debug.Log("picked up a memory shard!");
        //}
        //else if (item.itemObj.itemName == "Battery")
        //{
        //    Debug.Log("picked up a have a battery!");
        //}
     }
}
