using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    [SerializeField] private string layer;
    [SerializeField] private string sortingLayer;

    private void OnTriggerExit2D(Collider2D other)
    {
        // Меняем слой корневого объекта
        other.gameObject.layer = LayerMask.NameToLayer(layer);

        // Меняем слой всех дочерних объектов
        Transform[] allTransforms = other.gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in allTransforms)
        {
            t.gameObject.layer = LayerMask.NameToLayer(layer);
        }

        // Меняем сортировочный слой для всех SpriteRenderer
        SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (SpriteRenderer sr in srs)
        {
            sr.sortingLayerName = sortingLayer;
        }

        Debug.Log($"Set layer to {layer} and sorting layer to {sortingLayer} for {other.gameObject.name} and its children");
    }
}