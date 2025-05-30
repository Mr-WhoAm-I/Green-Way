using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatSlideController : MonoBehaviour
{
    public float slideDuration = 0.3f;
    [Range(0f, 1f)]
    public float targetWidthPercent = 0.4f;

    private RectTransform chatPanel;
    private float hiddenX;
    private float shownX;
    private bool isOpen = false;

    void Start()
    {
        chatPanel = GetComponent<RectTransform>();
        float fullWidth = ((RectTransform)chatPanel.parent).rect.width;
        float panelWidth = fullWidth * targetWidthPercent;
        shownX = 0f;
        hiddenX = -panelWidth;

        chatPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, panelWidth);
        chatPanel.anchoredPosition = new Vector2(hiddenX, 0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleChat();
        }
    }

    public void ToggleChat()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(SlideCoroutine(isOpen));
    }

    private IEnumerator SlideCoroutine(bool open)
    {
        float elapsed = 0f;
        Vector2 startPos = chatPanel.anchoredPosition;
        Vector2 endPos = new Vector2(open ? shownX : hiddenX, 0f);

        while (elapsed < slideDuration)
        {
            float t = elapsed / slideDuration;
            chatPanel.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        chatPanel.anchoredPosition = endPos;
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
