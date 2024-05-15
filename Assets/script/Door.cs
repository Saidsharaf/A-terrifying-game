using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Door : MonoBehaviour
{
    public GameObject door_closed, door_opened,intText,lockedText;
    public AudioSource open, close;
    public bool opened ,locked,allDoors;
    public static bool keyfounded, checklevel2;


    private void Start()
    {
        keyfounded = false;
       
    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
         if (other.CompareTag("MainCamera")){
            if (opened == false) {
                if (locked == false)
                {
                    intText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        door_closed.SetActive(false);
                        door_opened.SetActive(true);
                        intText.SetActive(false);
                        //open.play();
                        StartCoroutine(repeat());
                        opened = true;

                    }
                }
                if(locked == true)
                {
                    lockedText.SetActive(true);
                    
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (checklevel2 == true&&allDoors==true)
            {
                lockedText.SetActive(true);
                level2();
            }
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
            lockedText.SetActive(false);
        }
    }
   IEnumerator repeat()
    {
        yield return new WaitForSeconds(4.0f);
        opened=false;
        door_closed.SetActive(true);
        door_opened.SetActive(false);
    }
    private void Update()
    {
        if (keyfounded == true)
        {
            locked = false;
            checklevel2 = true;
        }
    }
    private void level2()
    {
        SceneManager.LoadScene(1);
    }
}
