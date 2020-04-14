using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTest : MonoBehaviour
{
    public Image health;
    void Start()
    {
        health = GetComponent<Image>();
    }

    public void TakeDamage(int amount)
    {
        health.fillAmount -= (float)amount/100f;
        if(health.fillAmount <= 0)
        {
            health.fillAmount = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
