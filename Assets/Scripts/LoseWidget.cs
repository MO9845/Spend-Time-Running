//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoseWidget : MonoBehaviour
{
    private Player player;

    public TextMeshProUGUI coinsNeededText, distanceText, bestDistanceText;

    private int coinsNeeded;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        coinsNeeded = 25;
    }

    public void RestartGame()
    {
        Floor.trucksZ = 0;
        Floor.lavaZ = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Respawn()
    {
        if (player.coins > coinsNeeded)
        {
            player.Respawn();
            player.coins -= coinsNeeded;

            coinsNeeded *= 2;
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void SaveDistance()
    {
        int playerDistance = (int)player.transform.position.z;
        distanceText.text = "Distance : " + playerDistance.ToString() + " mètres";
        bestDistanceText.text = "Meilleure distance : " + playerDistance + " mètres";

        string path = Application.persistentDataPath + "/distance.save";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;

        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.OpenOrCreate);
            int savedDistance = (int)formatter.Deserialize(stream);

            if (playerDistance > savedDistance)
            {
                bestDistanceText.text = "Meilleure distance : " + playerDistance + " mètres";

                stream.Close();
                stream = new FileStream(path, FileMode.Create);
                formatter.Serialize(stream, playerDistance);
            }
            else
                bestDistanceText.text = "Meilleure distance : " + savedDistance + " mètres";

            formatter.Serialize(stream, playerDistance);
        }
        else
        {
            stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, playerDistance);
        }

        stream.Close();
    }

    public void SetCoinsNeededText()
    {
        coinsNeededText.text = coinsNeeded.ToString();
        SaveDistance();
    }
}
