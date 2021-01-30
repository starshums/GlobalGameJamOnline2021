using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayItem : MonoBehaviour
{
    public Inventory inventory;
    
    //this will help align the UI
    public int x_start;
    public int y_start;

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
        //updateDisplayInv();
    }

    public void createDisplayInv()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.itemPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].stackAmount.ToString("n0");
        }
    }
    public Vector3 GetPosition(int i)
    {
        
        return new Vector3(x_start + (X_space_inbetween * (i % Column)), y_start + (-Y_space_inbetween * (i/Column)), 0f);
    }
}
