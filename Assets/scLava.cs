using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class scLava : MonoBehaviour
{
    private static float x;
    private static float xInicial;
    private static float yInicial;
    private static float zInicial;
    [SerializeField] GameObject jugador;
    void Start()
    {
        xInicial = transform.position.x;
        yInicial = transform.position.y;
        zInicial = transform.position.z;
        x = xInicial;
    }

    void FixedUpdate()
    {
        //Hacemos mover el modelo para que de la ilusión de un rio de lava
        transform.position = new Vector3(x, yInicial, zInicial);
        x = x - 0.015f;
        if(x < xInicial-10)
            x = xInicial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Cabeza"))
            //Si el player cae en la lava le indicamos a scJuego que debe matar al pj con el flag muere
            scJuego.muere = true;
        else if(other.name.Equals("BolaFuego"))
        {
            Destroy(gameObject);
            scFisicas.totalBolas--;
        }
    }
}
