//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class MovingVehicle : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player.transform.position.z > transform.position.z - 20)
            transform.Translate(new Vector3(0, 0, player.speed * Time.deltaTime));
    }
}
