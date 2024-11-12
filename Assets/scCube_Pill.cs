using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scCube_Pill : MonoBehaviour
{
    public GameObject pill;
    private Rigidbody rbCaja;
    private Boolean usado;
    [SerializeField]
    Material materialFinal;

    void Start()
    {
        rbCaja = transform.parent.GetComponent<Rigidbody>();
        usado = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Contains("Cabeza") && usado==false)
        {
            usado = true;
            Instantiate(pill, rbCaja.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        }

        if (usado)
        {
            transform.parent.GetComponent<MeshRenderer>().material = materialFinal;
        }
    }
}
