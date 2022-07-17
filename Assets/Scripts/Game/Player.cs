using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent _agent;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(PlayerRoutine());
    }

    private IEnumerator PlayerRoutine()
    {
        _agent.SetDestination(RandomPos());

        while (true)
        {
            if (_agent.remainingDistance < 0.1f)
            {
                _agent.SetDestination(RandomPos());
            }
            yield return new WaitForSecondsRealtime(0.25f);
        }

    }

    private Vector3 RandomPos()
    {
        Vector3 randomPos = new Vector3(Random.Range(-12f, 12f), 0f, Random.Range(-12f, 12f));
        return randomPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Contact Coin");

        }
    }
}
