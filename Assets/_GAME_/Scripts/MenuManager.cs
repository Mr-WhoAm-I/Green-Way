using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.SceneName.LVL2);
    }

    public void ChooseCharacter()
    {
        SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.SceneName.ChooseCharacter);
    }

    public void Exit()
    {
        Application.Quit();
    }
}