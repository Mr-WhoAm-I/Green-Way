using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SceneTransitionManager initialized");
            LogBuildScenes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum SceneName
    {
        GameIntro,
        Menu,
        GreenWay,
        MainVisual,
        LVL2,
        ChooseCharacter
    }

    public void LoadScene(SceneName sceneName, bool savePlayerData = true)
    {
        Debug.Log($"Attempting to load scene: {sceneName}");
        if (savePlayerData)
        {
            SavePlayerData();
        }

        SceneManager.LoadScene(sceneName.ToString());
    }

    public void LoadMenuWithDelay(float delay)
    {
        StartCoroutine(LoadMenuWithDelayCoroutine(delay));
    }

    private void SavePlayerData()
    {
        if (Player.Instance == null || Player.Instance.gameObject == null)
        {
            Debug.Log("Player not found or destroyed, skipping SavePlayerData");
            return;
        }

        var healthComponent = Player.Instance.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.SaveHealth();
        }
        else
        {
            Debug.LogWarning("HealthComponent not found on Player");
        }
    }

    private IEnumerator LoadMenuWithDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(SceneName.Menu);
    }

    private void LogBuildScenes()
    {
        string sceneList = "Scenes in Build Settings:\n";
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            sceneList += $"[{i}] {SceneUtility.GetScenePathByBuildIndex(i)}\n";
        }
        Debug.Log(sceneList);
    }
}
