using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PortalModule
{
    public class PortalTeleportation : MonoBehaviour
    {
        public event UnityAction OnPortalOpen;
        public event UnityAction OnPortalClose;

        [SerializeField] private Transform entryPlane;
        [SerializeField] private float transitThreshold = 0.3f;
        [SerializeField] private Vector2 planeSize;
        [SerializeField] private PortalTeleportation exit;
        [SerializeField] private Color gizmosColor = Color.red;

        private List<PortalTransient> transients = new List<PortalTransient>();

        [SerializeField] private bool isPortalOpened = false;

        public bool IsPortalOpened
        {
            get
            {
                return isPortalOpened;
            }

            private set
            {
                isPortalOpened = value;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var transient = other.GetComponent<IPortalTeleport>();
            if (transient != null)
                transients.Add(new PortalTransient(other.transform, transient));
        }

        public void OpenPortal()
        {
            if (IsPortalOpened)
                return;

            IsPortalOpened = true;
            if (OnPortalOpen != null)
            {
                OnPortalOpen();
            }
        }
        
        public void ClosePortal()
        {
            if (!IsPortalOpened)
                return;

            IsPortalOpened = false;
            if (OnPortalClose != null)
            {
                OnPortalClose();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var transient = other.GetComponent<IPortalTeleport>();
            if (transient != null)
            {
                var instanceID = other.transform.GetInstanceID();
                for (int i = 0; i < transients.Count; i++)
                {
                    if (transients[i].Transform.GetInstanceID() == instanceID)
                    {
                        transients.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void Update()
        {
            if (IsPortalOpened)
            {
                foreach (var transitingObject in transients)
                {
                    if (transitingObject != null)
                    {
                        var position = transitingObject.Teleport.Position;
                        var localPosition = entryPlane.InverseTransformPoint(position);

                        if (PositionInRect(localPosition, planeSize))
                        {
                            float threshold = 0;
                            if (transitingObject.Teleport.UseThreshold)
                                threshold = Vector3.Dot(entryPlane.forward, transitingObject.Transform.forward) * transitThreshold;

                            if (localPosition.z > threshold)
                            {
                                Transit(transitingObject, localPosition);
                            }
                        }
                    }
                }
            }
        }

        private bool PositionInRect(Vector2 position, Vector2 rect)
        {
            return position.x > -rect.x / 2 && position.x < rect.x / 2 && position.y > -rect.y / 2 && position.y < rect.y / 2;
        }

        private void Transit(PortalTransient transient, Vector3 localPosition)
        {
            Vector3 worldPosition;
            Quaternion worldRotation;
            CalculateTransformation(transient, localPosition, out worldPosition, out worldRotation);

            transient.Teleport.Teleport(worldPosition, worldRotation, entryPlane, exit.entryPlane);
        }

        private void CalculateTransformation(PortalTransient transient, Vector3 localPosition, out Vector3 worldPosition, out Quaternion worldRotation)
        {
            var rotation = transient.Transform.rotation;
            var localRotation = Quaternion.Inverse(entryPlane.rotation) * rotation;

            localPosition.z *= -1;
            localPosition.x *= -1;
            worldPosition = exit.entryPlane.TransformPoint(localPosition);
            worldRotation = Quaternion.AngleAxis(180, exit.entryPlane.up) * (exit.entryPlane.rotation * localRotation);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmosColor;
            Gizmos.matrix = entryPlane.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, planeSize);
        }

        [System.Serializable]
        private class PortalTransient
        {
            private Transform transform;
            private IPortalTeleport _teleport;

            public PortalTransient(Transform transform, IPortalTeleport teleport)
            {
                Transform = transform;
                Teleport = teleport;
            }

            public Transform Transform
            {
                get
                {
                    return transform;
                }

                private set
                {
                    transform = value;
                }
            }

            public IPortalTeleport Teleport
            {
                get
                {
                    return _teleport;
                }

                private set
                {
                    _teleport = value;
                }
            }
        }
    }
}