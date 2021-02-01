using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    public float health;

    [Tooltip("Game Object to be destroyed when the health bar goes to zero")]
    public GameObject GameObjectToDestroy;

    GameObject gameOverScreen;

    public GameObject[] itemsToDrop;
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
        gameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen");
        
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
                Invoke("GameOver", 0.5f);
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
                dropItem();
                Destroy(gameObject);
                break;
            case "Core":
                Debug.Log("Killed " + transform.tag);
                gameObject.GetComponent<BossCoreController>().isDead = true;
                break;
            default:
                Debug.Log(transform.name + " has no tag attached");
                break;
        }
    }
    void dropItem()
    {
        int rand = Random.Range(0, itemsToDrop.Length);
        if (itemsToDrop.Length > 0)
        {
            GameObject drop = Instantiate(itemsToDrop[rand], transform.position, Quaternion.identity);
            Bomb bombScript = drop.GetComponent<Bomb>();
            if (bombScript != null)
            {
                bombScript.isBombActive = false;
            }
        }
    }
    void GameOver()
    {
        gameOverScreen.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}