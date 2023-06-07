using System.Collections;
using UnityEngine;
using TMPro;
public class SubtitleManager : AbstractSingleton<SubtitleManager>
{
    [SerializeField] private TextMeshProUGUI SubtitleText;

    private string currentText = "";
    
    public void SetSubText(string subText, float time)
    {
        StartCoroutine(ShowText(subText, time));
    }
    
    private IEnumerator ShowText(string fullText, float time)
    {
        string currentText = "";
        float delay = (time / fullText.Length) / 2;
        
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            SubtitleText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
