using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class key : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inticon, thekey;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inticon.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
             thekey.SetActive(false);
             Door.keyfounded = true;
               // level2();
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
