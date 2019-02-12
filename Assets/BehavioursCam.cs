
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
    private bool isSelected = false;
    bool rotateCamera;
    static private Transform trSelect = null;
    GameObject cube;
    GameObject sphere;

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



    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();

        //Setting start color of all objects
        




    }

    // Update is called once per frame
    void Update()
    {

        // If object has not been touched.
        if (Input.touchCount != 1)
        {
            print("Please select the cube object");

        }


        // Detecting touches using phases for dragging / pinching rotating
        if (Input.touchCount > 0)
        {
            //Acquiring the position of the finger.
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
                    rotateObject = false;
                    break;
                // The second phase of the TouchPhase is moving the object.
                case TouchPhase.Moved:
                    isTapped = false;
                    isMoving = true;
                    pinchtoZoom = false;
                    rotateCamera = true;
                    rotateObject = true;
                    break;
                // The third phase of the TouchPhase is stationary ie when the object is not moving.
                case TouchPhase.Stationary:
                    isTapped = false;
                    isMoving = false;
                    pinchtoZoom = true;
                    rotateCamera = false;
                    rotateObject = false;
                    break;
                // The final phase of the TouchPhase is the ended phase in which the object stops moving.
                case TouchPhase.Ended:
                    isTapped = true;
                    isMoving = false;
                    pinchtoZoom = false;
                    rotateCamera = false;
                    rotateObject = false;
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


                print("Object Tapped");
                SelectedObject(hitObject);

                if (selectedObject == true)
                {
                    print("I am selected");

                    Object ob = selectedObject.GetComponent<Object>();

                    if (isMoving)
                    {
                        ob.dragObject();
                    }
                    if (pinchtoZoom)
                    {
                        ob.pinchToZoom();
                    }
                    if (rotateObject)
                    {
                        ob.rotate();
                    }

                }


            
        }


        else
        {

            clear();
            //do camera stuff
            dragCamera();
            cameraZoom();
            moveRotCamera();

        }
        


    }

    public void cameraZoom()
    {

        // If there are two touches on the device...
        if (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Stationary &&
                                      Input.GetTouch(1).phase == TouchPhase.Moved))
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

            if (Input.GetTouch(1).phase == TouchPhase.Ended || Input.GetTouch(2).phase == TouchPhase.Ended)
            {
                initTouch = new Touch();
            }

        }
    }


    public void dragCamera()
    {
        // Acquire the touch
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount == 1)
        {
            Vector3 deltaPosition = touch.deltaPosition;

            transform.Translate(-deltaPosition.x * speedcamera, -deltaPosition.y * speedcamera, 0);
        }

    }









    public void SelectedObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if (obj == selectedObject)
                return;

            clear();
        }

        Debug.Log("vdovmeriovewro");

        selectedObject = obj;
        
            selectedObject.GetComponent<Renderer>().material.color = Color.green;

            print("Working");

           
            // Obtain the script from the objects class and perform operations.
 
}

    public void clear()
    {
        
        if (selectedObject == null)
            return;
        print("Cleared");
        selectedObject.GetComponent<Renderer>().material.color = Color.white;
        selectedObject = null;

    }


}