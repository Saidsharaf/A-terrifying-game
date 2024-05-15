using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFollowing : MonoBehaviour
{
    public float speed = 1f;
    public GameObject player, GameOverScreen;

    private void Awake()
    {
        GameOverScreen.SetActive(false);
    }
    void Update()
    {
        if (player != null)
        {
            // Calculate direction from enemy to player
            Vector3 direction = player.transform.position - transform.position;

            // Rotate towards player's direction (only on the y-axis)
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

            // Move towards player's position
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("Player collided with enemy!");
            Destroy(other.gameObject); // Destroy the player GameObject
            GameOverScreen.SetActive(true);
            /*if (Input.GetKeyDown(KeyCode.F))
             {
                // SceneManager.LoadScene(0);
                 GameOverScreen.SetActive(false);
                 Debug.Log("ppppppppppppppp have coins !");

             }*/
            
        }
    }
   /* void gameOver()
    {
        //new WaitForSeconds(5.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    Debug.Log("ppppppppppppppp have coins !");
        
    }
   */

}
