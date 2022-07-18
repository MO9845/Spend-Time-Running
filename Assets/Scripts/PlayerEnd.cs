//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerEnd : MonoBehaviour
{
    private Player player;
    private FloorGenerator floorGenerator;

    void Start()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

        player = FindObjectOfType<Player>();
        floorGenerator = FindObjectOfType<FloorGenerator>();
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            floorGenerator.SpawnFloor();
            player.speed = Mathf.Clamp(player.speed * 1.03f, 5, 10);
        }

        if (other.gameObject.tag != "Double Coins")
            Destroy(other.gameObject);
    }
}
