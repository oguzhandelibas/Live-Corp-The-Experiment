using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

    public GameObject destroyVersion;

    private void OnMouseDown ()
    {
        Instantiate(destroyVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
