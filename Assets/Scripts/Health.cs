using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    public float health;
    private void Start()
    {
        health = maxHealth;
    }
    public void ChangeHealth(int changeInHealth)
    {
        health += changeInHealth;
        if (health > 10)
        {
            health = 10;
        }
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            health = 0;
            Debug.Log(transform.name + " is destroyed");
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
            default:
                Debug.Log(transform.name + " has no tag attached");
                break;
        }
    }
}