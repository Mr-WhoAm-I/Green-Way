using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private CheckSeguence ñheckSeguence;

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon") )
        {
            ñheckSeguence.StoneDestroyed(gameObject.name);
            gameObject.SetActive(false);
            NavMeshSurfaceManagement.Instance.RebakeNavmeshSurface();
        }
    }
}
