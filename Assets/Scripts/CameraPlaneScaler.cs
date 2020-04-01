using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlaneScaler : MonoBehaviour
{
    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height; // basically height * screen aspect ratio
        transform.localScale = new Vector3(width, 1, height)/10;
    }
}
