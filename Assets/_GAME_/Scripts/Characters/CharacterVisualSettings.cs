using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterVisualSettings : MonoBehaviour
{
    [SerializeField] private string sortingLayerName = "Layer 1"; // ��� �������������� ����
    [SerializeField] private int orderInLayer = 2; // ������� � ����
    [SerializeField] private float scaleFactor = 0.7f; // �������
    [SerializeField] private string[] applyToScenes = {"SCDemo" }; // ����� ��� ����������

    private void Start()
    {
        ApplyVisualSettings();
    }

    public void ApplyVisualSettings()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (!System.Array.Exists(applyToScenes, scene => scene == currentScene))
        {
            Debug.Log($"Character visual settings not applied in scene {currentScene} for {gameObject.name}");
            return;
        }

        // ��������� �������������� ���� � ������� ��� ���� SpriteRenderer
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        if (renderers.Length > 0)
        {
            foreach (var renderer in renderers)
            {
                renderer.sortingLayerName = sortingLayerName;
                renderer.sortingOrder = orderInLayer;
                Debug.Log($"Set {renderer.gameObject.name} to Layer: {sortingLayerName}, Order: {orderInLayer}");
            }
        }
        else
        {
            Debug.LogWarning($"No SpriteRenderers found in {gameObject.name}");
        }

        // ��������� ��������
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        Debug.Log($"Set {gameObject.name} scale to {scaleFactor} in {currentScene}");
    }
}