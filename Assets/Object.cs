﻿using System.Collections;
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
        //Changing color of cube to identify dragging movement.
        GetComponent<Renderer>().material.color = Color.black;
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

    public void dragObject()
    {

      
        //Changing color of cube to identify dragging movement.
        GetComponent<Renderer>().material.color = Color.blue;

        //Steps ,1. Get the users touch 
        Touch touch = Input.GetTouch(0);

        // Steps 2 Check to see if they have actually touched the cube
    
            Vector3 positionofTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 20));

        //Set position of the current object to the touch.
           transform.position = Vector3.Lerp(transform.position, positionofTouch, Time.deltaTime);

        }
        
    public void pinchToZoom()
    {
        //Step 1 Get users touch for both fingers,

        // Get finger at index 0.
        Touch touch0 = Input.GetTouch(0);
        //Get finger at index 1.
        Touch touch1 = Input.GetTouch(1);


    }



}
