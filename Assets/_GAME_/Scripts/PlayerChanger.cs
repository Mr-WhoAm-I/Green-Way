using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerChanger : MonoBehaviour
{
    [SerializeField] private List<GameObject> character;
    [SerializeField] private Vector3 position;          
    [SerializeField] private GameObject playerParent;

    private int characterId;

    void Awake()
    {
        characterId = PlayerPrefs.GetInt("Character");
        GameObject newCharacter = Instantiate(character[characterId], playerParent.transform);
        newCharacter.transform.localPosition = position; 
        newCharacter.transform.localRotation = Quaternion.identity;
    }
}
