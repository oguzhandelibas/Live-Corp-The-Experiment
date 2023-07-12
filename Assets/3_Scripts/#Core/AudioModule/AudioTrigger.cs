using Player;
using UnityEngine;
using UnityEngine.Events;

public class AudioTrigger : MonoBehaviour
{
    public UnityEvent OnTrigger;
    public UnityEvent TriggerEvent;
    [SerializeField] private Transform audioManagerPosition;
    [SerializeField] private int[] AudioIndex;
    private BoxCollider boxCollider;

    private void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() == null) return;
        OnTrigger?.Invoke();
        AudioManager.Instance.AddAudioClip(this, AudioIndex);
        AudioManager.Instance.SetSpeakerPosition(audioManagerPosition);
        //AudioManager.Instance.PlayAudioClip(AudioIndex[0]);
        boxCollider.enabled = false;
    }

    public void InvokeEvent()
    {
        Debug.Log("After Sound");
        TriggerEvent.Invoke();
    }
}
