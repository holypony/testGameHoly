using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UiManager : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private ScrollContent scrollContent;
    [SerializeField] private float outOfBoundsThreshold;

    private ScrollRect scrollRect;
    private Vector2 lastDragPosition;
    private bool positiveDrag;
    private WaitForSeconds shortWait = new WaitForSeconds(0.1f);
    private WaitForSeconds longWait = new WaitForSeconds(5f);
    private Vector2 ScrollVelocity = Vector2.zero;
    private float[] positions;
    private bool isScrolling = false;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

        CalculatePositions();

        StartCoroutine(Demo());

        IEnumerator Demo()
        {
            yield return longWait;

            int i = 0;
            while (true)
            {
                if (!isScrolling)
                {
                    i++;
                    page();
                }

                yield return longWait;
            }
        }
    }
    int nextIndex = 0;
    private void page()
    {
        Vector3 rectPos = scrollContent.rtChildren[0].position;
        for (int t = 0; t < scrollContent.rtChildren.Length; t++)
        {
            nextIndex++;
            if (nextIndex > 2) nextIndex = 0;
            rectPos.x = positions[nextIndex];
            scrollRect.content.GetChild(t).position = rectPos;
        }
        scrollRect.content.GetChild(2).SetSiblingIndex(0);
        nextIndex = 0;
    }

    private IEnumerator StartScrolling()
    {
        isScrolling = true;
        yield return shortWait;
        int i = 0;
        while (i < 50)
        {
            i++;
            if (Mathf.Abs(scrollRect.velocity.x) < 500f)
            {
                Stop();
            }
            yield return shortWait;
        }
        Stop();
    }

    void Stop()
    {

        isScrolling = false;
        scrollRect.velocity = Vector2.zero;

        Vector3 rectPos = scrollContent.rtChildren[0].position;

        rectPos.x = positions[0];
        scrollRect.content.GetChild(0).position = rectPos;

        rectPos.x = positions[1];
        scrollRect.content.GetChild(1).position = rectPos;

        rectPos.x = positions[2];
        scrollRect.content.GetChild(2).position = rectPos;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;

        StartCoroutine(StartScrolling());
    }

    public void OnDrag(PointerEventData eventData)
    {
        positiveDrag = eventData.position.x > lastDragPosition.x;
        lastDragPosition = eventData.position;
    }

    public void OnViewScroll()
    {
        HandleHorizontalScroll();
    }

    private void HandleHorizontalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);

        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag)
        {
            newPos.x = endItem.position.x - scrollContent.ChildWidth - scrollContent.ItemSpacing;
        }
        else
        {
            newPos.x = endItem.position.x + scrollContent.ChildWidth + scrollContent.ItemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    private bool ReachedThreshold(Transform item)
    {
        float posXThreshold = transform.position.x + scrollContent.Width * 0.5f + outOfBoundsThreshold;
        float negXThreshold = transform.position.x - scrollContent.Width * 0.5f - outOfBoundsThreshold;
        return positiveDrag ? item.position.x - scrollContent.ChildWidth * 0.5f > posXThreshold :
            item.position.x + scrollContent.ChildWidth * 0.5f < negXThreshold;
    }

    private void CalculatePositions()
    {
        positions = new float[3];

        positions[0] = scrollContent.ScreenCenter - Screen.width;
        positions[1] = scrollContent.ScreenCenter;
        positions[2] = scrollContent.ScreenCenter + Screen.width;
    }
}