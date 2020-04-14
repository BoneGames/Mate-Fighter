using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitActive : MonoBehaviour
{
    public bool active;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!active)
            return;
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Player>().TakeDamage(5);
        }
    }

    public void SetHitterActive(bool _active)
    {
        active = _active;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
