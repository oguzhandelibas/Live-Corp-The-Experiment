using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePortalAfterDestruct : MonoBehaviour
{
    [SerializeField] private ActivatePortal _activatePortal;
    
    public void Activate()
    {
        AudioManager.Instance.AddAudioClip(new int[1]{4});
        _activatePortal.Activate();
    }
}
