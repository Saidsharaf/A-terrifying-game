using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{

    public AudioSource src;
    public AudioClip sfx;

    private int countCoins = 0;
    public GameObject coins;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coins"))
        {
            Debug.Log("player have coins !");
            Destroy(other.gameObject); // Destroy the player GameObject
            sound();
            countCoins += 1;
            Debug.Log(countCoins);

        }
    }

    void sound()
    {
        src.clip = sfx;
        src.Play();
    }
}
