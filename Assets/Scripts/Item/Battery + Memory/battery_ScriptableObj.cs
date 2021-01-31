using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Items/Battery", fileName = "batteryName.asset")]
public class battery_ScriptableObj : Item
{
    public enum powerupType
    {
        moreTime, moreHP, death
    }

    public powerupType MyPowerUpType;
}

