using UnityEditor;
using UnityEngine;

public class SetSortingLayerByParent : EditorWindow
{
    [MenuItem("Tools/Set Sorting Layers By Parent")]
    private static void SetSortingLayers()
    {
        // ������� ��� ������� � ������� Layer 1, Layer 2, Layer 3
        GameObject[] layerObjects = { GameObject.Find("Layer 1"), GameObject.Find("Layer 2"), GameObject.Find("Layer 3") };

        foreach (GameObject layerObject in layerObjects)
        {
            if (layerObject == null)
            {
                Debug.LogWarning($"������ {layerObject} �� ������ �� �����!");
                continue;
            }

            // �������� ��� ������� (��������, "Layer 1")
            string sortingLayerName = layerObject.name;

            // ���������, ���������� �� ������������� ����
            if (SortingLayer.NameToID(sortingLayerName) == 0)
            {
                Debug.LogError($"������������� ���� '{sortingLayerName}' �� ����������! �������� ��� � Tags and Layers.");
                continue;
            }

            // ������� ��� SpriteRenderer � �������� ��������
            SpriteRenderer[] spriteRenderers = layerObject.GetComponentsInChildren<SpriteRenderer>(true);

            // ������������� Sorting Layer ��� ������� SpriteRenderer
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.sortingLayerName = sortingLayerName;
                Debug.Log($"���������� ������������� ���� '{sortingLayerName}' ��� {sr.gameObject.name}");
            }
        }

        // ��������� ��������� � �����
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
        Debug.Log("������������� ���� ���������!");
    }

    [MenuItem("Tools/Set Sorting Layers By Parent", true)]
    private static bool ValidateSetSortingLayers()
    {
        // ���������, ��� ����� �������
        return UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().isLoaded;
    }
}