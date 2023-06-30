using UnityEngine;

namespace NPC
{
    public class EffectControl : MonoBehaviour
    {
        [SerializeField] private GameObject bloodEffect;
        [SerializeField] private GameObject hitEffect;
        public void CreateBloodEffect(Transform parent, Vector3 pos)
        {
            GameObject blood = Instantiate(bloodEffect, pos, Quaternion.identity, parent);
            GameObject hit = Instantiate(hitEffect, pos, Quaternion.identity, parent);
            
            Destroy(blood,1);
            Destroy(hit,1);
        }
    }
}
