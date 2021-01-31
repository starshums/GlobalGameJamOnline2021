using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : ScriptableObject
{
    public string itemName;
    public GameObject itemImage;

    [TextArea(5, 5)]
    public string description;
}

[System.Serializable]
public class ItemInstance
{
  
    public Item item;
    // Object-specific data.

    public ItemInstance(Item item)
    {
        this.item = item;
    }
}

