using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class DoubleCoins : MonoBehaviour
{
    private Player player;
    public AudioSource doubleCoins;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * 100, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            if (GetComponent<Renderer>().enabled)
            {
                MainWidget.instance.bonusY += 10;
                MainWidget.instance.AddBonus();

                GetComponent<Renderer>().enabled = false;
                player.coinMultiplier *= 2;

                doubleCoins.pitch = Random.Range(0.95f, 1.05f);
                doubleCoins.Play();

                StartCoroutine(ResetCoinMultiplier());
            }
    }

    IEnumerator ResetCoinMultiplier()
    {
        yield return new WaitForSeconds(10);

        player.coinMultiplier /= 2;
        MainWidget.instance.bonusY -= 10;

        Destroy(gameObject);
    }
}
