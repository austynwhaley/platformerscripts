using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour

{
    public static HealthManager instance;

    public int currentHealth, maxHealth;
    public float invincibleLength = 2f;
    private float invinceCounter;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(invinceCounter > 0)
        {
            invinceCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController1.instance.playerPieces.Length; i++)
            {
                if(Mathf.Floor(invinceCounter * 5f) % 2==0)
                {
                    PlayerController1.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController1.instance.playerPieces[i].SetActive(false);
                }

                if(invinceCounter <=0)
                {
                    PlayerController1.instance.playerPieces[i].SetActive(true);
                }
 
            }
        }
    }

    public void Hurt()
    {
        if(invinceCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController1.instance.KnockBack();
                invinceCounter = invincibleLength;

                
            }
        }
        
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}

