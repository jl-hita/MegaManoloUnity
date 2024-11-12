using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class scFisicas : MonoBehaviour
{
    public GameObject manolo;
    private Rigidbody manoloRB;
    private const float fuerzaSalto=4750;
    private const float fuerzaCorrer=1000;
    //Para diferenciar si player está tocando el suelo o se sustenta en el escenario
    public static Boolean enSuelo;
    private static Vector3 spawnInicial = new Vector3(-3f, 0.55f, 0f);
    //private static Vector3 spawnInicial = new Vector3(17.1f, 0.6f, 0f);
    public static Vector3 spawnActual = spawnInicial;
    private static Vector3 checkpoint;
    private GameObject piernas;
    private static Boolean miraDerecha;
    //Flag para marcar al player como MegaManolo
    public static Boolean esMegaManolo;
    //Flag para hacerle rebotar hacia arriba
    public static Boolean rebotaHaciaArriba;
    private static float tiempo;
    public static int totalBolas;
    //Constante que marca el máximo de bolas de ataque en pantalla
    private const int maxBolas = 2;
    [SerializeField]
    private GameObject bolaFuego;
    [SerializeField]
    private Material pantalonRojo;
    //Efectos de sonido y variables útiles
    AudioSource[] sonidos;
    private bool isWalking = false;
    private float timeSinceLastFootstep;
    

    void Start()
    {
        manoloRB = manolo.GetComponent<Rigidbody>();
        piernas = GameObject.Find("ColliderPiernas");
        //Controla cuando el player está en contacto con una superficie del escenario (suelo, cajas, etc)
        enSuelo = false;
        //Flag que controla si ha de rebotar hacia arriba
        rebotaHaciaArriba = false;
        //Controla si el player mira hacia la derecha
        miraDerecha = true;
        //Cambiar a true para salir como MegaManolo
        esMegaManolo = false;
        //Útil solo si el player sale ya como MegaManolo
        if(esMegaManolo)
            transform.GetChild(0).GetComponent<MeshRenderer>().material = pantalonRojo;
        //Cantidad actual de bolas de fuego en pantalla
        totalBolas = 0;

        //cesped = GetComponent<AudioSource>();
        sonidos = GetComponents<AudioSource>();
        timeSinceLastFootstep = Time.time;
    }

    void Update()
    {
        //Cuando apretamos la tecla de ataque (control izquierdo)
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //Controlamos que no hayan más que maxBolas en pantalla a la vez y que el player sea MegaManolo
            if (totalBolas<maxBolas && esMegaManolo)
            {
                if (miraDerecha)
                {
                    sonidos[2].Play();
                    //Instanciar bola de fuego que vaya a la derecha
                    Instantiate(bolaFuego, transform.position + new Vector3(0.4f, 0.5f, 0f), Quaternion.identity);
                }
                if (!miraDerecha)
                {
                    sonidos[2].Play();
                    //Instanciar bola de fuego que vaya a la izquierda
                    Instantiate(bolaFuego, transform.position + new Vector3(-0.4f, 0.5f, 0f), Quaternion.identity);
                }
                totalBolas++;
            }
        }

        //Para controlar efectos de sonido
        if (isWalking)
        {
            // Check if enough time has passed to play the next footstep sound
            if (Time.time - timeSinceLastFootstep >= 0.12f)
            {
                sonidos[0].Play();
                timeSinceLastFootstep = Time.time; // Update the time since the last footstep sound
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) | Input.GetKeyUp(KeyCode.RightArrow))
        {
            isWalking = false;
            Debug.Log("Walking false");
        }

        if (Input.GetKeyDown(KeyCode.Space) & enSuelo)
        {
            sonidos[1].Play();
        }
    }
    private void FixedUpdate()
    {
        //Si presiona escape sale al menú principal
        if(Input.GetKey(KeyCode.Escape))
        {
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

        //Salto: cuando apretamos la tecla de salto (espacio) mientras estamos en el suelo
        if (Input.GetKey(KeyCode.Space) &  enSuelo)
        {
            manoloRB.AddForce(transform.up * fuerzaSalto);
        }

        //Cambiar el if comentado dependiendo de si se quiere cambiar dirección en el aire como en los castlevania clásicos o no
        //if (Input.GetKey(KeyCode.RightArrow))
        if (Input.GetKey(KeyCode.RightArrow) & enSuelo)
        {
            if (!miraDerecha)
            {
                //Giramos la cara de Manolo para que mire en la dirección en la que se desplaza
                transform.GetChild(1).Rotate(new Vector3 (0.0f, -110f, 0.0f));
                miraDerecha = true;
            }
            manoloRB.AddForce(transform.right * fuerzaCorrer);
            isWalking = true;
            Debug.Log("Walking true");
        }

        //Cambiar el if comentado dependiendo de si se quiere cambiar dirección en el aire como en los castlevania clásicos o no
        //if (Input.GetKey(KeyCode.LeftArrow))
        if (Input.GetKey(KeyCode.LeftArrow) & enSuelo)
        {
            if (miraDerecha)
            {
                //Giramos la cara de Manolo para que mire en la dirección en la que se desplaza
                transform.GetChild(1).Rotate(new Vector3(0.0f, 110f, 0.0f));
                miraDerecha = false;
            }
            manoloRB.AddForce(transform.right * -fuerzaCorrer);
            isWalking = true;
            Debug.Log("Walking true");
        }


        if (rebotaHaciaArriba)
        {
            tiempo += Time.deltaTime;
            Debug.Log("tiempo: " + tiempo);
            if (tiempo < 0.1f)
                manoloRB.AddForce(transform.up * 5000f);
            else
            {
                tiempo = 0;
                rebotaHaciaArriba = false;
            }
        }
    }
}
