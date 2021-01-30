using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterStats_UI : MonoBehaviour
{
    public Image content;

    public Health playerHealthScript;

    [SerializeField]
    public float lerpSpeed;

    //the number of fill in decimals
    public float currentFill;

    public float HP;
    public float maxHP;


    void Start()
    {
        content = GetComponent<Image>();
        playerHealthScript.health = HP;
        playerHealthScript.maxHealth = maxHP;
    }

    void healthCheck()
    {
        //this is to check the HP so it doesn't go too high or too low 
        if (HP > maxHP)
        {
            HP = maxHP;
        }
        else if (HP < 0)
        {
            HP = 0;
        }
        else
        {
            HP = HP;
        }
    }


    // Update is called once per frame
    void Update()
    {
        healthCheck();

        currentFill = playerHealthScript.health / playerHealthScript.maxHealth;

        //HP UI increase and decreasing
        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }


}
