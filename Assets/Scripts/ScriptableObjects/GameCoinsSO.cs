using System;
using UnityEngine;


[CreateAssetMenu(fileName = "GameCoins", menuName = "Scriptables/GameCoins")]
public class GameCoinsSO : ScriptableObject
{
    [SerializeField] private int coins;


    public int Coins
    {
        get => coins;
        set
        {
            coins = value;
            OnCoinsValueChange?.Invoke(coins);
        }
    }

    public event Action<int> OnCoinsValueChange;
}
