using System;
using Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InputFieldHandler : MonoBehaviour
{
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
