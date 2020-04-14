using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Player : Character
{
    
    public float xMove;
    public float yMove;
    public Rigidbody2D rigid;
    public bool isGrounded;
    public Animator anim;
    public List<Collider2D> attackCols = new List<Collider2D>();

    public KeyCode left, right, jump, punchKey;

   

    private void Awake()
    {
        health = maxHealth;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void SetHitterActive(int _active)
    {
        bool __active = _active == 0 ? false : true;
        currentAttackCol.GetComponent<HitActive>().active = __active;
    }
    //void Awake()
    //{
    //    Collider2D[] attachedColliders = GetComponentsInChildren<Collider2D>();
    //    foreach (var col in attachedColliders)
    //    {
    //        HitTrigger hit = col.GetComponent<HitTrigger>();
    //        if (hit)
    //        {
    //            // Remove's component
    //            Destroy(hit);
    //        }
    //        else
    //        {
    //            // Add the hittrigger
    //            hit = col.gameObject.AddComponent<HitTrigger>();
    //            // Subscribe functions
    //            hit.onCollisionEnter2D += OnCollisionEnter2D;
    //            hit.onCollisionStay2D += OnCollisionStay2D;
    //            hit.onCollisionExit2D += OnCollisionExit2D;
    //        }
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyDown(jump) && isGrounded)
        {
            rigid.AddForce(new Vector2(0, yMove), ForceMode2D.Impulse);
        }
        if (Input.GetKey(left))
        {
            transform.Translate(new Vector3(-xMove, 0, 0));
            return;
        }
        if (Input.GetKey(right))
        {
            transform.Translate(new Vector3(xMove, 0, 0));
            return;
        }
        if(Input.GetKeyDown(punchKey))
        {
            anim.SetTrigger("Punch");
            currentAttackCol = attackCols[0];
            return;
        }
    }

   
    
}
