using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scCheckpoint : MonoBehaviour
{
    private static Boolean checkpointActivado=false;
    [SerializeField] private Material banderaActivada;
    AudioSource check;

    void Start()
    {
        check = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!checkpointActivado)
        {
            if (GameObject.FindWithTag("Player").GetComponent<Transform>().transform.position.x >= transform.position.x)
            {
                check.Play();
                scFisicas.spawnActual = new Vector3(transform.position.x, 0.6f, 0f);
                checkpointActivado = true;
                transform.GetChild(0).GetComponent<MeshRenderer>().material = banderaActivada;
            }
        }   
    }
}
