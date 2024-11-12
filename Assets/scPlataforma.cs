using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scPlataforma : MonoBehaviour
{
    private Rigidbody rb;
    public float velocidad;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //movimiento = 0.009f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(velocidad, 0, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        //Hacemos que las plataformas cambien de sentido al colisionar con otra plataforma o el escenario
        if (other.gameObject.CompareTag("Escenario"))
            velocidad = velocidad * -1;
    }

    private void OnCollisionStay(Collision other)
    {
        //Un modo cutre de que el player no se deslice de la plataforma
        if (other.gameObject.CompareTag("Player"))
            other.transform.position = new Vector3(other.transform.position.x + (velocidad * 3f), other.transform.position.y, other.transform.position.z);
    }
}
