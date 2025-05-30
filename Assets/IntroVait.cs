using System.Collections;
using UnityEngine;

public class IntroVait : MonoBehaviour
{
    [SerializeField] private float waitTime = 3f; // Время ожидания в секундах

    private void Start()
    {
        StartCoroutine(WaitForLevel());
    }

    private IEnumerator WaitForLevel()
    {
        yield return new WaitForSeconds(waitTime);
        SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.SceneName.GreenWay, savePlayerData: false);
    }
}