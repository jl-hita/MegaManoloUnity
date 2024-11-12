using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scFollowPlayer : MonoBehaviour
{
    public static GameObject player;

    void Start()
    {
    }

    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z - 20);
    }
}
