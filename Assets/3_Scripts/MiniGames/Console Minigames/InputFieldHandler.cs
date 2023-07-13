using System;
using Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class InputFieldHandler : MonoBehaviour
{
    public UnityEvent OnConsoleHack;
    [SerializeField] private TMP_InputField m_TextMeshProUGUI;
    [SerializeField] private ConsoleData consoleData;
        
    private void Start()
    {
        m_TextMeshProUGUI.onEndEdit.AddListener(OnEndEdit);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            m_TextMeshProUGUI.interactable = true;
            m_TextMeshProUGUI.Select();
        }
    }

    private void OnEndEdit(string value)
    {
        if (consoleData.HasContain(value))
        {
            if (value == consoleData.HackCommandContent(0))
            {
                OnConsoleHack?.Invoke();
                AudioManager.Instance.AddAudioClip(new int[2]{21,22});
            }
            Debug.Log("Hack command found!");
            PlayerController.Instance.ConsolePanelActiveness(false);
        }
        else
        {
            Debug.Log("Hack command not found!");
        }
        
        m_TextMeshProUGUI.text = "";
        EventSystem.current.SetSelectedGameObject(m_TextMeshProUGUI.gameObject);
        PlayerController.Instance.ConsolePanelActiveness(false);
    }
}
