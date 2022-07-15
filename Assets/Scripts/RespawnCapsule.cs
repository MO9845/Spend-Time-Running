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
        GetComponent<Renderer>().material.color = new Vector4(1, 1, 0, Mathf.Clamp(Mathf.Sin(Time.frameCount * 0.01f), 0, 0.5f));
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
