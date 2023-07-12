using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Self Parameters")]
    [SerializeField] private Transform TurretHead;
    [SerializeField] private Transform muzzleSocket;
    [SerializeField] private GameObject prefabProjectile;
    [SerializeField] private float rotationSpeed;

    [Header("Dependency")]
    [SerializeField] private Transform PlayerTransform;
    
    private void Start()
    {
        Debug.Log("burdaa yım");
        if (PlayerTransform == null) PlayerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (PlayerTransform != null)
        {
            Vector3 playerPosition = PlayerTransform.position;
            Vector3 turretPosition = TurretHead.position;
            
            Vector3 direction = playerPosition - turretPosition;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            TurretHead.rotation = Quaternion.Lerp(TurretHead.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void ActivateTurret()
    {
        InvokeRepeating("Shoot", 0.0f, 1.0f);
    }

    private void Shoot()
    {
        
        Quaternion rotation = Quaternion.LookRotation(PlayerTransform.position - muzzleSocket.position);
        GameObject projectile = Instantiate(prefabProjectile, muzzleSocket.position, rotation);
        Debug.Log("owe " + projectile);
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 200;  
    }
}
