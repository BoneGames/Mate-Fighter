using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image health, mana;
    public Image[] slots;
    public void UpdateHealth(float _damage)
    {
        health.fillAmount -= _damage;
    }

    public void UpdateMana(float _mana)
    {
        mana.fillAmount -= _mana;
    }

    public void UpdateSlots(int fullSlots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < fullSlots)
            {
                slots[i].enabled = true;
            }
            else
            {
                slots[i].enabled = false;
            }
        }
    }
}
