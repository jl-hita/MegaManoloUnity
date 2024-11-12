using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scEnemy_Chaser : MonoBehaviour
{
    public float fuerza = 12.0f;
    public float maxDist = 7.0f;
    public Transform target;
    private float distancia;
    private Boolean miraDerecha;

    void Start()
    {
        miraDerecha = false;
    }

    void Update()
    {
        if (target == null)
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        //Averigua la distancia entre el enemigo y el player
        distancia = target.position.x - transform.position.x;

        //Mira al player
        if (distancia >0)
        {
            if (!miraDerecha)
            {
                transform.Rotate(new Vector3(0.0f, -110f, 0.0f));
                miraDerecha = true;
            }
        } else
        {
            if (miraDerecha)
            {
                transform.Rotate(new Vector3(0.0f, 110f, 0.0f));
                miraDerecha = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //Si el player se acerca bastante el enemigo le persigue
        if (Math.Abs(distancia) < maxDist)
            transform.GetComponent<Rigidbody>().AddForce(new Vector2(fuerza * Math.Sign(distancia), 0f));
    }

    private void OnCollisionEnter(Collision other)
    {
        //Si el enemigo toca al player le indicamos a scJuego que debe matar al pj con el flag muere
        if (other.gameObject.tag == "Player")
            scJuego.muere = true;
    }
}
