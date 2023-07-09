using System;
using UnityEngine;
using UnityEngine.Events;

namespace BreakableBox
{
	public class Destructible: MonoBehaviour
	{
		public UnityEvent OnDestruct;
		public GameObject destroyedVersion;
		public bool CanBreak { get; set; }
		private void OnCollisionEnter(Collision other)
		{
			if(!CanBreak) return;
			Instantiate(destroyedVersion, transform.position, transform.rotation);
			OnDestruct?.Invoke();
			Destroy(gameObject);
		}
	}
}

