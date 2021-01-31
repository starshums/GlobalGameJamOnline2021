using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Items/Memory", fileName = "memoryName.asset")]
public class memory_ScriptableObj : Item
{
    public enum memoryType
    {
        small, big, medium
    }

    public memoryType MyMemoryType;

}
