using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataSaver : MonoBehaviour
{
    [SerializeField] private string ObjectName;
    [SerializeField] private int id;

    public void SaveInt()
    {
        PlayerPrefs.SetInt(ObjectName, id);
    }
}
