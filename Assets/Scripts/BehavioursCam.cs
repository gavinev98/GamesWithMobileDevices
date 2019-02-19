
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehavioursCam : MonoBehaviour
{
    public Transform target;
    public float speedcamera = 0.125f;
    public Vector3 offset;
    public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;
    private Camera cam;
    bool isTapped;
    bool isMoving;
    bool rotateObject;
    bool pinchtoZoom;
    bool isJumping;
    bool destroyCube;
    private bool isSelected = false;
    bool rotateCamera;
    static private Transform trSelect = null;
    public Vector3 OriginalPos;



    private Touch initTouch = new Touch();

    //the selected game object
    public GameObject selectedObject;

    private float rotationX = 0f;
    private float rotationy = 0f;

    //Original Roatation State for camera.
    private Vector3 originalRot;

    //Roatation speed of camera
    public float rotationSpeed = 0.5f;
    public float directionOFCamera = -1;

    //Gyroscope enabled code.
    private bool gyroEnable;
    private Gyroscope gyro;


    //Acceleromter
    bool isAccelerometerEnabled;


    const float pinchTurnRatio = Mathf.PI / 2;
    const float minTurnAngle = 0;

    const float pinchRatio = 1;
    const float minPinchDistance = 0;

    const float panRatio = 1;
    const float minPanDistance = 0;


    // Implementing rotation for camera.

    //   The delta of the angle between two touch points
    static public float turnAngleDelta;
    //   The angle between two touch points
    static public float turnAngle;
    //The delta of the distance between two touch points that were distancing from each other
    static public float pinchDistanceDelta;

    //The distance between two touch points that were distancing from each other
    static public float pinchDistance;



    // Use this for initialization
    void Start()
    {
        //startposition of camera
       

        //enable the gyroscop in start frame
        gyroEnable = EnableGyro();

        cam = GetComponent<Camera>();

        //Setting start color of all objects

        // If object has not been touched.
        if (Input.touchCount != 1)
        {
            print("Please select the cube object");

        }

       



    }

    // Update is called once per frame
    void Update()
    {

       


        //detecting the gyrascope
        //check to see if it is enabled
        if (gyroEnable)
        {
            //Displaying statistics incase it isnnt working
            Debug.Log("attitude" + gyro.attitude);
            Debug.Log("gravity" + gyro.gravity);
            Debug.Log("Acceleration" + gyro.userAcceleration);

            //phone probably doesnt have gyrascope.

        }

        // Detecting touches using phases for dragging / pinching rotating
        if (Input.touchCount > 0)
        {
            //Acquiring the position of the first finger.
            Touch touch = Input.GetTouch((0));

            //swithcing true the touch phases to check for movements.
            switch (touch.phase)
            {
                //First phase is the began phase which is used for touching the object.
                case TouchPhase.Began:
                    //Setting isTapped to false becuase the tap can only be valid once finger is removed.
                    isTapped = false;
                    isMoving = false;
                    pinchtoZoom = true;
                    rotateCamera = false;
                    rotateObject = true;
                    isJumping = false;
                    isAccelerometerEnabled = false;
                    destroyCube = true;
                    break;
                // The second phase of the TouchPhase is moving the object.
                case TouchPhase.Moved:
                    isTapped = false;
                    isMoving = true;
                    pinchtoZoom = false;
                    rotateCamera = true;
                    rotateObject = true;
                    isJumping = false;
                    isAccelerometerEnabled = false;
                    destroyCube = false;
                    break;
                // The third phase of the TouchPhase is stationary ie when the object is not moving.
                case TouchPhase.Stationary:
                    isTapped = false;
                    isMoving = false;
                    pinchtoZoom = true;
                    rotateCamera = false;
                    rotateObject = true;
                    isJumping = false;
                    isAccelerometerEnabled = false;
                    destroyCube = false;
                    break;
                // The final phase of the TouchPhase is the ended phase in which the object stops moving.
                case TouchPhase.Ended:
                    isTapped = true;
                    isMoving = false;
                    pinchtoZoom = false;
                    rotateCamera = false;
                    rotateObject = false;
                    isJumping = true;
                    isAccelerometerEnabled = true;
                    destroyCube = false;
                    break;


            }
        }


        //Creating a camera object to identify the position of the touch
        Ray laser = cam.ScreenPointToRay(Input.touches[0].position);

        //Creating a RaycastHit object which is used to retrieve infoormation from the raycast.
        RaycastHit hitInformation;

        if (Physics.Raycast(laser, out hitInformation))
        {

            //Obtain the hit object
            GameObject hitObject = hitInformation.transform.gameObject;

            //Acquire Touch
            Touch touch0 = Input.GetTouch(0);

            // Check if the touches length is greater than 0 and if finger has been lifted.
            if (Input.touches.Length > 0 && touch0.phase == TouchPhase.Ended)
            {
                print("Object Tapped");
                SelectedObject(hitObject);
            }


            Touch toucb0 = Input.GetTouch(0);

            if (selectedObject == true)
            {

                print("I am selected");

                Object ob = selectedObject.GetComponent<Object>();

                //Change color to green if selected
                ob.selectedColor();


                if (isMoving)
                {
                    //Accessing object class and applying drag method to selected object.
                    ob.dragObject();
                }

                if (rotateObject)
                {
                    //Accessing object class and applying rotate method to selected object.
                    ob.rotateCameras();
                }

                if (pinchtoZoom)
                {

                    ob.pinching();
                }
                if (isJumping)
                {
                    //Accessing object class and applying jump method to selected object.
                    ob.Jump();
                }
                if(destroyCube)
                {
                    ob.destroyCube(selectedObject);
                }



            }



        }
        else
        {
            //clearing the selected object.
            clear();
            //do camera stuff
            dragCamera();
            cameraZoom();
            rotateCameras();
            //  moveRotCamera();

        }

     


    }
    // Method to enable the gyrascope.
    public bool EnableGyro()
    {
        //testing the gyroscope
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            return true;
        }
        //if gyro is not enabled return false.
        Debug.Log("No gyrascope found");
        return false;

     }

    public void Toggle_Changed(bool value)
    {
        
    }


    //Selecting the object methods
    public void SelectedObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if (obj == selectedObject)
                return;

            clear();
        }

        Debug.Log("Testing/Working");

        selectedObject = obj;

        selectedObject.GetComponent<Renderer>().material.color = Color.green;

        // Obtain the script from the objects class and perform operations.

    }
    // Method to clear the selected object.
    public void clear()
    {

        if (selectedObject == null)
            return;
        print("Cleared");
        selectedObject.GetComponent<Renderer>().material.color = Color.white;
        selectedObject = null;

    }

    // Method to zoom in with the camera.
    public void cameraZoom()
    {

        // If there are two touches on the device...
        if (Input.touchCount == 2 && (Input.GetTouch(1).phase == TouchPhase.Moved))
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (cam.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                cam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 0.1f, 179.9f);
            }
        }

    }

    // Method to rotate the camera.
    public void moveRotCamera()
    {

        print("Rotating Camera");

        foreach (Touch touch in Input.touches)
        {
            // needs 3 fingers one stationary , 2 moving.
            if (Input.touchCount >= 2 &&
                (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved))
            {

                //calculate postition that camera is going to with delta position. Use began position.
                //first position
                float deltaXPosition = initTouch.position.x - touch.position.x;
                //y value current position minus the starting position
                float deltaYPosition = initTouch.position.y - touch.position.y;
                //decrease from the current x position created at the start by deltaX.
                rotationX -= deltaYPosition * Time.deltaTime * rotationSpeed * directionOFCamera;
                //Add rotation speed
                rotationy += deltaXPosition * Time.deltaTime * rotationSpeed * directionOFCamera;

                // stop values from going over 80 degrees by clamping on the x axis.
                rotationX = Mathf.Clamp(rotationX, -60f, 60f);
                //stoping values from going over 80 degress on the y axis.
                rotationy = Mathf.Clamp(rotationy, -60f, 60f);
                //translation to camera using vector.
                cam.transform.eulerAngles = new Vector3(rotationX, rotationy, 0f);
            }

            else
            {
                initTouch = new Touch();
            }

        }
    }

    // Method to drag camera with one finger
    public void dragCamera()
    {
        // Acquire the touch
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount == 1 && touch.phase == TouchPhase.Moved)
        {
            Vector3 deltaPosition = touch.deltaPosition;

            transform.Translate(-deltaPosition.x * speedcamera, -deltaPosition.y * speedcamera, 0);
        }

    }



    public void randomColor()
    {
        print("Reset to starting position");
        //Rest the camera position to the starting position.
        
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
        float pinchAmount = 0;
        Quaternion desiredRotation = transform.rotation;

        BehavioursCam.CalculateCamRotation();

        if (Mathf.Abs(BehavioursCam.turnAngleDelta) > 0)
        { // rotate
            Vector3 rotationDeg = Vector3.zero;
            rotationDeg.z = -BehavioursCam.turnAngleDelta;
            desiredRotation *= Quaternion.Euler(rotationDeg);
        }


        // not so sure those will work:
        transform.rotation = desiredRotation;
        transform.position += Vector3.forward * pinchAmount;
    }
}