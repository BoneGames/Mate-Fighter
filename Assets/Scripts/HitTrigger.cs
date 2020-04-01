using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    public delegate void CollisionEvent(Collision col);
    public CollisionEvent onCollisionEnter;
    public CollisionEvent onCollisionStay;
    public CollisionEvent onCollisionExit;

    public delegate void CollisionEvent2D(Collision2D col);
    public CollisionEvent2D onCollisionEnter2D;
    public CollisionEvent2D onCollisionStay2D;
    public CollisionEvent2D onCollisionExit2D;

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter.Invoke(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        onCollisionStay.Invoke(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        onCollisionExit.Invoke(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onCollisionEnter2D.Invoke(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        onCollisionStay2D.Invoke(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onCollisionExit2D.Invoke(collision);
    }

    //public Transform hitPoint;
    //public AudioSource audioSource;
    //public Collider2D thisCol;
    //public bool pow;
    //public AudioClip hit, block;
    //public int damage;

    //   void Start ()
    //   {

    //}

    //   void OnCollisionEnter2D(Collision2D col)
    //   {

    //   }

    //   // Every Frame
    //   void OnCollisionStay2D(Collision2D col)
    //{
    //	Debug.Log("Collider");
    //	if(col.collider.GetType() == typeof(BoxCollider2D) && !pow)
    //	{
    //		StartCoroutine(Pow(block));
    //		pow = true;
    //		return;
    //	}
    //	if(col.collider.GetType() == typeof(CapsuleCollider2D) && !pow)
    //	{
    //		StartCoroutine(Pow(hit));
    //		col.transform.GetComponent<FightHealth>().TakeDamage(damage);
    //		pow = true;
    //	}
    //}

    //IEnumerator Pow(AudioClip sound)
    //{
    //	audioSource.clip = sound;
    //	audioSource.Play();
    //	yield return null;
    //}
}
