//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenuWidget : MonoBehaviour
{
    private int characterIndex;

    public GameObject[] characters;

    void Start()
    {
        string path = Application.persistentDataPath + "/character.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            characterIndex = (int)formatter.Deserialize(stream);
            foreach (GameObject character in characters)
                character.transform.position += new Vector3(characterIndex * 10, 0, 0);

            stream.Close();
        }
    }

    public void PlayGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/character.save";

        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, characterIndex);
        stream.Close();

        SceneManager.LoadScene("Gameplay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PrecCharacter()
    {
        characterIndex--;

        if (characterIndex < 0)
            characterIndex = 0;
        else
            foreach (GameObject character in characters)
                character.transform.position -= new Vector3(10, 0, 0);
    }

    public void NextCharacter()
    {
        characterIndex++;
        if (characterIndex > 1)
            characterIndex = 1;
        else
            foreach (GameObject character in characters)
                character.transform.position += new Vector3(10, 0, 0);
    }
}
