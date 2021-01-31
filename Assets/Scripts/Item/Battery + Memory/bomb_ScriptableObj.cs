using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Items/Bomb", fileName = "bombType.asset")]
public class bomb_ScriptableObj : Item
{
    public enum bombType
    {
        Freeze, Fire, Sleep
    }

    public bombType MyBombType;
}

