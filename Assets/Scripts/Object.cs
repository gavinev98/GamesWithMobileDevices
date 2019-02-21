using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Object : MonoBehaviour
{
    
    public GameObject remains;
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

    //The delta of the distance between two touch points that were distancing from each other
    static public float pinchDistanceDelta;


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

        //Accelerometer for cubes
        transform.Translate(Input.acceleration.x * 0.01f, 0, -Input.acceleration.z * 0.01f);

    }

    public void selectedColor()
    {

        if (Input.touchCount >= 1)
        {
            // Adding color on tap of cube.
            GetComponent<Renderer>().material.color = Color.green;
        }
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


    static public void CalculateCamRotation()
    {
        pinchDistance = pinchDistanceDelta = 0;
        turnAngle = turnAngleDelta = 0;

        // if two fingers are touching the screen at the same time ...
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.touches[0];
            Touch touch2 = Input.touches[1];

            // ... if at least one of them moved ...
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // ... check the delta distance between them ...
                pinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition,
                                                      touch2.position - touch2.deltaPosition);
                pinchDistanceDelta = pinchDistance - prevDistance;

                // ... or check the delta angle between them ...
                turnAngle = Angle(touch1.position, touch2.position);
                float prevTurn = Angle(touch1.position - touch1.deltaPosition,
                                       touch2.position - touch2.deltaPosition);
                turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

                // ... if it's greater than a minimum threshold, it's a turn!
                if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
                {
                    turnAngleDelta *= pinchTurnRatio;
                }
                else
                {
                    turnAngle = turnAngleDelta = 0;
                }
            }
        }
    }

    static private float Angle(Vector3 pos1, Vector3 pos2)
    {
        Vector3 from = pos2 - pos1;
        Vector3 to = new Vector3(1, 0);

        float result = Vector3.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);

        if (cross.z > 0)
        {
            result = 360f - result;
        }

        return result;


    }

    public void rotateCameras()
    {

        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        if (Input.touchCount >= 2)
        {

            if (touch1.phase == TouchPhase.Moved)
            {
                float pinchAmount = 0;
                Quaternion desiredRotation = transform.rotation;



                CalculateCamRotation();

                if (Mathf.Abs(turnAngleDelta) > 0)
                {
                    // rotate
                    Vector3 rotationDeg = Vector3.zero;
                    rotationDeg.z = -turnAngleDelta;
                    desiredRotation *= Quaternion.Euler(rotationDeg);
                }


                // not so sure those will work:
                transform.rotation = desiredRotation;
                transform.position += Vector3.forward * pinchAmount;
            }
        }
    }


    // method to destroy cube into mini particles.
    public void destroyCube(GameObject hitObject)
    {
        //Acquiring the first touch.
        Touch touch0 = Input.GetTouch(0);
        //Acqu
        Touch touch1 = Input.GetTouch(1);

        if(Input.touchCount > 0)
        {
            // Two fingers need to be in the began stage in order to activate the destroy.
            if(touch0.phase == TouchPhase.Began && touch1.phase == TouchPhase.Began)
            {

                print("cube destroyed");
                //Instantiating the gameobject  i attatched to the script.
                Instantiate(remains, transform.position, transform.rotation);
                //Destroying the currently selected object.
                Destroy(hitObject);
            }
        }
    }














}
