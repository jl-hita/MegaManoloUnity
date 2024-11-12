using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scMenuPrincipal : MonoBehaviour
{
    public static CanvasGroup cgMenuPrincipal;
    public Boolean apagarMain = false;

    void Start()
    {
    }
    public void OnclickStart()
    {
        Debug.Log("Cargando mundo 1");
        SceneManager.LoadScene(sceneBuildIndex: 1, LoadSceneMode.Single);
    }

    public void OnclickRanking()
    {
        Debug.Log("Click: Ranking");
    }

    public void OnclickSettings()
    {
    }

    public void OnclickExit()
    {
        Debug.Log("Click: Exit");
        Application.Quit();
    }
}
