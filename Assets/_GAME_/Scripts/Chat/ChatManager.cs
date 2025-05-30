using UnityEngine;
using TMPro;
using System.Collections;

public class ChatManager : MonoBehaviour
{
    public Transform messageParent;

    // ��� ������ ������� ��� ���������
    public GameObject messageItemMinPrefab;
    public GameObject messageItemMaxPrefab;

    public ChatSlideController slideController; 

    public static ChatManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddMinMessage("�������� ��������� �� Mr. Who.");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            AddMaxMessage("��� ������� ��������� �� Mr. Who, ������� �������� ������ ������������ � ������������ � ������� ������.");
        }
    }

    // �������� ��������� � ��������� �����
    public void AddMinMessage(string text)
    {
        AddMessage(text, messageItemMinPrefab);
    }

    // �������� ��������� � ������� �����
    public void AddMaxMessage(string text)
    {
        AddMessage(text, messageItemMaxPrefab);
    }

    // ����� ������ ��� �������� � ��������� ������
    private void AddMessage(string text, GameObject prefab)
    {
        if (slideController != null && !slideController.IsOpen())
        {
            slideController.ToggleChat();
        }
        var messageGO = Instantiate(prefab, messageParent);
        var textComponent = messageGO.transform.Find("MessageBackground/MessageText").GetComponent<TextMeshProUGUI>();
        textComponent.text = text;

        StartCoroutine(RemoveAfterDelay(messageGO, 10f));
    }

    private IEnumerator RemoveAfterDelay(GameObject messageGO, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(messageGO);
    }
}

public static class MrWho
{
    public static void SayMin(string text) => ChatManager.Instance?.AddMinMessage(text);

    public static void SayMax(string text) => ChatManager.Instance?.AddMaxMessage(text);
}
