using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Object : MonoBehaviour
{




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Accelerometer
      //  transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);

    }

    public void addTapEffect()
    {
        // Making the object/lcube bigger by 1 ontap

        transform.localScale += new Vector3(0.1f, 0, 0);

       // Adding color on tap of cube.
        GetComponent<Renderer>().material.color = Color.magenta;
       





    }
}