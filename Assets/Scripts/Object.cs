using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Object : MonoBehaviour
{

    private float rotationRate = 3.0f;
    private float jumpingForce = 200f;
    const float minPinchDistance = 0;
    const float pinchRatio = 1;
    const float minTurnAngle = 0;
    const float pinchTurnRatio = Mathf.PI / 2;


    static public float differenceFrames;
    static public float pinchDistance;
    static public float turnAngle;
    static public float turnAngleDelta;
    

    //Rigidbody for jumping
    private Rigidbody jumping;


    // Start is called before the first frame update
    void Start()
    {

        //Getting the RigidBody Component
        jumping = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.acceleration.x * 0.05f, 0, -Input.acceleration.z * 0.05f);
    }

    public void selectedColor()
    {

        // Adding color on tap of cube.
        GetComponent<Renderer>().material.color = Color.green;
    }

    public void dragObject()
    {

        //Steps ,1. Get the users touch 
        Touch touch = Input.GetTouch(0);

        // Steps 2 Check to see if they have actually touched the cube
        if (Input.touchCount > 0 && touch.phase == TouchPhase.Moved)
        {
            GetComponent<Renderer>().material.color = Color.blue;

            Vector3 mousePosition = new Vector3(touch.position.x, touch.position.y, 20);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = objPosition;
        }
        
    }

    public void pinching()
    {
        //Step 1 Get users touch for both fingers,

        // Get finger at index 0.
        Touch touch0 = Input.GetTouch(0);
        //Get finger at index 1.
        Touch touch1 = Input.GetTouch(1);

        // Step 2 The amount of touches has to be greater than or equal to 2.
        if (Input.touchCount >= 2)
        {


            GetComponent<Renderer>().material.color = Color.red;
            // Acquire the position of touch0.
            Vector3 touchPosition0 = touch0.position - touch0.deltaPosition;

            // Acquire the position of touch1.
            Vector3 touchPosition1 = touch1.position - touch1.deltaPosition;

            // Obtain distance between each of the touches.
            float prevTouch = (touchPosition0 - touchPosition1).magnitude;
            float touchdelta = (touch0.position - touch1.position).magnitude;


            //Finding the differences between frames
            float differenceFramess = prevTouch - touchdelta;

            // Scaling the object
            Vector3 upScaled = this.transform.localScale - new Vector3(differenceFramess, differenceFramess, differenceFramess) * 0.005f;
            this.transform.localScale = upScaled;


        }


    }

    public void rotate()
    {
        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch phase Moved");
                transform.Rotate(touch.deltaPosition.y * rotationRate,
                                 -touch.deltaPosition.x * rotationRate, 0, Space.World);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch phase Ended");
            }
        }
    
    }

    public void Jump()
    {
        // add force to the specific object using rigid body
        jumping.AddForce(new Vector3(0f, jumpingForce), ForceMode.Force);
    }










   
}
