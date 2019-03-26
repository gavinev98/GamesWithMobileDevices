using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    #region Variables
    //Public
    [SerializeField]
    private float minimumX = 0.0f;
    [SerializeField]
    private float minimumY = 0.0f;

    //Get game object for spawning blocks will be multiple blocks.
    [SerializeField]
    private GameObject[] fallingblocks;


    //spawn rate of blocks
    [SerializeField]
    private float spawnTime = 0.0f;

    //when to spawn the blocks
    bool spawnblocks = false;


    //max amount of blocks to spawn
    private int maxAmountofblocks = 0;

    // blocks spawning
    private int blockSpawning = 0;

    //acquire ui functions
    private UiFunctions uiFunctions;

    //Private
    #endregion


    #region UnityFunctions
    // Start is called before the first frame update
    void Start()
    {
        uiFunctions = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UiFunctions>();
        spawnblocks = true;
    }

    // Update is called once per frame
    void Update()
    {

        //spawn blocks every frame if varaibles is true
        if(spawnblocks == true && uiFunctions.gamePlaying == true)
        {
            StartCoroutine("GenerateBlocks");
        }
        
    }

    public IEnumerator GenerateBlocks()
    {
        //initially set the blocks to false so they cant fall.
        spawnblocks = false;

        //create random spawn times
        spawnTime = Random.Range(0.5f, 2.0f);

        //spawn block
        maxAmountofblocks = Random.Range(1, 10);


        //loop over and keep creating the falling blocks
        for(int i =0; i < maxAmountofblocks; i ++)
        {
            Vector3 pos = new Vector3(Random.Range(minimumX, minimumY), Random.Range(5.0f, 10.0f), 0.0f);
            //Instantiate the falling blocks from position above cube.
            Instantiate(fallingblocks[blockSpawning], pos, Quaternion.identity);

        }

        //wait for a few seconds after game loadsWS
        yield return new WaitForSeconds(spawnTime);

        //spawn blocks after a number of seconds
        spawnblocks = true;

    }
    #endregion
}
