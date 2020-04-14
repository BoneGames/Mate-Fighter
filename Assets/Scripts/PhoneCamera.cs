using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        public Renderer planeRend;
        public SnapCam snapCam;
        private bool camAvailable = false;
        public WebCamTexture frontCam;
        public RawImage test;
        public GameObject plane;
        public AspectRatioFitter fitPLANE;
        readonly int scaleToPixel = 100;
        public SpritePos[] masks;
        public Texture2D baseSpriteSINGLE;
        public SpritePos currentMask;
        public int currentMaskIndex;
        public float previewFadeTime;
        public Material webCamRendMat, preview_Mat;
        public Texture2D _scaledMask;

        public void SetUpCamera(bool selfieCam)
        {
            frontCam = GetCamTex(selfieCam);

            frontCam.Play();
            //background.texture = frontCam;
            webCamRendMat.SetTexture("_MainTex", frontCam);
        }

        public void ToFight()
        {
            SceneManager.LoadScene(2);
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
                if (!devices[i].isFrontFacing && selfieCam)
                {
                    camAvailable = true;
                    return new WebCamTexture(devices[i].name);//, Screen.width, Screen.height);
                }
                if (devices[i].isFrontFacing && !selfieCam)
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
            // Get body part sprite from which button pressed
            currentMask = masks[maskIndex];
            Texture2D t = currentMask.single.texture;

            // Get Tile ratio according to screen Plane
            Vector3 planeTiling = GetPlaneTiling(t);
            _scaledMask = GetScaledMask(t, planeTiling);

            // set mask for webcam to render into
            webCamRendMat.SetTexture("_Mask", _scaledMask);
            // set preview
            preview_Mat.mainTexture = _scaledMask;
            // fade preview out
            StartCoroutine(FadePreview(preview_Mat));
        }

        IEnumerator FadePreview(Material preview_Mat)
        {
            preview_Mat.color = new Color(1, 1, 1, 1);
            float alpha = 1;
            float timer = 0;
            while (alpha > 0)
            {
                alpha = Mathf.Lerp(1, 0, timer / previewFadeTime);
                timer += Time.deltaTime;
                preview_Mat.color = new Color(1, 1, 1, alpha);
                yield return null;
            }
        }

        Vector3 GetPlaneTiling(Texture2D mask)
        {
            // get mask to screen ratio
            Vector2 maskToScreenRatio = new Vector2((plane.transform.localScale.x * scaleToPixel) / mask.width, (plane.transform.localScale.z * scaleToPixel) / mask.height);
            int xCloserTo1 = Mathf.Abs(maskToScreenRatio.x - 1) < Mathf.Abs(maskToScreenRatio.y - 1) ? 1 : 0;
            if (xCloserTo1 == 1)
                return new Vector3(1, maskToScreenRatio.y / maskToScreenRatio.x, xCloserTo1);
            else
                return new Vector3(maskToScreenRatio.x / maskToScreenRatio.y, 1, xCloserTo1);
        }

        Texture2D GetScaledMask(Texture2D mask, Vector3 tiling)
        {
            // make plane sized texture
            Texture2D output = new Texture2D((int)plane.transform.localScale.x * scaleToPixel, (int)plane.transform.localScale.z * scaleToPixel);
            // scale mask to fit plane aspect ratio
            Texture2D scaledSingle = TextureScaler.scaled(mask, (int)((float)output.width / tiling.x), (int)((float)output.height / tiling.y));
            // commit new pixels to memory
            scaledSingle.Apply();
            // create offset to centre it
            Vector2Int offset = tiling.z == 0 ? new Vector2Int((output.width - scaledSingle.width) / 2, 0) : new Vector2Int(0, (output.height - scaledSingle.height) / 2);
            // save scaling info for later
            //currentMaskInfo = new ScaledMaskInfo(scaledSingle, offset, tiling);

            // cancel out screen aspect ratio for square pixels
            float pixelWidth = mask.width * tiling.x;
            float pixelHeight = mask.height * tiling.y;

            // set webcamTex & Mask overlay to match pixel count of original mask texture
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

        //void Update()
        //{
        //    //if (blit)
        //    //    Graphics.Blit(webCamRendMat.GetTexture("_MainTex"), blitDest, webCamRendMat);

        //    if (!camAvailable)
        //        return;

        //    if (Input.touchCount > 0)
        //    {

        //    }

        //    camRatio = ((float)frontCam.width / (float)frontCam.height);// * (canvasRect.sizeDelta.x / canvasRect.sizeDelta.y);

        //    //fitCANVAS.aspectRatio = camRatio;
        //    //fitPLANE.aspectRatio = camRatio;

        //    float scaleY = frontCam.videoVerticallyMirrored ? -1f : 1f;
        //    ///backgroundCANVAS.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        //    int orient = -frontCam.videoRotationAngle;
        //    //backgroundCANVAS.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

        //}

        public bool SameColorIgnoreAlpha(Color pixel, Color bg)
        {
            Vector3 colors = new Vector3(pixel.r, pixel.g, pixel.b);
            Vector3 bgs = new Vector3(bg.r, bg.g, bg.b);
            if (colors == bgs)
            {
                return true;
            }
            return false;
        }

        Vector2 GetDimensions(Texture2D mask, Texture2D camImage)
        {
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
            // increase magnitude until one dimension hits screen edge
            while (dimensions.x < camImage.width && dimensions.y < camImage.height && counter < 1000)
            {
                if ((camImage.width - dimensions.x) > unitVec.x * 100 && (camImage.height - dimensions.y) > unitVec.y * 100)
                {
                    dimensions = new Vector2(dimensions.x + unitVec.x * 100, dimensions.y + unitVec.y * 100);
                }
                dimensions = new Vector2(dimensions.x + unitVec.x, dimensions.y + unitVec.y);
                counter++;
            }

            return dimensions;
        }

        Texture2D GetMaskedTexture()
        {
            // screenshot
            Texture2D camImage = snapCam.SnapShot();
            // dimensions of scaled up mask
            Vector2 dimensions = GetDimensions(currentMask.single.texture, camImage);
            // create new image container
            Texture2D croppedCamImage = new Texture2D((int)dimensions.x, (int)dimensions.y);
            // get axis differentials (one of them should be 0)
            int widthDiff = camImage.width - croppedCamImage.width;
            int heightDiff = camImage.height - croppedCamImage.height;

            // write new pixels (effectively cropping out 2 bars on x or y axis)
            Color bg = Camera.main.backgroundColor;
            for (int x = 0; x < croppedCamImage.width; x++)
            {
                for (int y = 0; y < croppedCamImage.height; y++)
                {
                    int xOffset = (int)((float)widthDiff / 2f);
                    int yOffset = (int)((float)heightDiff / 2f);
                    int xCoord = x + xOffset;
                    int yCoord = y + yOffset;
                    Color pixel = camImage.GetPixel(xCoord, yCoord);
                    if (SameColorIgnoreAlpha(pixel, bg))
                    {
                        pixel = new Color(0, 0, 0, 0);
                    }
                    croppedCamImage.SetPixel(x, y, pixel);
                }
            }

            croppedCamImage = TextureScaler.scaled(croppedCamImage, (int)currentMask.multiple.rect.width, (int)currentMask.multiple.rect.height);
            croppedCamImage.Apply();

            return croppedCamImage;
        }

        void SuperImposeTex(Texture2D _snapshot)
        {
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
        }

        public void DefaultTex()
        {
            SuperImposeTex(masks[currentMaskIndex].single.texture);
        }

        public void ResetAllTextures()
        {
            for (int i = 0; i < masks.Length; i++)
            {
                currentMask = masks[i];
                SuperImposeTex(masks[i].single.texture);
            }
        }

        public void TakeSnapshot()
        {
            SuperImposeTex(GetMaskedTexture());
        }
    }
}
