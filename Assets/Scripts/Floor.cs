
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public static float trucksZ = 0;
    public static float lavaZ = 0;

    public GameObject car, truck, pillar, panel, coin, doubleCoins, lavaFloor, platform;
    public MovingVehicle movingTruck, movingCar;

    void Start()
    {
        if (transform.position.z == 0)
            return;

        bool trucks = false;
        bool lava = false;

        if (transform.position.z > trucksZ + 20 && transform.position.z < trucksZ + 120)
            trucks = true;
        else if (transform.position.z > trucksZ + 120)
            trucksZ += 120;

        if (transform.position.z > lavaZ + 160 && transform.position.z < lavaZ + 260)
        {
            lava = true;
            trucks = false;
        }
        else if (transform.position.z > lavaZ + 260)
            lavaZ += 160;

        int truckCount = 0;

        if (trucks)
        {
            Instantiate(pillar, new Vector3(0, 4, transform.position.z), Quaternion.identity);

            Instantiate(coin, new Vector3(0, 1, transform.position.z + 4), Quaternion.identity);
            Instantiate(coin, new Vector3(0, 1, transform.position.z + 6), Quaternion.identity);
            Instantiate(coin, new Vector3(0, 1, transform.position.z + 8), Quaternion.identity);
            Instantiate(coin, new Vector3(0, 1, transform.position.z + 10), Quaternion.identity);


            float x = Random.Range(0, 2);
            switch (x)
            {
                case 0:
                    x = -4.7f;
                    break;

                case 1:
                    x = 4.7f;
                    break;
            }

            if (Random.Range(0, 2) == 0)
                Instantiate(movingTruck, new Vector3(x, 1.5f, transform.position.z + 10), Quaternion.Euler(0, 180, 0));
            else
                Instantiate(movingCar, new Vector3(x, 1, transform.position.z + 10), Quaternion.Euler(0, 180, 0));
        }
        else if (lava)
        {
            Instantiate(lavaFloor, transform.position, Quaternion.identity);

            float x = Random.Range(-1, 2) * 4.7f;
            Instantiate(platform, transform.position + new Vector3(x, 1, 0), Quaternion.identity);

            Instantiate(coin, transform.position + new Vector3(x, 2, -4), Quaternion.identity);
            Instantiate(coin, transform.position + new Vector3(x, 2, 0), Quaternion.identity);
            Instantiate(coin, transform.position + new Vector3(x, 2, 4), Quaternion.identity);

            if (Random.Range(0, 4) == 0)
                Instantiate(doubleCoins, transform.position + new Vector3(x, 2, 6), Quaternion.identity);

            Destroy(gameObject);
        }
        else
            for (float x = -4.7f; x <= 4.7f; x += 4.7f)
            {
                int type = Random.Range(0, 3);

                switch (type)
                {
                    case 0:
                        Instantiate(car, new Vector3(x, 0.75f, transform.position.z), Quaternion.Euler(0, 180, 0));
                        Instantiate(coin, new Vector3(x, 2.25f, transform.position.z + 0.5f), Quaternion.identity);

                        break;

                    case 1:
                        if (truckCount != 2)
                        {
                            Instantiate(truck, new Vector3(x, 1.5f, transform.position.z), Quaternion.Euler(0, 180, 0));
                            truckCount++;
                        }

                        break;

                    case 2:
                        Instantiate(panel, new Vector3(x, 0, transform.position.z), Quaternion.identity);
                        Instantiate(coin, new Vector3(x, 0.5f, transform.position.z), Quaternion.identity);
                        break;
                }
            }
    }
}
