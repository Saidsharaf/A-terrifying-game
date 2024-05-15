using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class gun : MonoBehaviour
{

    public Transform Gunbullet;

    public GameObject Gunshots;
    public float speed = 10; // Adjust the speed as needed

    void Update()
    {
        if (Input.GetKey("w") || Input.GetKey("e"))
        {
            var bullet = Instantiate(Gunshots, Gunbullet.position, Gunbullet.rotation);
            bullet.GetComponent<Rigidbody>().velocity = Gunbullet.forward * speed * -1;
        }
    }
}

