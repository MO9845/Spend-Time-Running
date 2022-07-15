// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            if (!player.rolling || !player.onGround)
            {
                player.hitObject = gameObject;
                player.Die();
            }
    }
}
