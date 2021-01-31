using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreController : MonoBehaviour
{
    void Start() { }

    void Update()
    {
        if (gameObject.GetComponent<Health>().health == 0)
        {
            GameObject.FindObjectOfType<SkullatronHandController>().isBossDead = true;
            Destroy(this.gameObject, 1f);
        }
    }

    // private void OnCollisionEnter(Collision other)
    // {

    // }

    internal void Hit(int damage)
    {
#if UNITY_EDITOR
        Debug.Log("Core hit");
#endif
        gameObject.GetComponent<Health>().ChangeHealth(-damage);
    }

}
