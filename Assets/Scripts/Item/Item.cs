using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;

    [TextArea(10, 10)]
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

