using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace MF
{
    [System.Serializable]
    public struct SpritePos
    {
        public Sprite single;
        public Sprite multiple;
    }


    public class PhoneCamera : MonoBehaviour
    {
        private bool camAvailable = false;
        public WebCamTexture frontCam;
        public RawImage test;
        //private Texture defaultBackground;
        public GameObject plane;
        //public RawImage backgroundCANVAS;
        //public Renderer webCamRend, previewRend;
        //public AspectRatioFitter fitCANVAS;
        public AspectRatioFitter fitPLANE;
        //public Image maskImageCANVAS;
        int scaleToPixel = 100;

        public SpritePos[] masks;

        public float camRatio;

        public RenderTexture blitDest;

        public Texture2D baseSpriteSINGLE;
        //public Texture2D baseSpriteMULTIPLE;

        //public Dictionary<string, Sprite> bodyParts = new Dictionary<string, Sprite>();

        //public RawImage masked, super;

        public SpritePos currentMask;

        public int currentMaskIndex;

        //public Image previewCANVAS;

        public float previewFadeTime;

        //public RenderTexture rendTex;

        public Material webCamRendMat;

        bool blit = false;

        public Texture2D _scaledMask;


        void Start()
        {

        }

        public void SetUpCamera(bool selfieCam)
        {
            frontCam = GetCamTex(selfieCam);

            frontCam.Play();
            //background.texture = frontCam;
            webCamRendMat.SetTexture("_MainTex", frontCam);
        }

        WebCamTexture GetCamTex(bool selfieCam)
        {
            WebCamDevice[] devices = WebCamTexture.devices;

            if (devices.Length == 0)
            {
                Debug.Log("No Camera Detected");

                camAvailable = false;
                return null;
            }

            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i].isFrontFacing && selfieCam)
                {
                    camAvailable = true;
                    return new WebCamTexture(devices[i].name);//, Screen.width, Screen.height);
                }
                if (!devices[i].isFrontFacing && !selfieCam)
                {
                    camAvailable = true;

                    return new WebCamTexture(devices[i].name);//, Screen.width, Screen.height);
                }
            }

            Debug.Log("could not find desired cam");
            return null; ;
        }

        public void SetCurrentMaskIndex(GameObject button)
        {
            currentMaskIndex = button.transform.GetSiblingIndex();
        }

        public void SetPhotoMask(int maskIndex)
        {
            Sprite mask = masks[maskIndex].single;
            currentMask = masks[maskIndex];
            //maskImageCANVAS.sprite = mask;
            //previewCANVAS.sprite = mask;
            Texture2D t = mask.texture;

            Vector2 tiling = GetTiling(t);
            _scaledMask = GetScaledMask(t, tiling);

            //webCamRendMat.SetTextureScale("_Mask", tiling);

            webCamRendMat.SetTexture("_Mask", _scaledMask);

            // sprite image preview
            //previewCANVAS.canvasRenderer.SetAlpha(1);
            //previewCANVAS.CrossFadeAlpha(0, previewFadeTime, false);
        }

        Vector3 GetTiling(Texture2D mask)
        {
            Vector2 maskAspectRatio = new Vector2((plane.transform.localScale.x * scaleToPixel) / mask.width, (plane.transform.localScale.z * scaleToPixel) / mask.height);
            int xCloserTo1 = Mathf.Abs(maskAspectRatio.x - 1) < Mathf.Abs(maskAspectRatio.y - 1) ? 1 : 0;
            if (xCloserTo1 == 1)
                return new Vector3(1, maskAspectRatio.y / maskAspectRatio.x, xCloserTo1);
            else
                return new Vector3(maskAspectRatio.x / maskAspectRatio.y, 1, xCloserTo1);
        }

        Texture2D GetScaledMask(Texture2D mask, Vector3 tiling)
        {
            Texture2D output = new Texture2D((int)plane.transform.localScale.x * scaleToPixel, (int)plane.transform.localScale.z * scaleToPixel);
            Texture2D scaledSingle = TextureScaler.scaled(mask, (int)(output.width / tiling.x), (int)(output.height / tiling.y));
            //test.texture = scaledSingle;
            Vector2Int offset = tiling.z == 0 ? new Vector2Int((output.width - scaledSingle.width) / 2, 0) : new Vector2Int(0, (output.height - scaledSingle.height) / 2);

            int pixelWidth = mask.width + (offset.x * 2);
            int pixelHeight = mask.height + (offset.y * 2);

            webCamRendMat.SetFloat("_MaskXPixelCount", pixelWidth);
            webCamRendMat.SetFloat("_MaskYPixelCount", pixelHeight);
            webCamRendMat.SetFloat("_XPixelCount", pixelWidth);
            webCamRendMat.SetFloat("_YPixelCount", pixelHeight);


            // make all transparent
            for (int i = 0; i < output.width; i++)
            {
                for (int j = 0; j < output.height; j++)
                {
                    output.SetPixel(i, j, new Color(0, 0, 0, 0));
                }
            }
            // write mask to center
            for (int i = 0; i < scaledSingle.width; i++)
            {
                for (int j = 0; j < scaledSingle.height; j++)
                {
                    output.SetPixel(i + offset.x, j + offset.y, scaledSingle.GetPixel(i, j));
                }
            }

            output.Apply();
            return output;
        }




        //IEnumerator CrossFadeAlpha()
        //{
        //    Texture2D tex = webCamRendMat.GetTexture("_Mask") as Texture2D;

        //}

        // Update is called once per frame
        void Update()
        {
            if(blit)
            {

            }

            if (!camAvailable)
                return;

            if (Input.touchCount > 0)
            {

            }

            camRatio = ((float)frontCam.width / (float)frontCam.height);// * (canvasRect.sizeDelta.x / canvasRect.sizeDelta.y);

            //fitCANVAS.aspectRatio = camRatio;
            //fitPLANE.aspectRatio = camRatio;

            float scaleY = frontCam.videoVerticallyMirrored ? -1f : 1f;
            ///backgroundCANVAS.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -frontCam.videoRotationAngle;
            //backgroundCANVAS.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

        }

        bool SameSize(Texture2D a, Texture2D b)
        {
            if (a.width != b.width)
            {
                Debug.Log(a.name + " has a different width to " + b.name);
                return false;
            }
            if (a.height != b.height)
            {
                Debug.Log(a.name + " has a different height to " + b.name);
                return false;
            }
            return true;
        }

        Texture2D GetMaskedTexture()
        {
            // ref to mask image
            //Texture2D mask = maskImageCANVAS.sprite.texture;

            Texture2D mask = webCamRendMat.GetTexture("_Mask") as Texture2D;

            //RenderTexture rt = (RenderTexture)webCamRendMat.GetTexture("_MainTex");

            Material _m = Resources.Load("PixelateWebcam_R") as Material;
            Material _mP = Resources.Load("Preview_Mat 1") as Material;

            _m.SetTexture("_Mask", _scaledMask);

            Graphics.Blit(webCamRendMat.GetTexture("_MainTex"), _mP);
            blit = true;


            // create file container
            //Texture2D camImage = new Texture2D(frontCam.width, frontCam.height);
            Texture2D camImage = new Texture2D(mask.width, mask.height);

            // cast camTex to Tex2D
            camImage.SetPixels(frontCam.GetPixels());
            // get mask aspect ratio parts
            float maskX = (float)mask.width;
            float maskY = (float)mask.height;
            // sum of sqr parts used as denominator to get unit vector
            float denom = Mathf.Pow(maskX, 2) + Mathf.Pow(maskY, 2);
            // get unit vector
            Vector2 unitVec = new Vector2((float)maskX / (float)Mathf.Sqrt(denom), (float)maskY / (float)Mathf.Sqrt(denom));
            // init dimensions
            Vector2 dimensions = new Vector2();
            // stop crashing...
            int counter = 0;
            // increase magnitude until one dimension hist screen edge
            while (dimensions.x < frontCam.width && dimensions.y < frontCam.height && counter < 500)
            {
                counter++;
                dimensions = new Vector2(dimensions.x + unitVec.x, dimensions.y + unitVec.y);
            }

            // create new image container
            Texture2D croppedCamImage = new Texture2D((int)dimensions.x, (int)dimensions.y);
            // get axis differentials (one of them should be 0)
            int widthDiff = camImage.width - croppedCamImage.width;
            int heightDiff = camImage.height - croppedCamImage.height;

            // write new pixels (effectively cropping out 2 bars on x or y axis)
            for (int x = 0; x < croppedCamImage.width; x++)
            {
                for (int y = 0; y < croppedCamImage.height; y++)
                {
                    int xCoord = x + (int)(widthDiff / 2);
                    int yCoord = y + (int)(heightDiff / 2);
                    croppedCamImage.SetPixel(x, y, camImage.GetPixel(xCoord, yCoord));
                }
            }

            //croppedCamImage.Apply();
            //return croppedCamImage;
            Debug.Log("CCH: " + croppedCamImage.height + ", CCW: " + croppedCamImage.width + "; MH: " + mask.height + ", MW: " + mask.width);
            // scale Cam Image to Mask Image size
            croppedCamImage = TextureScaler.scaled(croppedCamImage, mask.width, mask.height);
            // check same size
            if (!SameSize(mask, croppedCamImage))
                return croppedCamImage;

            // create masked Image
            for (int x = 0; x < croppedCamImage.width; x++)
            {
                for (int y = 0; y < croppedCamImage.height; y++)
                {
                    if (mask.GetPixel(x, y).a > 0.5f)
                        croppedCamImage.SetPixel(x, y, croppedCamImage.GetPixel(x, y));
                    else
                        croppedCamImage.SetPixel(x, y, new Color(1f, 1f, 1f, 0f));
                }
            }

            croppedCamImage.Apply();
            //test.texture = croppedCamImage;
            test.texture = blitDest;
            return croppedCamImage;
        }

        void SuperImposeTex(Texture2D _snapshot)
        {
            //Texture2D _baseSprite = baseSpriteSINGLE;

            Vector2 offset = currentMask.multiple.rect.position;

            // paste _snapShot onto base sprite
            for (int x = (int)offset.x, snapX = 0; x < (int)offset.x + _snapshot.width; x++, snapX++)
            {
                for (int y = (int)offset.y, snapY = 0; y < (int)offset.y + _snapshot.height; y++, snapY++)
                {
                    baseSpriteSINGLE.SetPixel(x, y, _snapshot.GetPixel(snapX, snapY));
                }
            }

            baseSpriteSINGLE.Apply();

            //return _baseSprite;
        }

        public void DefaultTex()
        {
            SuperImposeTex(masks[currentMaskIndex].single.texture);
        }

        public void ResetAllTextures()
        {
            foreach (var item in masks)
            {
                SuperImposeTex(item.single.texture);
            }
        }

        public void TakeSnapshot()
        {
            SuperImposeTex(GetMaskedTexture());
        }
    }
}
