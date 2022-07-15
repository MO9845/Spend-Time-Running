//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainWidget : MonoBehaviour
{
    public float bonusY;
    public static MainWidget instance;

    private Player player;
    public TextMeshProUGUI coins;
    public GameObject bonusList, bonusWidget;

    void Start()
    {
        instance = this;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        coins.text = player.coins.ToString();
    }

    public void AddBonus()
    {
        Instantiate(bonusWidget, bonusList.transform);
    }
}
