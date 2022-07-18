using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RespawnCapsule : MonoBehaviour
{
    private Player player;

    private float alpha;
    private bool incrementAlpha;

    void Start()
    {
        StartCoroutine(Disable());

        incrementAlpha = true;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        alpha += incrementAlpha ? Time.deltaTime : Time.deltaTime * -1;
        if (alpha >= 0.5f)
            incrementAlpha = false;
        else if (alpha <= 0)
            incrementAlpha = true;

        transform.position = player.transform.position + new Vector3(0, 1, 0);
        GetComponent<Renderer>().material.color = new Vector4(1, 1, 0, alpha);
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
