using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnResetButtonClicked()
    {
        // Reload the current scene
        SceneManager.LoadScene(0);
        Debug.Log("player have coins !");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene(0);

            Debug.Log("Game Over !");

        }
    }
}
