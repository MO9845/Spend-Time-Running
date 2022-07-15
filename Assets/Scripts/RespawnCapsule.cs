using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RespawnCapsule : MonoBehaviour
{
    private Player player;

    void Start()
    {
        StartCoroutine(Disable());
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 1, 0);
        GetComponent<Renderer>().material.color = new Vector4(1, 1, 0, Mathf.Sin(Time.frameCount * 0.03f) / 4.0f + 0.25f);
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
