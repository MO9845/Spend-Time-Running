//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            if (FindObjectOfType<RespawnCapsule>() != null)
            {
                foreach (Collider collider in GetComponents<Collider>())
                    collider.enabled = false;
            }
            else
            {
                Player player = FindObjectOfType<Player>();

                player.hitObject = this.gameObject;
                player.Die();
            }
    }
}
