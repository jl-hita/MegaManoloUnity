using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scCoinCaja : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.right * 100);
        Destroy(this.gameObject, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(3f, 0f, 0f, Space.Self);
    }
}
