using System.Collections;
using UnityEngine;
using Cinemachine;

public class AltarTrigger : MonoBehaviour
{
    public int runeIndex;
    public Cainos.PixelArtTopDown_Basic.PropsAltar altar;

    public CinemachineVirtualCamera altarCam;
    public CinemachineVirtualCamera playerCam;

    [TextArea]
    public string activationMessage;

    public int layerIndexRequired = 0;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[AltarTrigger] Entered trigger: {other.name}");

        if (activated)
        {
            Debug.Log("[AltarTrigger] Already activated — ignoring.");
            return;
        }

        if (!other.CompareTag("Player"))
        {
            Debug.Log("[AltarTrigger] Not the player — ignoring.");
            return;
        }

        int playerLayer = other.gameObject.layer;
        Debug.Log($"[AltarTrigger] Player layer: {playerLayer}, Required: Layer {layerIndexRequired} -> {LayerMask.NameToLayer("Layer " + layerIndexRequired)}");

        if (playerLayer != layerIndexRequired)
        {
            Debug.Log("[AltarTrigger] Player not on the correct layer — ignoring.");
            return;
        }

        activated = true;
        altar.ActivateRune(runeIndex);
        Debug.Log($"[AltarTrigger] Activating rune {runeIndex}!");

        StartCoroutine(SwitchToAltarCamera());
    }

    private IEnumerator SwitchToAltarCamera()
    {
        altarCam.Priority = 20;
        playerCam.Priority = 10;

        if (!string.IsNullOrEmpty(activationMessage))
        {
            ChatManager.Instance.AddMaxMessage(activationMessage);
        }

        yield return new WaitForSeconds(3.5f);

        altarCam.Priority = 10;
        playerCam.Priority = 20;
    }
}
