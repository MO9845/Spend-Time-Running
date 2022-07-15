using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class StartBonuses : MonoBehaviour
{
    private Player player;
    public GameObject respawnCapsule, boostBonus;

    void Start()
    {
        StartCoroutine(DisableBonuses());
        player = FindObjectOfType<Player>();
    }

    public void Boost()
    {
        if (player.coins >= 100)
        {
            Instantiate(respawnCapsule, player.transform.position, Quaternion.identity);
            boostBonus.SetActive(false);

            player.rigidbody.AddForce(new Vector3(0, 1000, 2000));
            player.coins -= 100;
        }
    }

    IEnumerator DisableBonuses()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
