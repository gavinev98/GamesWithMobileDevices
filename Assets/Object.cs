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
        
        Vector3 mousePosition = new Vector3(touch.position.x, touch.position.y, 20);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;

    }
        
    public void pinchToZoom()
    {
        GetComponent<Renderer>().material.color = Color.red;
        //Step 1 Get users touch for both fingers,

        // Get finger at index 0.
        Touch touch0 = Input.GetTouch(0);
        //Get finger at index 1.
        Touch touch1 = Input.GetTouch(1);

        // Step 2 The amount of touches has to be greater than or equal to 2.
        if(Input.touchCount >= 2)
        {
            // Acquire the position of touch0.
            Vector3 touchPosition0 = touch0.position - touch0.deltaPosition;

            // Acquire the position of touch1.
            Vector3 touchPosition1 = touch1.position - touch1.deltaPosition;

            // Obtain distance between each of the touches.
            float prevTouch = (touchPosition0 - touchPosition1).magnitude;
            float touchdelta = (touch0.position - touch1.position).magnitude;


            //Finding the differences between frames
            float differenceFrames = prevTouch - touchdelta;

            // Scaling the object
            Vector3 upScaled = this.transform.localScale - new Vector3(differenceFrames, differenceFrames, differenceFrames);
            this.transform.localScale = upScaled;
        }


    }

  



}
