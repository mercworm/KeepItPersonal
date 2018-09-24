using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour {

    public Rigidbody rb;
    public float speed;

    private void Update()
    {
        rb.velocity = Vector3.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
