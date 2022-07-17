using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollisionHandler : MonoBehaviour
{
    public GameManager gm;
    public TMP_Text textCoins;
    private int coins = 0;
    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Coin"))
        {
            textCoins.text = "Coins: " + coins++;
        }
    }

}
