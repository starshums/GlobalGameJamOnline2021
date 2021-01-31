using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterStats_UI : MonoBehaviour
{
    public Image content;

    public Health healthscript;

    [SerializeField]
    public float lerpSpeed;

    //the number of fill in decimals
    public float currentFill;

    public float HP;
    public float maxHP;


    void Start()
    {
        content = GetComponent<Image>();
        // healthscript = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        if (healthscript)
        {
            HP = healthscript.health;
            maxHP = healthscript.maxHealth;
        }
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
            HP = healthscript.health;
        }


        currentFill = healthscript.health / healthscript.maxHealth;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.transform.CompareTag("Battery"))
        {
            {
                HP += 5;
            }

        }
    }

    void Update()
    {
        healthCheck();

        //HP UI increase and decreasing
        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }


}
