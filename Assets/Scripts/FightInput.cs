using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FightInput : MonoBehaviour {

	public GameObject[] parts = new GameObject[2];//1 rightPunch, 2 leftPunch, 3 bodyGuard, 4 bodyPunching;
	public GameObject guardBody;
	public GameObject block;
	public Sprite guarding, crouchJump;
	public Sprite[] punching = new Sprite[2];
	SpriteRenderer bodyRend;
	Rigidbody2D rigid;
	public float jumpForce, jumpPause;
	public float moveSpeed;
	public bool grounded;
	bool punchSwitch;
	HitTrigger hitTrig;
	public Animator guardAnim;
	//public string filePath;
	//public GameObject frame;
	AI aI;


	void Awake()
	{
		aI = FindObjectOfType<AI>();
		bodyRend = GetComponentInChildren<SpriteRenderer>();
		rigid = GetComponent<Rigidbody2D>();

		// if(File.Exists(filePath))
		// {
		// 	Debug.Log("fileExists");
		// }
		//frame.GetComponent<Renderer>().material.mainTexture = LoadPNG(filePath);
	}	

	// public static Texture2D LoadPNG(string filePath) {
    //  	Texture2D tex = null;
    //  	byte[] fileData;
    //  	if (File.Exists(filePath))     
	// 	{
    //     	fileData = File.ReadAllBytes(filePath);
    //     	tex = new Texture2D(2, 2);
    //     	tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
    //  	}
	// 	// tex.format
    //  	return tex;
 	// }

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform.tag == "Floor")
		grounded = true;
	}
	void Update () {
		Reset();
		if(FightHealth.winner)
		{
			guardAnim.enabled = true;
			return;
		}
		
		Move();
		Attacks();
	}

	void Block()
	{
		bodyRend.enabled = false;
		block.SetActive(true);
	}

	void Punch()
	{
		guardAnim.enabled = false;
		// Convert Bool to int via ternary op (cant cast directly in C#) to cycle between arm punches
		int punchInt = punchSwitch ? 1 : 0;
		// Apply relevant Sprites GameObjects, Triggers...  (THIS CAN BE SIMPLIFIED...)
		bodyRend.sprite = punching[punchInt];
		parts[punchInt].SetActive(true);
		hitTrig = parts[punchInt].GetComponent<HitTrigger>();
	}

	void Crouch()
	{
		foreach(GameObject part in parts)
		{
			part.SetActive(false);
		}
		guardBody.SetActive(false);
		parts[2].SetActive(true);
	}

	void Attacks()
	{
		if(grounded)
		{
			// cross arm block
			if(Input.GetKey(KeyCode.B))
			{
				Block();
			}
			// Punch
			else if(Input.GetKey(KeyCode.X))
			{
				Punch();
			}
		
			else if(Input.GetKeyUp(KeyCode.X))
			{
				// condition for allowing hitTrigger on punches to collide
				//hitTrig.pow = false;
				// Change punching arm
				punchSwitch = !punchSwitch;
				guardAnim.enabled = true;
			}
			// Crouch
			else if(Input.GetKey(KeyCode.DownArrow))
			{
				Crouch();
			}
		}
	}

	void Move()
	{
		// MOVE: left/right
		transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed,0,0);

		// JUMP
		if(Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			StartCoroutine("Jump");
		}
	}

	IEnumerator Jump()
	{
		rigid.AddForce(new Vector2(0,jumpForce));
		grounded = false;

		yield return new WaitForSeconds(jumpPause);
		guardAnim.enabled = false;
		while(!grounded)
		{
			bodyRend.sprite = crouchJump;
			yield return null;
		}
		guardAnim.enabled = true;
	}

	void Reset()
	{
		foreach(GameObject part in parts)
		{
			part.SetActive(false);
		}
		guardBody.SetActive(true);
		bodyRend.enabled = true;
		block.SetActive(false);
		bodyRend.sprite = guarding;
	}
}
