using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scEnemigoBasico : MonoBehaviour
{
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    private float velocidad;
    private Boolean miraDerecha;
    [SerializeField] GameObject jugador;
    private Rigidbody rb;
    
    void Start()
    {
        velocidad = 0.07f;
        rb = GetComponent<Rigidbody>();
        miraDerecha = false;
    }

    public void Set(float xMinimo, float xMaximo)
    {
        this.xMin = xMinimo;
        this.xMax = xMaximo;
    }

    void Update()
    {
        //Gira a derecha e izquierda al enemigo en un rango determinado en el eje X
        if(miraDerecha)
        {
            if (transform.position.x > xMax)
            {
                transform/*.GetChild(0)*/.Rotate(new Vector3(0.0f, 110f, 0.0f));
                miraDerecha = false;
            }
        } else
        {
            if (transform.position.x < xMin)
            {
                transform/*.GetChild(0)*/.Rotate(new Vector3(0.0f, -110f, 0.0f));
                miraDerecha = true;
            }   
        }
    }

    private void FixedUpdate()
    {
        if (miraDerecha)
            transform.position = new Vector3(transform.position.x + velocidad, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x - velocidad, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        //Si el enemigo toca al player le indicamos a scJuego que debe matar al pj con el flag muere
        if (other.gameObject.tag == "Player")
            scJuego.muere = true;
    }
}
