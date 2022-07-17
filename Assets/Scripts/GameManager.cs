using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text textCoins;
    private int coins = 0;

    public void CoinPickUp()
    {
        textCoins.text = "Coins: " + coins++;
    }
}
