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

    public void ResetSubtitle()
    {
        StopAllCoroutines();
        SetSubtitleActiveness(false, 0.0f);  
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
            if(audioTrigger!= null) audioTrigger.InvokeEvent();
            SetSubtitleActiveness(false, 0.25f);    
        }
        
    }

    public void SetSubtitleActiveness(bool value, float time = 0.0f)
    {
        if (!value) StartCoroutine(DeactivateRoutine(time));
        else SubtitleText.gameObject.SetActive(value);
    }

    IEnumerator DeactivateRoutine(float time = 0.0f)
    {
        yield return new WaitForSeconds(time);
        SubtitleText.gameObject.SetActive(false);
    }
}
