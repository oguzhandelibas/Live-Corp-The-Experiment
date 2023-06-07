using UnityEngine;

namespace PortalModule
{
    public class Teleporter : MonoBehaviour, IPortalTeleport
    {
        [SerializeField] private bool useThreshold = false;

        public bool UseThreshold { get { return useThreshold; } }
        public Vector3 Position { get { return transform.position; } }
        

        public void Teleport(Vector3 position, Quaternion rotation, Transform entry, Transform exit)
        {
            Vector3 offset = transform.position - Position;
            transform.position = position - offset;
            transform.rotation = rotation;
        }
    }
}