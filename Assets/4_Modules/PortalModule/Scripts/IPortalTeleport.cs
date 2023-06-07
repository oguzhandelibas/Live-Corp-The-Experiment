using UnityEngine;

namespace PortalModule
{
    public interface IPortalTeleport
    {
        void Teleport(Vector3 position, Quaternion rotation, Transform entry, Transform exit);
        bool UseThreshold { get; }
        Vector3 Position { get; }
    }
}