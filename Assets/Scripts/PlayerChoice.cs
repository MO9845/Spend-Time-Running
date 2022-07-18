//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerChoice : MonoBehaviour
{
    void Start()
    {
        string path = Application.persistentDataPath + "/character.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int characterIndex = (int)formatter.Deserialize(stream);

            switch (characterIndex)
            {
                case 0:
                    if (transform.name != "Vanguard")
                        gameObject.SetActive(false);
                    break;

                case 1:
                    if (transform.name != "Ely")
                        gameObject.SetActive(false);
                    break;
            }

            stream.Close();
        }
    }
}
