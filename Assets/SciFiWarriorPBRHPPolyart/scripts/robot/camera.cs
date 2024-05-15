using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camiracontroller : MonoBehaviour
{
    // public Transform target;
    public GameObject player;
    Vector3 offset;
    Vector3 newpos;

    void Start()
    {
        // Calculate the initial offset between the camera and the player
        offset = player.transform.position - transform.position;
        // Keep the y-offset only
        offset.y = -5;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            newpos = transform.position;
            newpos.x = player.transform.position.x - offset.x;
            newpos.z = player.transform.position.z - offset.z;
            transform.position = newpos;
            // transform.LookAt(target);


            // // Update the camera position to maintain the same y-position as the initial offset
            // transform.position = player.transform.position - offset;

            // // Rotate the camera to match the player's rotation smoothly
            // transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, Time.deltaTime);
        }
    }
}
