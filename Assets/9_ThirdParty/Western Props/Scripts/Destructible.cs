using System;
using UnityEngine;
using UnityEngine.Events;

namespace BreakableBox
{
	public class Destructible: MonoBehaviour
	{
		[SerializeField] private ActivatePortal _activatePortal;
		public GameObject destroyedVersion;
		public bool CanBreak { get; set; }
		private void OnCollisionEnter(Collision other)
		{
			if(!CanBreak) return;
			Instantiate(destroyedVersion, transform.position, transform.rotation);
			
			AudioManager.Instance.AddAudioClip(4);
			AudioManager.Instance.PlayAudioClip(transform,4);
			_activatePortal.Activate();
			
			Destroy(gameObject);
		}
	}
}

