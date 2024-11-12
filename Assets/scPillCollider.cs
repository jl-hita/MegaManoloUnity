using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scPillCollider : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Contains("Cube"))
            scPill.movimiento = scPill.movimiento * -1;
    }
}
