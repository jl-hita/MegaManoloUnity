using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class scJuego : MonoBehaviour
{
    //Nombre del jugador
    public static string nombrePlayer;
    //Vidas totales del player
    public static int vidas;
    //Puntuación total
    public static int puntuacion;
    //Tiempo actual
    public static float tiempo;
    //Tiempo disponible al comenzar un mapa / perder una vida
    public const float tiempoInicial = 400;
    //Puntos ganados al tomar una moneda
    public const int puntosCoin = 100;
    //Puntos ganados al eliminar a un enemigo
    public const int puntosEnemigo = 1000;
    //Puntos ganados por cada segundo restante al llegar a meta
    public const int puntosSegundo = 20;
    //Puntos ganados por cada vida restante al llegar a meta
    public const int puntosVida = 2000;
    //Flag que indica que el player muere
    public static Boolean muere;
    //Lista de objetos instanciados
    List<GameObject> instancias = new List<GameObject>();
    [SerializeField] GameObject jugador;
    [SerializeField] GameObject cube_breakable;
    [SerializeField] GameObject cube_coin;
    [SerializeField] GameObject cube_pill;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject enemy_basic;
    [SerializeField] GameObject enemy_chaser;
    //GameObjects del HUD
    //[SerializeField] TextMeshPro HUDVidas;
    //[SerializeField] TextMeshPro HUDPuntos;
    private TMP_Text HUDNombre;
    private TMP_Text HUDVidas;
    private TMP_Text HUDPuntos;
    //Sonidos
    AudioSource[] sonidos;
    public static Boolean hit;
    public static Boolean buff;

    void Start()
    {
        HUDNombre = GameObject.Find("HUDNombre").GetComponent<TMP_Text>();
        HUDVidas = GameObject.Find("HUDVidas").GetComponent<TMP_Text>();
        HUDPuntos = GameObject.Find("HUDPuntos").GetComponent<TMP_Text>();
        //Si el player nunca ha cambiado el nombre en Settings se le asigna Manolo por defecto
        HUDNombre.text = PlayerPrefs.GetString("name", "Manolo");
        vidas = 2;
        //Nombre por defecto
        nombrePlayer = "Manolo";
        sonidos = GetComponents<AudioSource>();
        muere = false;
        hit = false;
        buff = false;
        this.Reset();
    }

    private void Update()
    {
        HUDPuntos.text = puntuacion.ToString();
        HUDVidas.text = vidas.ToString();
        //Si se ha marcado el flag muere es porque hay que matar al player
        if (muere)
        {
            //Ponemos el flag muere a false
            muere = false;
            perderVida(jugador);
        }

        if (hit)
        {
            sonidos[2].Play();
            hit = false;
        }

        if (buff)
        {
            sonidos[3].Play();
            buff = false;
        }
    }

    void FixedUpdate()
    {
        //Timer
        if (tiempo > 0)
            tiempo -= Time.deltaTime;
        else //Cuando el tiempo termina el jugador pierde una vida
            perderVida(jugador);
    }

    private void perderVida(GameObject pj)
    {
        //Reproducimos el sonido de muerte
        sonidos[0].Play();
        //El jugador pierde una vida
        vidas--;
        Debug.Log("Vidas: " + vidas);

        //Si el jugador pierde todas las vidas se activa la rutina para terminar la partida
        if (vidas == 0)
            PierdePartida();

        //Eliminamos todos los GameObject instanciados
        foreach (GameObject go in instancias)
            Destroy(go);
        //Destruye una pill si no se ha cogido antes de morir
        GameObject gOTemp = GameObject.Find("Pildora(Clone)"); ;
        if (gOTemp!=null)
            Destroy(gOTemp);
        //Reseteamos el mundo
        this.Reset();
    }

    private void PierdePartida()
    {
        //Suma puntos por tiempo restante y por vidas restantes
        puntuacion += vidas * puntosVida;
        puntuacion += (int)Math.Round(tiempo) * puntosSegundo;
        //Se guarda la puntuación en PlayerPrefs
        PlayerPrefs.SetInt("finalScore", puntuacion);
        //Nos aseguramos que scRanking guarde la nueva puntuacion con el flag addScore
        scRanking.addScore = true;
        //Vuelve al menú principal
        SceneManager.LoadScene(sceneBuildIndex: 0, LoadSceneMode.Single);
    }

    //Resetea el mundo
    public void Reset()
    {
        //Resetea el player (Recordar quitar el instantiate del método perder vida y llamar a este)
        instancias.Add(Instantiate(jugador, scFisicas.spawnActual, Quaternion.identity));
        //Hacemos que la cámara siga el nuevo GameObject del player
        scFollowPlayer.player = jugador;
        //Reiniciamos el timer
        tiempo = tiempoInicial;
        //Reseteamos la puntuación
        puntuacion = 0;

        //Resetea las Cube_breakable
        instancias.Add(Instantiate(cube_breakable, new Vector3(3f, 2.5f, 0f), Quaternion.identity));
        instancias.Add(Instantiate(cube_breakable, new Vector3(7.5f, 1.2f, 0f), Quaternion.identity));
        instancias.Add(Instantiate(cube_breakable, new Vector3(7.5f, 0.4f, 0f), Quaternion.identity));
        instancias.Add(Instantiate(cube_breakable, new Vector3(16f, 1.2f, 0f), Quaternion.identity));
        instancias.Add(Instantiate(cube_breakable, new Vector3(16f, 0.4f, 0f), Quaternion.identity));

        //Resetea las Cube_coin
        instancias.Add(Instantiate(cube_coin, new Vector3(3.8f, 2.5f, 0f), Quaternion.identity));
        instancias.Last<GameObject>().transform.GetChild(0).GetComponent<scCube_Coin>().SetUsos(1);
        instancias.Add(Instantiate(cube_coin, new Vector3(4.6f, 2.5f, 0f), Quaternion.identity));
        instancias.Last<GameObject>().transform.GetChild(0).GetComponent<scCube_Coin>().SetUsos(5);

        //Resetea las Cube_pill
        instancias.Add(Instantiate(cube_pill, new Vector3(11.6f, 4.8f, 0f), Quaternion.identity));

        //Resetea los Coin
        instancias.Add(Instantiate(coin, new Vector3(1f, 0.4f, 0f), Quaternion.identity));
        instancias.Last<GameObject>().transform.Rotate(0f, 0f, 90f);
        instancias.Add(Instantiate(coin, new Vector3(10f, 3.2f, 0f), Quaternion.identity));
        instancias.Last<GameObject>().transform.Rotate(0f, 0f, 90f);
        instancias.Add(Instantiate(coin, new Vector3(10.8f, 3.2f, 0f), Quaternion.identity));
        instancias.Last<GameObject>().transform.Rotate(0f, 0f, 90f);
        instancias.Add(Instantiate(coin, new Vector3(11.6f, 3.2f, 0f), Quaternion.identity));
        instancias.Last<GameObject>().transform.Rotate(0f, 0f, 90f);
        
        //Resetea Los enemigos
        instancias.Add(Instantiate(enemy_basic, new Vector3(11.6f, 0.55f, 0f), Quaternion.identity));
        instancias.Last<GameObject>().transform.Rotate(new Vector3(0f, 45f, 0f));
        instancias.Last<GameObject>().GetComponent<scEnemigoBasico>().Set(8.5f, 15f);

        instancias.Add(Instantiate(enemy_chaser, new Vector3(50f, 0.55f, 0f), Quaternion.identity));
        instancias.Last<GameObject>().transform.Rotate(new Vector3(0f, 45f, 0f));

        //Reproduce la música de fondo
        sonidos[1].Play();
    }
}
