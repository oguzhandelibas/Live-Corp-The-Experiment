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
        foreach (var item in AudioIndex)
        {
            AudioManager.Instance.AddAudioClip(this, item);
        }
        AudioManager.Instance.PlayAudioClip(audioManagerPosition, AudioIndex[0]);
        boxCollider.enabled = false;
    }

    public void InvokeEvent()
    {
        TriggerEvent.Invoke();
    }
}
