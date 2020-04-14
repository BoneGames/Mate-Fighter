using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class ChangeBG : MonoBehaviour
{
    public Texture[] backgrounds;
    public Material backGroundMat;
    public KeyCode bgForward;
    public KeyCode bgBackward;
    public int textureIndex = 0;
    private void Start()
    {
        backGroundMat.mainTexture = backgrounds[textureIndex];
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(bgForward))
        {
            textureIndex++;
            if(textureIndex >= backgrounds.Length)
            {
                textureIndex = 0;
            }
            backGroundMat.mainTexture = backgrounds[textureIndex];
        }
        if (Input.GetKeyDown(bgBackward))
        {
            textureIndex--;
            if (textureIndex < 0)
            {
                textureIndex = backgrounds.Length -1;
            }
            backGroundMat.mainTexture = backgrounds[textureIndex];
        }
    }

    [Button]
    public void SetBG()
    {
        backGroundMat.mainTexture = backgrounds[textureIndex];
    }
}
