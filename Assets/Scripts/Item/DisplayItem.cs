using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayItem : MonoBehaviour
{
    public Inventory inventory;


    //this will help align the UI
    public int x_offset;
    public int y_offset;


    //inbetween the icons

    public int X_space_inbetween;
    public int Y_space_inbetween;
    public int Column;
    Dictionary<InventorySlot, GameObject> displayItems = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        createDisplayInv();
    }

    // Update is called once per frame
    void Update()
    {
        updateDisplayInv();
       
    }

    public void updateDisplayInv()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            //checks if this item is already in the inventory
            if (displayItems.ContainsKey(inventory.Container[i]))
            {
                // we already have the item = just increase the stack amount
                displayItems[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].stackAmount.ToString("n0");
            }
            else
            {
                //if we don't have the item yet --> create the item
                var obj = Instantiate(inventory.Container[i].item.itemImage, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].stackAmount.ToString("n0");
                displayItems.Add(inventory.Container[i], obj);

            }
        }
    }
    public void createDisplayInv()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            //create the inventory inside the UI
            var obj = Instantiate(inventory.Container[i].item.itemImage, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].stackAmount.ToString("n0");
            displayItems.Add(inventory.Container[i], obj);
        }
    }
    public Vector3 GetPosition(int i)
    {
        
        return new Vector3(x_offset + (X_space_inbetween * (i % Column)), y_offset + (-Y_space_inbetween * (i/Column)), 0f);
    }
}
