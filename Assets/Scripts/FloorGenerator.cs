//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    private float floorZ;

    public GameObject floor;

    void Start()
    {
        for (int i = 0; i < 10; i++)
            SpawnFloor();
    }

    public void SpawnFloor()
    {
        Instantiate(floor, new Vector3(0, 0, floorZ), Quaternion.identity);
        floorZ += 20;
    }
}
