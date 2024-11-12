using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scFireballDerecha : MonoBehaviour
{
    private Rigidbody rb;
    public static float movimiento;
    private GameObject player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Detectamos si la bola va hacia la derecha o la izquierda
        if (player.transform.position.x - transform.position.x > 0)
            movimiento = -0.2f;
        else
            movimiento = 0.2f;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movimiento, 0, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        //Hacemos sonar el sonido hit
        scJuego.hit = true;
        //Gestionamos con condicionales el comportamiento según cada objeto con el que puede chocar
        if (other.transform.name.Contains("breakable"))
        {
            Destroy(other.gameObject);
            scFisicas.totalBolas--;
            Destroy(this.gameObject);
        }
        else if (other.transform.name.Contains("Enemy"))
        {
            Destroy(other.gameObject);
            scFisicas.totalBolas--;
            Destroy(this.gameObject);
            //Sumamos puntos por eliminar a un enemigo
            scJuego.puntuacion += scJuego.puntosEnemigo;
        }
        else if(!other.transform.name.Contains("Suelo"))
        {
            scFisicas.totalBolas--;
            Destroy(this.gameObject);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Equals("Lava"))
        {
            scFisicas.totalBolas--;
            Destroy(this.gameObject);
        }
    }
}
