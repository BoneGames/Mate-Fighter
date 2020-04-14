using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCam : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    public void SetCamSize(float cameraWidth, float cameraHeight)
    {
        // Camera has fixed width and height on every screen solution
        float x = (100f - 100f / (Screen.width / cameraWidth)) / 100f;
        float y = (100f - 100f / (Screen.height / cameraHeight)) / 100f;
        GetComponent<Camera>().rect = new Rect(x, y, 1, 1);
    }

    public Texture2D SnapShot()
    {
        // Initialize and render
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        cam.targetTexture = rt;
        cam.Render();
        RenderTexture.active = rt;

        // Create Texture
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // Read pixels
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);

        // Clean up
        //cam.targetTexture = null;
        //RenderTexture.active = null; // added to avoid errors 
        //DestroyImmediate(rt);
        tex.Apply();
        return tex;

    }
}
