using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scMeta : MonoBehaviour
{
    private Boolean llegaMeta;
    private Rigidbody rbJugador;
    AudioSource fin;

    void Start()
    {
        llegaMeta = false;
        fin = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(llegaMeta)
        {
            fin.Play();
            float delay = 60f;
            while(delay > 0)
                delay -= Time.deltaTime;
            //Suma puntos por tiempo restante y por vidas restantes
            scJuego.puntuacion += scJuego.vidas * scJuego.puntosVida;
            scJuego.puntuacion += (int)Math.Round(scJuego.tiempo) * scJuego.puntosSegundo;
            //Se guarda la puntuación en PlayerPrefs
            PlayerPrefs.SetInt("finalScore", scJuego.puntuacion);
            //Nos aseguramos que scRanking guarde la nueva puntuacion con el flag addScore
            scRanking.addScore = true;
            //Vuelve al menú principal
            SceneManager.LoadScene(sceneBuildIndex: 0, LoadSceneMode.Single);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            llegaMeta = true;
    }
}
