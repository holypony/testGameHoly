using UnityEngine;

public class ScrollContent : MonoBehaviour
{
    public float ItemSpacing { get { return itemSpacing; } }
    public float HorizontalMargin { get { return horizontalMargin; } }
    public float Width { get { return width; } }
    public float ChildWidth { get { return childWidth; } }
    private RectTransform rectTransform;
    public RectTransform[] rtChildren;
    private float width;
    private float childWidth;
    public float ScreenCenter;
    [SerializeField] private float itemSpacing;

    [SerializeField] private float horizontalMargin;

    private void Start()
    {
        ScreenCenter = Screen.width / 2f;
        rectTransform = GetComponent<RectTransform>();
        rtChildren = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }

        width = rectTransform.rect.width - (2 * horizontalMargin);
        childWidth = rtChildren[0].rect.width;

        InitializeContentHorizontal();
    }

    private void InitializeContentHorizontal()
    {

        float originX = ScreenCenter - childWidth - itemSpacing;

        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].position;
            childPos.x = originX;
            originX += childWidth + itemSpacing;
            rtChildren[i].position = childPos;
        }
    }
}