using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;



public class ScaledContentFitter : MonoBehaviour
{
    Vector4 topLeft = new Vector4(0, 1, 0, 1);
    Vector4 topRight = new Vector4(1, 1, 1, 1);
    Vector4 bottomLeft = new Vector4(0, 0, 0, 0);
    Vector4 bottomRight = new Vector4(1, 0, 1, 0);

    [System.Serializable]
    public struct xyz
    {
        public RectTransform element;
        public bool controlWidth;
        public bool widthAdjustable;
        [Range(0f, 1f)]
        public float xPortion;
        public Vector2 spacing;


        public xyz(RectTransform element, bool controlWidth, bool widthAdjustable, float xPortion, Vector2 spacing)
        {
            this.element = element;
            this.controlWidth = true;
            this.widthAdjustable = widthAdjustable;
            this.xPortion = xPortion;
            this.spacing = spacing;
        }
    }
    [Range(0f, 1f)]
    public float minimumPortion;

    public bool edit;

    public RectTransform parentRect => GetComponent<RectTransform>();

    public List<xyz> itemsToFit;
    public Canvas Canvas => GetComponentInParent<Canvas>();



    int GetNewItemIndex()
    {
        for (int i = 0; i < itemsToFit.Count; i++)
        {
            if (itemsToFit[i].xPortion <= 0)
            {
                return i;
            }
        }
        return -1;
    }
    [Button]
    public void DistributeEvenly()
    {
        float x = 1 / (float)itemsToFit.Count;
        for (int i = 0; i < itemsToFit.Count; i++)
        {
            itemsToFit[i] = new xyz(itemsToFit[i].element, true, true, x, itemsToFit[i].spacing);
        }
        ApplyValues();
    }

    [Button]
    public void StretchCurrentToBoundary()
    {
        float xPortionSum = itemsToFit.Sum(s => s.xPortion);
        for (int i = 0; i < itemsToFit.Count; i++)
        {
            float spaceRatio = itemsToFit[i].xPortion / xPortionSum;
            itemsToFit[i] = new xyz(itemsToFit[i].element, true, true, spaceRatio, itemsToFit[i].spacing);
        }
        ApplyValues();
    }

    [Button]
    public void AnchorAllTopLeft()
    {
        //RectTransform[] childRTs = transform.GetComponentsInChildren<RectTransform>();
        List<RectTransform> childRTs = GetChildRects();
        for (int i = 1; i < childRTs.Count; i++)
        {
            childRTs[i].anchorMin = new Vector2(0, 1);
            childRTs[i].anchorMax = new Vector2(0, 1);
            // set pivot
            childRTs[i].pivot = new Vector2(0, 1);
        }
    }

    List<RectTransform> GetChildRects()
    {
        List<RectTransform> childRTs = new List<RectTransform>();
        foreach (Transform item in transform)
        {
            childRTs.Add(item.GetComponent<RectTransform>());
        }
        childRTs = childRTs.OrderBy(o => o.anchoredPosition.x).ToList();
        return childRTs;
    }

    [Button]
    public void GetCurrentSpacing()
    {
        // get child rect transforms
        List<RectTransform> childRTs = GetChildRects();
        // init info struct list
        List<xyz> currentSpacingList = new List<xyz>();
        // init anchor
        float currentAnchorPortion = 0;
        for (int i = 0; i < childRTs.Count; i++)
        {
            // spacing
            RectTransform _rect = childRTs[i];
            float preSpaceLength = _rect.anchoredPosition.x - (currentAnchorPortion * parentRect.rect.width);
            if(Mathf.Abs(preSpaceLength) < 0.005f)
                preSpaceLength = 0;

            float postSpaceLength = i == (childRTs.Count - 1) ? parentRect.rect.width - (_rect.anchoredPosition.x + _rect.rect.width) : 0;
            if (Mathf.Abs(postSpaceLength) < 0.005f)
                postSpaceLength = 0;

            float preSpacePortion = (_rect.anchoredPosition.x / parentRect.rect.width) - currentAnchorPortion;
            if (Mathf.Abs(preSpacePortion) < 0.005f)
                preSpacePortion = 0;

            float postSpacePortion = postSpaceLength == 0 ? 0 : postSpaceLength / parentRect.rect.width;
            if (Mathf.Abs(postSpacePortion) < 0.005f)
                postSpacePortion = 0;

            Vector2 _spacing = new Vector2(preSpacePortion, postSpacePortion);
            // xportion (includes spacing)
            float newXPortion = (preSpaceLength + _rect.rect.width + postSpaceLength) / parentRect.rect.width;
            // add to collection
            currentSpacingList.Add(new xyz(_rect, true, true, newXPortion, _spacing));
            // update anchor
            currentAnchorPortion += newXPortion;
        }
        itemsToFit = currentSpacingList;
    }

    [Button]
    public void MakeRoomForNewElements()
    {
        // check that there is new element to make room for (something with zero width)
        int zeros = itemsToFit.Where(o => o.xPortion == 0).Count();
        if (zeros == 0)
        {
            Debug.Log("no new items to make room for");
            return;
        }

        // make new element portion equal to smallest current element portion that isn't zero
        float newElementPortion = itemsToFit.OrderBy(item => item.xPortion).ToList()[zeros].xPortion;

        // make proportional changes
        for (int i = 0; i < itemsToFit.Count; i++)
        {
            xyz item = itemsToFit[i];
            // if current element
            if (itemsToFit[i].xPortion != 0)
            {
                // remove the (new portion * number of new portions) / num of old portions
                int existingPortions = itemsToFit.Count - zeros;
                float newXportion = item.xPortion - ((newElementPortion * zeros) / existingPortions);
                itemsToFit[i] = new xyz(item.element, true, true, newXportion, itemsToFit[i].spacing);
            }
            else
            {
                itemsToFit[i] = new xyz(item.element, true, true, newElementPortion, itemsToFit[i].spacing);
            }
        }
        // apply
        ApplyValues();

    }

    [Button]
    public void ApplyValues()
    {
        float anchorBase = 0;
        for (int i = 0; i < itemsToFit.Count; i++)
        {
            xyz item = itemsToFit[i];
            RectTransform rect = item.element;
                // set anchor
            //rect.anchorMin = new Vector2(anchorBase + item.spacing.x, topLeft.y);
            //rect.anchorMax = new Vector2(anchorBase - item.spacing.y, topLeft.w);
            //rect.anchoredPosition = new Vector3(0, 0, 0);
                // set position (with pre space)
            rect.anchoredPosition = new Vector2((anchorBase + item.spacing.x) * parentRect.rect.width, 0);
            // set size
            float xSizePortion = item.xPortion - (item.spacing.x + item.spacing.y);

            rect.sizeDelta = new Vector2(xSizePortion * parentRect.rect.width, rect.sizeDelta.y);
                // increment anchorPoint
            anchorBase += item.xPortion;
        }
    }

    private void OnValidate()
    {
        if (edit)
        {
            //DistributeEvenly();
            return;
        }      // SIZING
        float widthPortionRemianing = 1;


        float items = itemsToFit.Count;
        float overFlow = 0;
        int cycles = 0;



        bool distributing = false;
        foreach (var item in itemsToFit)
        {
            if (item.xPortion == 0)
            {
                distributing = true;
                break;
            }
        }

        while (distributing && cycles < 20)
        {
            int newItem = GetNewItemIndex();
            // anti-crash
            cycles++;

            for (int i = 0; i < items; i++)
            {
                float standardPortionSize = (1f / items) - overFlow;
                // init new item with standard portion of fill size
                if (i == newItem)
                {
                    itemsToFit[i] = new xyz(itemsToFit[i].element, true, true, standardPortionSize, itemsToFit[i].spacing);
                    widthPortionRemianing -= standardPortionSize;
                }
                else
                {

                    float newXportion = itemsToFit[i].xPortion - (standardPortionSize / (items - 1));
                    if (newXportion < minimumPortion)
                    {
                        overFlow += (Mathf.Abs(newXportion) - minimumPortion);
                        newXportion = minimumPortion;
                    }

                    itemsToFit[i] = new xyz(itemsToFit[i].element, true, true, newXportion, itemsToFit[i].spacing);
                }
            }
            // run loop again?
            distributing = overFlow == 0 ? true : false;
        }

        // ANCHORING
        float anchorPoint = 0;
        for (int i = 0; i < itemsToFit.Count; i++)
        {
            if (itemsToFit[i].controlWidth)
            {
                // get rect
                RectTransform rect = itemsToFit[i].element;
                // set anchor
                rect.anchorMin = new Vector2(anchorPoint, topLeft.y);
                rect.anchorMax = new Vector2(anchorPoint, topLeft.w);
                rect.anchoredPosition = new Vector3(0, 0, 0);
                // set pivot
                rect.pivot = new Vector2(0, 1);
                float x = parentRect.rect.width * itemsToFit[i].xPortion;
                rect.sizeDelta = new Vector2(x, rect.sizeDelta.y);
                anchorPoint += itemsToFit[i].xPortion;
            }
            else
            {
                // get rect
                // RectTransform rect = itemsToFit[i].element;

            }

        }

    }
    void Start()
    {
        //parentRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
