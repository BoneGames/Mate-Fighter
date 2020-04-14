using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System.IO;
using SFB;

public interface IKillable
{
    void TakeDamage(int damage);
}

public class Character : MonoBehaviour, IKillable
{
    public int player;
    public int maxHealth = 100;
    [Header("Hit Boxes")]
    public BoxCollider2D punch;
    public BoxCollider2D block;
    [Header("Mechanics")]
    public int damage = 10;
    [Header("Audio")]
    public AudioClip punchClip;
    public AudioClip blockClip;

    public SpriteRenderer rend;

    public int health = 0;
    public Collider2D currentAttackCol;

   

    void DrawHitBox(BoxCollider2D hitbox)
    {
        Vector3 localOffset = hitbox.transform.lossyScale * hitbox.offset;
        
        Vector3 position = hitbox.transform.position + localOffset;
        Vector3 size = hitbox.transform.lossyScale * hitbox.size;
        
        Gizmos.DrawCube(position, size);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    DrawHitBox(punch);
    //    DrawHitBox(block);
    //}

    Collider2D[] GetAllHits(BoxCollider2D hitbox)
    {
        Vector3 localOffset = hitbox.transform.lossyScale * hitbox.offset;
        
        Vector3 position = hitbox.transform.position + localOffset;
        Vector3 size = hitbox.transform.lossyScale * hitbox.size;
        float angle = hitbox.transform.eulerAngles.z;

        return Physics2D.OverlapBoxAll(position, size,  angle);
    }

    /// <summary>
    /// Returns the thing that 
    /// </summary>
    /// <param name="hitbox"></param>
    /// <returns></returns>
    T GetHitCharacter<T>(BoxCollider2D hitbox)
    {
        Collider2D[] hits = GetAllHits(punch);
        foreach (var hit in hits)
        {
            T character = hit.GetComponent<T>();
            if (character != null)  // Not my character?
            {
                return character;
            }
        }
        return default(T);
    }

//    string[] OpenFileDialogue(string title, string directory, string extension, bool multiselect)
//    {
//        // Preprocessor Directives
//#if UNITY_WEBGL // Definition
//        return null; // This needs fixing
//#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
//        return StandaloneFileBrowser.OpenFilePanel(title, directory, extension, multiselect);
//#else
//        return null;
//#endif
//    }

//    Texture2D LoadTexture2D(string path)
//    {
//        Texture2D tex = null;
//        byte[] fileData;
//        if (File.Exists(path))
//        {
//            fileData = File.ReadAllBytes(path);
//            tex = new Texture2D(2, 2);
//            tex.LoadImage(fileData);
//        }
//        return tex;
//    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        string[] filePaths = OpenFileDialogue("Open File", "", "", false);
    //        if (filePaths != null)
    //        {
    //            StartCoroutine(WaitLoad(filePaths.First()));
    //        }
    //        //if (DllTest.GetOpenFileName(ofn))
    //        //{
    //        //    StartCoroutine(WaitLoad(ofn.file));//加载图片到panle
    //        //    Debug.Log("Selected file with full path: {0}" + ofn.file);
    //        //}
    //    }
        
    //    if(Input.GetKeyDown(KeyCode.X))
    //    {
    //        // Try hitting the character with puch hitbox
    //        IKillable killable = GetHitCharacter<IKillable>(punch);
    //        if (killable != null)
    //        {
    //            // Deal Damage
    //            killable.TakeDamage(damage);
    //        }
    //    }    
    //}

    //IEnumerator WaitLoad(string fileName)
    //{
    //    Texture2D tex = LoadTexture2D(fileName);
    //    yield return tex;
    //    Rect rec = new Rect(0, 0, tex.width, tex.height);
    //    punch.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f));
    //}

    public void TakeDamage(int damage)
    {
        Debug.Log("starting health: " + health);
        Game.UI.TakeDamage(player, damage);
        health -= damage;
        if(health <= 0)
        {
            Debug.Log(gameObject.name + " is dead.");
            //Destroy(gameObject);
        }
        Debug.Log(gameObject.name + " just took " + damage + " damage. They now have " + health + " health left.");
    }

}
