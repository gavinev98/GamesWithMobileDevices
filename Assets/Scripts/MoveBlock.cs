using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{

    #region Variables
    [SerializeField]
    private float speedOfCube = 0.0f;

    //access rigidbody for cube
    private Rigidbody cubeRigid;



    #endregion


    #region UnityFunctions
    // Start is called before the first frame update
    void Start()
    {
        //get rigid body on start method
        cubeRigid = GetComponent<Rigidbody>();
    
    }

    // Update is called once per frame
    void Update()
    {
        onMove();
    }
    

    private void onMove()
    {
        //Accelerometer for cubes
        transform.Translate(Input.acceleration.x * 0.09f, 0, -Input.acceleration.z * 0.00f);

    }
  
    #endregion
}
