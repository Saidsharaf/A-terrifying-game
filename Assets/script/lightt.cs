using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class lightt : MonoBehaviour
{
    public GameObject flashlight_ground, inticon, flashlight_player;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
                    inticon.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                       flashlight_ground.SetActive(false);
                       inticon.SetActive(false);
                       flashlight_player.SetActive(true);
                    }
                
            
        }
            }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inticon.SetActive(false);
        }
    }
}
