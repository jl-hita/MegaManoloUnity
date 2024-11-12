using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using Unity.UI;
using UnityEngine.UI;

public class scSettings : MonoBehaviour
{
    private string nombre;

    void Start()
    {
    }

    public void GuardarNombre(string name)
    {
        nombre = name;
    }

    public void OnChangeText(string text)
    {
        Debug.Log("Nombre: " + text);
    }

    public void ExitSettings()
    {
        Debug.Log("Guardando nombre: " + nombre);
        PlayerPrefs.SetString("name", nombre);
        PlayerPrefs.Save();
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt("Volume", 50);
        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        int volume = PlayerPrefs.GetInt("Volume", 0);
    }

    void Update()
    {
        
    }
}
