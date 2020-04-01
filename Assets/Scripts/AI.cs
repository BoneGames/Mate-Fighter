using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Character
{
    private void Update()
    {
        
    }

    //void Awake()
    //{
    //	bodyRend = GetComponentInChildren<SpriteRenderer>();
    //	rigid = GetComponent<Rigidbody2D>();
    //	StartCoroutine(MoveCycler());
    //}	
    //int SetOdds()
    //{
    //	int functionIndex = 0;
    //	int seed = Random.Range(0,10);

    //	if(seed < 5)
    //	{
    //		functionIndex = 0;
    //	}
    //	else if (seed >= 5 && seed < 8)
    //	{
    //		functionIndex = 1;
    //	} 
    //	else if (seed >= 8 && seed < 9)
    //	{
    //		functionIndex = 2;
    //	}
    //	else
    //	{
    //		functionIndex = 3;
    //	}
    //	return functionIndex;
    //}

    //IEnumerator MoveCycler()
    //{
    //	while(true)
    //	{
    //		if(!moving && grounded && !FightHealth.winner)
    //		{
    //			moveDelay = Random.Range(0.25f, 1f);
    //			yield return new WaitForSeconds(moveDelay);
    //			StartCoroutine(IENames[SetOdds()]);
    //		}
    //		yield return null;
    //	}
    //	//return null;
    //}



    //IEnumerator PunchIE()
    //{
    //	Debug.Log("Punch");
    //	float startTime = Time.time;
    //	while(Time.time - startTime < 0.25f)
    //	{
    //		Punch();
    //		yield return null;
    //	}
    //	hitTrig.pow = false;
    //}

    //public IEnumerator BlockIE()
    //{
    //	Debug.Log("Block");
    //	float startTime = Time.time;
    //	while(Time.time - startTime < 0.5f)
    //	{
    //		Block();
    //		yield return null;
    //	}
    //}

    //void Block()
    //{
    //	bodyRend.enabled = false;
    //	block.SetActive(true);
    //}


    //void OnCollisionEnter2D(Collision2D other)
    //{
    //	if(other.transform.name == "Floor")
    //	grounded = true;
    //}

    //float RaycastDistance()
    //{
    //	RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.left, 20f);
    //	foreach(RaycastHit2D hit in hits)
    //	{
    //		if(hit.collider.name == "Spruce1")
    //		{
    //			distanceFromPlayer = Vector2.Distance(transform.position, hit.transform.position);
    //			break;
    //		}
    //	}
    //	return distanceFromPlayer;
    //}

    //void FixedUpdate()
    //{
    //	if(!moving && (RaycastDistance() > maxDistance || RaycastDistance() < minDistance))
    //	{	
    //		StartCoroutine(MoveToPLayer());
    //	}
    //}

    //IEnumerator MoveToPLayer()
    //{
    //	moving = true;
    //	yield return new WaitForSeconds(moveDelay);
    //	float xMove = 0;
    //	// To Move Left (closer to opponent)
    //	while(RaycastDistance() > maxDistance)
    //	{
    //		xMove += Time.deltaTime;
    //		float translation = Mathf.Clamp01(xMove) * Time.deltaTime * moveSpeed;
    //		transform.Translate(-translation,0,0);
    //		yield return new WaitForEndOfFrame();
    //	}
    //	xMove = 0;
    //	while(RaycastDistance() < minDistance)
    //	{
    //		xMove += Time.deltaTime;
    //		float translation = Mathf.Clamp01(xMove) * Time.deltaTime * moveSpeed;
    //		transform.Translate(translation,0,0);
    //		yield return new WaitForEndOfFrame();
    //	}
    //	moving = false;
    //	yield return null;
    //}
    //void Update () {
    //	Reset();
    //}

    //void Punch()
    //{
    //	// Convert Bool to int via ternary op (cant cast directly in C#) to cycle between arm punches
    //	int punchInt = punchSwitch ? 1 : 0;
    //	// Apply relevant Sprites GameObjects, Triggers...  (THIS CAN BE SIMPLIFIED...)
    //	bodyRend.sprite = punches[punchInt];
    //	parts[punchInt].SetActive(true);
    //	hitTrig = parts[punchInt].GetComponent<hitTrigger>();
    //}

    //IEnumerator CrouchIE()
    //{
    //	Debug.Log("Crouch");
    //	float startTime = Time.time;
    //	while(Time.time - startTime < 0.5f)
    //	{
    //		Crouch();
    //		yield return null;
    //	}
    //}
    //void Crouch()
    //{
    //	foreach(GameObject part in parts)
    //	{
    //		part.SetActive(false);
    //	}
    //	guardBody.SetActive(false);
    //	parts[2].SetActive(true);
    //}
    //IEnumerator JumpIE()
    //{
    //	Debug.Log("Jump");
    //	rigid.AddForce(new Vector2(0,jumpForce));
    //	grounded = false;

    //	yield return new WaitForSeconds(jumpPause);
    //	guardAnim.enabled = false;
    //	while(!grounded)
    //	{
    //		bodyRend.sprite = crouchJump;
    //		yield return null;
    //	}
    //	guardAnim.enabled = true;
    //}

    //void Reset()
    //{
    //	foreach(GameObject part in parts)
    //	{
    //		part.SetActive(false);
    //	}
    //	guardBody.SetActive(true);
    //	bodyRend.enabled = true;
    //	block.SetActive(false);
    //	bodyRend.sprite = guarding;
    //}
}
