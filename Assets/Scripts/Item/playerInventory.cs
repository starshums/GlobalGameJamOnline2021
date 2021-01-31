using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerInventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    public Inventory inventory;

    public bool hasBomb = false;

    private void Start()
    {
        hasBomb = false;
    }
    private void Update()
    {
        enterScene2();
        enterScene3();
        noBombs();

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

    public void noBombs()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].item.itemName == "fireBomb" || inventory.Container[i].item.itemName == "freezeBomb" || inventory.Container[i].item.itemName == "sleepBomb" && inventory.Container[i].stackAmount <= 0)
            {
                //player has no bomb
                hasBomb = false;

                Debug.Log("No bombs");
            }
            else
            {
                hasBomb = true;
            }
        }
    }

    public void useFireBomb()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].item.itemName == "fireBomb")
            {
                //player remove firebomb
                inventory.Container[i].removeStack(1);

            }
        }
    }

    public void useFreezeBomb()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].item.itemName == "freezeBomb")
            {
                //player remove firebomb
                inventory.Container[i].removeStack(1);

            }
        }
    }

    public void useSleepBomb()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].item.itemName == "sleepBomb")
            {
                //player remove firebomb
                inventory.Container[i].removeStack(1);

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
