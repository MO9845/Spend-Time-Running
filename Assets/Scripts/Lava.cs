//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            player.Die();
    }
}
