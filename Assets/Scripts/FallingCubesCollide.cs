using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingCubesCollide : MonoBehaviour
{


    #region Variables
    private UiFunctions uiFunctions;
    public GameObject restartGame;
    public GameObject scoretxt;


    #endregion


    #region UnityFunctions
    void Start()
    {
        uiFunctions = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiFunctions>();
    }

    void Update()
    {

    }

    #endregion
    private void OnCollisionEnter(Collision collison)
    {
        //get the tag from the object
        if (collison.gameObject.tag == "Platform")
        {
            //set game object to false;
          this.gameObject.SetActive(false);
        }

        //get the tag from the object
        if (collison.gameObject.tag == "Player")
        {
            Debug.Log("Player hit!!");
            scoretxt.gameObject.SetActive(false); // disable score text
            restartGame.gameObject.SetActive(true); // show restart button
            uiFunctions.gamePlaying = false; // disable game
            uiFunctions.GameEnded();
            //if one of the cubes falling hits the player then restart game
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
