using UnityEngine;
using UnityEngine.UI;

public class SidePanelController : MonoBehaviour
{
    public RectTransform panel;
    public float animationSpeed = 800f;

    private bool isVisible = false;
    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;
    private float targetX;

    void Start()
    {
        float width = Screen.width * 0.4f;
        panel.sizeDelta = new Vector2(width, panel.sizeDelta.y);

        hiddenPosition = new Vector2(-width, 0);
        visiblePosition = new Vector2(0, 0);
        panel.anchoredPosition = hiddenPosition;
        targetX = hiddenPosition.x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isVisible = !isVisible;
            targetX = isVisible ? visiblePosition.x : hiddenPosition.x;
        }

        // Плавное движение
        Vector2 pos = panel.anchoredPosition;
        pos.x = Mathf.Lerp(pos.x, targetX, Time.deltaTime * 10f);
        panel.anchoredPosition = pos;
    }
}
