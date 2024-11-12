using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scRanking : MonoBehaviour
{
    private Button bVolver;
    public static CanvasGroup cgRanking;
    private List<int> clasiPuntos;
    private List<string> clasiNames;
    [SerializeField] private TMP_Text name1;
    [SerializeField] private TMP_Text name2;
    [SerializeField] private TMP_Text name3;
    [SerializeField] private TMP_Text name4;
    [SerializeField] private TMP_Text name5;
    [SerializeField] private TMP_Text clasi1;
    [SerializeField] private TMP_Text clasi2;
    [SerializeField] private TMP_Text clasi3;
    [SerializeField] private TMP_Text clasi4;
    [SerializeField] private TMP_Text clasi5;
    public static Boolean addScore = false;

    private void Start()
    {
        RecuperarRanking();
        MostrarRanking();
    }

    public void RecuperarRanking()
    {
        clasiPuntos = new List<int>();
        clasiNames = new List<string>();

        //Recuperamos la clasificación de PlayerPrefs, ponemos puntos a 0 y nombre a Manolo por defecto
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("i: " + i.ToString());
            clasiPuntos.Add(PlayerPrefs.GetInt(i.ToString(), 0));
            clasiNames.Add(PlayerPrefs.GetString("name"+i.ToString(), "Manolo"));
        }
    }

    public void MostrarRanking()
    {
        name1.text = clasiNames[0];
        name2.text = clasiNames[1];
        name3.text = clasiNames[2];
        name4.text = clasiNames[3];
        name5.text = clasiNames[4];
        clasi1.text = clasiPuntos[0].ToString();
        clasi2.text = clasiPuntos[1].ToString();
        clasi3.text = clasiPuntos[2].ToString();
        clasi4.text = clasiPuntos[3].ToString();
        clasi5.text = clasiPuntos[4].ToString();
    }

    public void InsertarPuntuacion()
    {
        //Recuperamos nombre y puntuación de la última partida
        int puntos = PlayerPrefs.GetInt("finalScore", 0);
        string nombre = PlayerPrefs.GetString("name", "Manolo");
        
        //Vamos desde el final del Ranking buscando dónde colocar la puntuación nueva
        for (int i = 4; i >= 0; i--)
        {
            if (i > 0)
            {
                //Si no es el último puesto hay que mover las puntuaciones superiores hacia abajo
                if (puntos > clasiPuntos[i-1])
                {
                    clasiNames[i] = clasiNames[i-1];
                    PlayerPrefs.SetString("name"+i, clasiNames[i-1]);
                    clasiPuntos[i] = clasiPuntos[i-1];
                    PlayerPrefs.SetInt(i.ToString(), clasiPuntos[i-1]);
                } else if(puntos > clasiPuntos[i])
                { // Si encuentra su sitio antes de llegar al primer puesto
                    clasiNames[i] = nombre;
                    PlayerPrefs.SetString("name" + i, clasiNames[i]);
                    clasiPuntos[i] = puntos;
                    PlayerPrefs.SetInt(i.ToString(), clasiPuntos[i]);
                }
            } else if (puntos > clasiPuntos[i])
            { //Si es la primera puntuación
                clasiNames[i] = nombre;
                PlayerPrefs.SetString("name" + i, clasiNames[i]);
                clasiPuntos[i] = puntos;
                PlayerPrefs.SetInt(i.ToString(), clasiPuntos[i]);
            }
        }
    }

    private void OnclickVolver()
    {
        //Hacer que no se pueda interactuar con este canvas
        cgRanking.interactable = false;
        //Hacer invisible este canvas
        cgRanking.alpha = 0.0f;
        //Hacer interactuable el canvas con el menu principal
        scMenuPrincipal.cgMenuPrincipal.interactable = true;
        //Hacer visible el canvas con el menu principal
        scMenuPrincipal.cgMenuPrincipal.alpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Si el flag es true es porque se ha finalizado una partida
        if (addScore)
        {
            InsertarPuntuacion();
            MostrarRanking();
            addScore = false;
        }
    }
}
