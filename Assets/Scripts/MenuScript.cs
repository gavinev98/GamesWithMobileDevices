using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void ChangeSceneView(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Touch touch0 = Input.GetTouch(0);
        if (touch0.phase == TouchPhase.Ended)
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
   
}
