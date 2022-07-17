using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isGrounded = false;

    private void OnEnable()
    {
        CancelInvoke("LifeRoutine");
        Invoke("LifeRoutine", 5f);
    }

    void Update()
    {
        if (isGrounded) transform.Rotate(0.0f, 1.0f, 0.0f, Space.World);
    }

    private void LifeRoutine()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
