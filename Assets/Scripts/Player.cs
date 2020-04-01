using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Player : Character
{   
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    
}
