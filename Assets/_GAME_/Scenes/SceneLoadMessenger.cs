using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StartupMessage
{
    [TextArea]
    public string text;
    public bool useMaxMessage = false;
}

public class SceneLoadMessenger : MonoBehaviour
{
    [Header("Messages to display on scene load")]
    [SerializeField] private List<StartupMessage> messages = new List<StartupMessage>();

    [SerializeField] private float delayBetweenMessages = 2f;

    private void Start()
    {
        if (messages != null && messages.Count > 0)
        {
            StartCoroutine(DisplayMessagesRoutine());
        }
    }

    private IEnumerator DisplayMessagesRoutine()
    {
        yield return null;

        var chatSlide = FindObjectOfType<ChatSlideController>();
        if (chatSlide != null)
        {
            chatSlide.ToggleChat();
        }

        foreach (var message in messages)
        {
            if (ChatManager.Instance != null && !string.IsNullOrWhiteSpace(message.text))
            {
                if (message.useMaxMessage)
                    ChatManager.Instance.AddMaxMessage(message.text);
                else
                    ChatManager.Instance.AddMinMessage(message.text);
            }

            yield return new WaitForSeconds(delayBetweenMessages);
        }
    }
}
