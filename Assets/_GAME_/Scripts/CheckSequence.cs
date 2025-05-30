using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckSeguence : MonoBehaviour
{
    [SerializeField] private List<string> correctSequence = new List<string> { "Stone W", "Stone N", "Stone S", "Stone E" };
    private List<string> currentSequence = new List<string>();

    private List<GameObject> stones = new List<GameObject>();

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemySpawnPoint;

    private int count = 1;

    [SerializeField] private Text winText;

    private void Start()
    {
        foreach (string stoneName in correctSequence)
        {
            GameObject stone = GameObject.Find(stoneName);
            if (stone != null)
            {
                stones.Add(stone);
            }
        }
    }


    public void StoneDestroyed(string stoneName)
    {
        currentSequence.Add(stoneName);

        if (currentSequence.Count == correctSequence.Count)
        {
            for (int i = 0; i < correctSequence.Count; i++)
            {
                if (currentSequence[i] != correctSequence[i])
                {
                    StartCoroutine(ResetSequence());
                    return;
                }
            }

            // —ообщение о правильной последовательности
            ChatManager.Instance.AddMinMessage("Enemy?");
            SpawnEnemy();
        }
    }


    private IEnumerator ResetSequence()
    {
        ChatManager.Instance.AddMinMessage("Try again    " + count++.ToString());
        yield return new WaitForSeconds(2);
        currentSequence.Clear();
        RespawnStones();
    }

    private void RespawnStones()
    {
        foreach (GameObject stone in stones)
        {
            stone.SetActive(true);
            NavMeshSurfaceManagement.Instance.RebakeNavmeshSurface();
        }
    }

    private void SpawnEnemy()
    {
        float offsetX = Random.Range(-7f, 7f);
        float offsetY = Random.Range(-7f, 7f);
        Vector3 spawnPosition = new Vector3(enemySpawnPoint.position.x + offsetX, enemySpawnPoint.position.y + offsetY, enemySpawnPoint.position.z);
        Instantiate(enemyPrefab, spawnPosition, enemySpawnPoint.rotation);
    }

}
