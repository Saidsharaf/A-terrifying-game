using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("Player collided with enemy!");
            Destroy(other.gameObject); // Destroy the player GameObject
            Destroy(gameObject); // Destroy the player GameObject

        }

    }



}
