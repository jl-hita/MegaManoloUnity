using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scCube_Breakable : MonoBehaviour
{
    
    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Contains("Cabeza"))
        {
            scJuego.hit = true;
            Destroy(transform.parent.gameObject);
        }
    }
}
