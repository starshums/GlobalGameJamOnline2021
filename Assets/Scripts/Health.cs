using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    public float health;

    [Tooltip("Game Object to be destroyed when the health bar goes to zero")]
    public GameObject GameObjectToDestroy;
    
    private void Awake()
    {
        health = maxHealth;
    }

    private void Start()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void ChangeHealth(int changeInHealth)
    {
        health += changeInHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

#if UNITY_EDITOR
        Debug.Log("Health: " + health);
#endif

        if (health <= 0)
        {
            health = 0;
#if UNITY_EDITOR
            Debug.Log(transform.name + " is destroyed");
#endif
            Death();
        }
    }

    void Death()
    {
        switch (transform.tag)
        {
            case "Player":
                Debug.Log("Game Over");
                break;
            case "Enemy":
                Debug.Log("Killed enemy");
                Destroy(gameObject);
                break;
            case "BombmanEnemy":
                Debug.Log("Killed " + transform.tag);
                Destroy(GameObjectToDestroy);
                break;
            case "SkullMinionEnemy":
                Debug.Log("Killed " + transform.tag);
                Destroy(gameObject);
                break;
            default:
                Debug.Log(transform.name + " has no tag attached");
                break;
        }
    }
}