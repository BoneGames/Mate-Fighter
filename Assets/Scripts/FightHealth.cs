using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightHealth : MonoBehaviour {

	int currentHealth, maxHealth = 100;
	public GameObject healthBar;
	Image healthMeter;
	Image healthBacker;
	Color healthCol;
	float red = 0f ,green = 1f;
	public Animator anim;
	public Animator guardAnim;
	public static bool winner;
	bool won = false;
	public GameObject buttons;
	

	void Start () {
		winner = won = false;
		Animator[] guardAnims = GetComponentsInChildren<Animator>();
		foreach(Animator a in guardAnims)
		{
			if(a != anim)
			{
				guardAnim = a;
				guardAnim.SetBool("Celebrate", false);
				Debug.Log("celebrate flase");

				break;
			}
		}
		currentHealth = maxHealth;
		Image[] meters = healthBar.GetComponentsInChildren<Image>();
		healthMeter = meters[1];
		healthBacker = meters[0];
		healthBacker.color = Color.green;
	}

	void Update()
	{
		if(winner && !won)
		{
			Debug.Log(this.name + " won");
			guardAnim.SetBool("Celebrate", true);
			won = true;
			buttons.SetActive(true);
		}
	}

	public void TakeDamage(int _damage)
	{
		currentHealth -= _damage;
		healthMeter.fillAmount = currentHealth/100f;

		float healthLost = maxHealth - currentHealth;

		healthBacker.color = Color.Lerp(Color.green, Color.red, healthLost/(float)maxHealth);

		if(currentHealth <= 0)
		{
			
			anim.SetTrigger("Die");
			won = true;
			winner = true;
		}
	}
}
