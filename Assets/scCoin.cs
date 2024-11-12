using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scCoin : MonoBehaviour
{
    private Rigidbody rb;
    private Boolean puntuado;
    AudioSource sonido;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        puntuado = false;
        sonido = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(0f, 3f, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            rb.AddForce(transform.right * 100);
            Destroy(this.gameObject, 1);
            sonido.Play();

            //Sumamos puntos por conseguir una moneda
            if (!puntuado)
            {
                scJuego.puntuacion += scJuego.puntosCoin;
                puntuado = true;
            }
        }
    }
}
