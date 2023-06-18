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

    private void OnEndEdit(string value)
    {
        if (consoleData.HasContain(value))
        {
            Debug.Log("Hack command found!");
            // İstenilen işlemleri burada yapabilirsiniz.
        }
        else
        {
            Debug.Log("Hack command not found!");
        }

        // Input Field'ı temizleme
        m_TextMeshProUGUI.text = "";
        EventSystem.current.SetSelectedGameObject(m_TextMeshProUGUI.gameObject);
    }
}
