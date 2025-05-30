using UnityEngine;

public class AltarHintTrigger : MonoBehaviour
{
    [TextArea]
    public string hintMessage;

    public int layerIndexRequired = 0;

    private bool shown = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shown) return;
        if (!other.CompareTag("Player")) return;

        int playerLayer = other.gameObject.layer;
        if (playerLayer != layerIndexRequired) return;

        if (!string.IsNullOrEmpty(hintMessage))
        {
            ChatManager.Instance.AddMaxMessage(hintMessage);
            shown = true;
        }
    }
}
