using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scCube_Coin : MonoBehaviour
{
    public GameObject coin;
    private Rigidbody rbCaja;
    [SerializeField] private int usos;
    [SerializeField] Material materialFinal;
    AudioSource sonido;

    void Start()
    {
        rbCaja = transform.parent.GetComponent<Rigidbody>();
        sonido = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void SetUsos(int u)
    {
        this.usos = u;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Contains("Cabeza") && usos>0)
        {
            sonido.Play();
            usos --;
            Instantiate(coin, rbCaja.transform.position + new Vector3(0f, 1f, 0f), Quaternion.Euler(0,0,90) /*Quaternion.identity*/);
            //Sumamos puntos por conseguir una moneda
            scJuego.puntuacion += scJuego.puntosCoin;
        }
        if(usos==0)
            transform.parent.GetComponent<MeshRenderer>().material = materialFinal;
    }
}
