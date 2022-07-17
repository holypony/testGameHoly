using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : PoolBase<Coin>
{
    [SerializeField] private Coin CoinPrefab;
    private List<Coin> Coins;

    private void Awake()
    {
        Coins = InitPool(CoinPrefab, 15);
        StartCoroutine(CoinRain());
    }

    private IEnumerator CoinRain()
    {
        while (true)
        {
            Get(Coins, RandomPos(), Quaternion.identity);
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }

    private Vector3 RandomPos()
    {
        Vector3 randomPos = new Vector3(Random.Range(-12f, 12f), 8f, Random.Range(-12f, 12f));
        return randomPos;
    }
}
