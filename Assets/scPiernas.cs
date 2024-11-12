using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scPiernas : MonoBehaviour
{
    AudioSource sonido;
    void Start()
    {
        sonido = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    /*
     * Los siguientes triggers controlan si Manolo está apoyado sobre una superficie (suelo, cajas, enemigo, etc)
     */
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Escenario"))
            scFisicas.enSuelo = true;

        if (other.gameObject.CompareTag("Enemy"))
        {
            //Hacemos que el player rebote
            scFisicas.rebotaHaciaArriba = true;
            //Destruimos el enemigo
            Destroy(other.transform.parent.gameObject);
            //Sumamos puntos por eliminar a un enemigo
            scJuego.puntuacion += scJuego.puntosEnemigo;
            sonido.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Escenario"))
            scFisicas.enSuelo = false;
    }
}
