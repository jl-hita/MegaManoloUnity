using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scLoadMundo1 : MonoBehaviour
{
    void Onclick()
    {
        Debug.Log("Cargando mundo 1");
        SceneManager.LoadScene(sceneBuildIndex: 1, LoadSceneMode.Single);
    }
}
