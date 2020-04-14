using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public StatusBar statusP1, statusP2;
    // Start is called before the first frame update

    private void Awake()
    {
        Game.UI = this;
    }
    public void TakeDamage(int player, int damage)
    {
        StatusBar _status = player == 1 ? statusP1 : statusP2;
        // update health bar
        _status.UpdateHealth((float)damage / 100f);
        // play sound

        // trigger other effect
    }

    public void UseMana(int player, int amount)
    {
        StatusBar _status = player == 1 ? statusP1 : statusP2;
        // update mana bar
        _status.UpdateMana((float)amount / 100f);
    }

    public void UseSlot(int player, int newActiveAmount)
    {
        StatusBar _status = player == 1 ? statusP1 : statusP2;
        // update active slots
        _status.UpdateSlots(newActiveAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
