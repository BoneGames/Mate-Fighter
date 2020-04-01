using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam : MonoBehaviour
{
  public WebCamTexture webcamTexture;
  public Material targetMat;

  // Start is called before the first frame update
  void Start()
  {

    WebCamDevice[] devices = WebCamTexture.devices;

    print(devices.Length);

    for (int i = 0; i < devices.Length; i++)
    {

      if (i == 0)
      {
        print(devices[i].name + " at index " + i);

        webcamTexture = new WebCamTexture(devices[i].name);
      }
    }

    print(webcamTexture.deviceName);

    //GetComponent<Renderer>().material.mainTexture = webcamTexture;

    targetMat.SetTexture("_WebCam", webcamTexture);

    webcamTexture.Play();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
