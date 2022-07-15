//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * 100, 0));        
    }
}
