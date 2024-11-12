using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scPill : MonoBehaviour
{
    private Rigidbody rb;
    public static float movimiento;
    [SerializeField]
    private Material pantalonRojo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movimiento = 0.04f;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movimiento, 0, 0);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            scJuego.buff = true;
            other.transform.GetChild(0).GetComponent<MeshRenderer>().material = pantalonRojo;
            scFisicas.esMegaManolo = true;
            Destroy(this.gameObject);
        }
    }
}
