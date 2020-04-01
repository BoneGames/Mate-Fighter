using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MF
{
    public class Adder : MonoBehaviour
    {
        public RectTransform rect;
        public RectTransform canvasRect;

        public PhoneCamera pc;

        // Start is called before the first frame update
        void Start()
        {
            rect = GetComponent<RectTransform>();
            canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Vector2 startingSize = pc.maskImageCANVAS.sprite.rect.size;
                //pc.maskImage.SetNativeSize();
                int counter = 0;
                while (rect.sizeDelta.x < canvasRect.sizeDelta.x && rect.sizeDelta.y < canvasRect.sizeDelta.y && counter < 200)
                {
                    rect.sizeDelta += new Vector2(1, 1);
                    counter++;
                }
                Debug.Log(counter);
                //Vector2 endSize = pc.maskImageCANVAS.sprite.rect.size;
                //float scalefactor = endSize.magnitude / startingSize.magnitude;
                //pc.
            }
        }
    } 
}
