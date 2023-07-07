using System.Collections;
using UnityEngine;
using TMPro;
public class SubtitleManager : AbstractSingleton<SubtitleManager>
{
    [SerializeField] private TextMeshProUGUI SubtitleText;

    private string currentText = "";
    
    public void SetSubText(AudioTrigger audioTrigger, string subText, float time, bool lastSound)
    {
        StartCoroutine(ShowText(audioTrigger, subText, time, lastSound));
    }
    
    private IEnumerator ShowText(AudioTrigger audioTrigger, string fullText, float time, bool lastSound)
    {
        SubtitleText.gameObject.SetActive(true);
        string currentText = "";
        float delay = (time / fullText.Length) / 1.5f;
        
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            SubtitleText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        
        if (lastSound)
        {
            audioTrigger.InvokeEvent();
            SetSubtitleActiveness(false);    
        }
        
    }

    public void SetSubtitleActiveness(bool value)
    {
        if (!value) StartCoroutine(DeactivateRoutine());
        else SubtitleText.gameObject.SetActive(value);
    }

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        SubtitleText.gameObject.SetActive(false);
    }
}
