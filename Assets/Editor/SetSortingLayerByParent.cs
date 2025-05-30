using UnityEditor;
using UnityEngine;

public class SetSortingLayerByParent : EditorWindow
{
    [MenuItem("Tools/Set Sorting Layers By Parent")]
    private static void SetSortingLayers()
    {
        // Находим все объекты с именами Layer 1, Layer 2, Layer 3
        GameObject[] layerObjects = { GameObject.Find("Layer 1"), GameObject.Find("Layer 2"), GameObject.Find("Layer 3") };

        foreach (GameObject layerObject in layerObjects)
        {
            if (layerObject == null)
            {
                Debug.LogWarning($"Объект {layerObject} не найден на сцене!");
                continue;
            }

            // Получаем имя объекта (например, "Layer 1")
            string sortingLayerName = layerObject.name;

            // Проверяем, существует ли сортировочный слой
            if (SortingLayer.NameToID(sortingLayerName) == 0)
            {
                Debug.LogError($"Сортировочный слой '{sortingLayerName}' не существует! Создайте его в Tags and Layers.");
                continue;
            }

            // Находим все SpriteRenderer в дочерних объектах
            SpriteRenderer[] spriteRenderers = layerObject.GetComponentsInChildren<SpriteRenderer>(true);

            // Устанавливаем Sorting Layer для каждого SpriteRenderer
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.sortingLayerName = sortingLayerName;
                Debug.Log($"Установлен сортировочный слой '{sortingLayerName}' для {sr.gameObject.name}");
            }
        }

        // Сохраняем изменения в сцене
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
        Debug.Log("Сортировочные слои обновлены!");
    }

    [MenuItem("Tools/Set Sorting Layers By Parent", true)]
    private static bool ValidateSetSortingLayers()
    {
        // Проверяем, что сцена активна
        return UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().isLoaded;
    }
}