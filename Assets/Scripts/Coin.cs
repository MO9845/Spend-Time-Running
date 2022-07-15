using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Coin : MonoBehaviour
{
    private Player player;
    public AudioSource coin;

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
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(PlayCoinSound());

            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/coins.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, FindObjectOfType<Player>().coins);
            stream.Close();
        }
    }

    IEnumerator PlayCoinSound()
    {
        if (GetComponent<Renderer>().enabled)
        {
            GetComponent<Renderer>().enabled = false;
            player.coins += player.coinMultiplier;

            coin.pitch = Random.Range(0.95f, 1.05f);
            coin.Play();

            yield return new WaitForSeconds(1);

            Destroy(gameObject);
        }
    }
}
