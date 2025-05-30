using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private CheckSeguence �heckSeguence;

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon") )
        {
            �heckSeguence.StoneDestroyed(gameObject.name);
            gameObject.SetActive(false);
            NavMeshSurfaceManagement.Instance.RebakeNavmeshSurface();
        }
    }
}
