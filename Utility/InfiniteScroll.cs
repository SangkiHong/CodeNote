using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Parent of list items has VerticalLayoutGroup and ContentSizeFitter component. Turn off both components after setting certain positions and intervals.
// Change the MovementType in ScrollRect to Unrestricted.
public class InfiniteScroll : MonoBehaviour
{
    [Header("Need")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private VerticalLayoutGroup verticalLayout;
    [SerializeField] private RectTransform[] itemTransforms;

    // Index list for order index management (Bottom -> Top)
    List<int> indexList = new List<int>();

    private RectTransform contentTransform;

    // Vector2 for scroll detection range
    private Vector2 scrollRange;
    private float switchingSize;


    private void Awake()
    {
        contentTransform = scrollRect.content;

        for (int i = 0; i < itemTransforms.Length; i++)
        { 
            indexList.Add(i); 
        }

        switchingSize = itemTransforms[0].sizeDelta.y + verticalLayout.spacing;
        scrollRange.y = -switchingSize;
    }

    private void FixedUpdate()
    {
        if (contentTransform != null)
        {
            float anchoredPosY = contentTransform.anchoredPosition.y;
            if (anchoredPosY > 0)
            {
                scrollRect.movementType = ScrollRect.MovementType.Elastic;
            }
            else
            {
                if (scrollRect.movementType == ScrollRect.MovementType.Elastic)
                    scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

                if (scrollRange.x < anchoredPosY && scrollRange.x < 0)
                {
                    TopToBottom();
                }
                else if (scrollRange.y > anchoredPosY)
                {
                    BottomToTop();
                }
            }
        }
    }

    // Method that move immediately to the corresponding index
    public void MoveTo(int focusIndex)
    {
        if (focusIndex > 3)
        {
            float adjustHeight = (focusIndex - 3) * switchingSize;
            Vector2 setPoint = Vector2.up * -adjustHeight;
            contentTransform.anchoredPosition = setPoint;

            scrollRange.x = -adjustHeight;
            scrollRange.y = -adjustHeight - switchingSize;

            float bottomHeight = -2360 + adjustHeight;
            for (int i = 0; i < itemTransforms.Length; i++)
            {
                Vector2 setPos = itemTransforms[i].anchoredPosition;
                setPos.y = bottomHeight + (i * switchingSize);
                itemTransforms[i].anchoredPosition = setPos;
            } 
        }
    }


    private void BottomToTop()
    {
        scrollRange.x -= switchingSize;
        scrollRange.y -= switchingSize;

        int bottomIndex = indexList.First();
        int topIndex = indexList.Last();

        Vector2 anchoredPos = itemTransforms[topIndex].anchoredPosition;
        anchoredPos.y += switchingSize;
        itemTransforms[bottomIndex].anchoredPosition = anchoredPos;
        indexList.RemoveAt(0);
        indexList.Add(bottomIndex);
    }

    private void TopToBottom()
    {
        if (scrollRange.x < 0)
        {
            scrollRange.x += switchingSize;
            scrollRange.y += switchingSize;
        }

        int bottomIndex = indexList.First();
        int topIndex = indexList.Last();

        Vector2 anchoredPos = itemTransforms[bottomIndex].anchoredPosition;
        anchoredPos.y -= switchingSize;
        itemTransforms[topIndex].anchoredPosition = anchoredPos;
        indexList.RemoveAt(indexList.Count - 1);
        indexList.Insert(0, topIndex);
    }
}
