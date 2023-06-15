using System;
using UnityEngine;

namespace BreakableBox
{
	public class Destructible: MonoBehaviour 
	{

		public GameObject destroyedVersion;
		public bool CanBreak { get; set; }
		private void OnCollisionEnter(Collision other)
		{
			if(!CanBreak) return;
			Instantiate(destroyedVersion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}

