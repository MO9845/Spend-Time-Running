//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainWidget : MonoBehaviour
{
    public static MainWidget instance;

    private Player player;

    public TextMeshProUGUI coins;
    public GameObject bonusesList, bonusWidget;

    void Start()
    {
        player = FindObjectOfType<Player>();
        instance = this;
    }

    void Update()
    {
        coins.text = player.coins.ToString();
    }

    public void AddBonus()
    {
        Instantiate(bonusWidget, bonusesList.transform);
    }
}
