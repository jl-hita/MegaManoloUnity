using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Windows;

public class scPlataformaEspecial : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float velocidad;
    //Puntos en coordenada X donde hace tope
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    //Va hacia derecha?
    [SerializeField] private Boolean haciaDerecha;
    private Vector3 posInicial;
    private Vector3 posFinal;
    //Segundos que espera al llegar a uno de los destinos
    private const float tiempoParadaMax = 1.5f;
    private float tiempoParada;
    private Boolean enMovimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posInicial = new Vector3(xMin, transform.position.y, transform.position.z);
        posFinal = new Vector3(xMax, transform.position.y, transform.position.z);
        tiempoParada = tiempoParadaMax;
        enMovimiento = true;
    }

    void FixedUpdate()
    {
        //Si se marca el flag enMovimiento como falso se hace una parada desde tiempoParadaMax hasta 0
        if (!enMovimiento)
        {
            tiempoParada -= Time.deltaTime;
            if (tiempoParada < 0)
            {
                tiempoParada = tiempoParadaMax;
                enMovimiento = true;
            }
        } else
        {
            //Si el flag enMovimiento es true se mueve la plataforma en la dirección que toque
            if (!haciaDerecha)
            {
                if (transform.position.x > xMin)
                    rb.MovePosition(Vector3.MoveTowards(transform.position, posInicial, velocidad));
                else
                {
                    enMovimiento = false;
                    haciaDerecha = true;
                }

            }
            else
            {
                if (transform.position.x < xMax)
                    rb.MovePosition(Vector3.MoveTowards(transform.position, posFinal, velocidad));
                else
                {
                    enMovimiento = false;
                    haciaDerecha = false;
                }
            }
        }
    }
}
