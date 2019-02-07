
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehavioursCam : MonoBehaviour
{

    public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;
    private Camera cam;
    bool isTapped;
    bool isMoving;
    bool pinchtoZoom;
    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();

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
                    break;
                // The second phase of the TouchPhase is moving the object.
                case TouchPhase.Moved:
                    isTapped = false;
                    isMoving = true;
                    pinchtoZoom = false;
                    break;
                // The third phase of the TouchPhase is stationary ie when the object is not moving.
                case TouchPhase.Stationary:
                    isTapped = false;
                    isMoving = false;
                    pinchtoZoom = true;
                    break;
                // The final phase of the TouchPhase is the ended phase in which the object stops moving.
                case TouchPhase.Ended:
                    isTapped = true;
                    isMoving = false;
                    pinchtoZoom = false;
                    break;


            }
        }

        // Methods for if the object has been tapped
        if (isTapped)
        {
            print("Is tapped");
            //Creating a camera object to identify the position of the touch
            Ray laser = Camera.main.ScreenPointToRay(Input.touches[0].position);

            //Creating a RaycastHit object which is used to retrieve infoormation from the raycast.
            RaycastHit hitInformation;

            if (Physics.Raycast(laser, out hitInformation))
            {

                // Obtain the script from the objects class and perform operations.
                Object scriptofObj = hitInformation.collider.GetComponent<Object>();

                // If the object has been tapped i want to be able to add an effect
                scriptofObj.addTapEffect();
            }
        }

        if (isMoving)
        {
            print("Is moving");
            //Creating a camera object to identify the position of the touch
            Ray laser = Camera.main.ScreenPointToRay(Input.touches[0].position);

            //Creating a RaycastHit object which is used to retrieve information from the raycast.
            RaycastHit hitInformation;

            if (Physics.Raycast(laser, out hitInformation))
            {

                // Obtain the script from the objects class and perform operations.
                Object scriptofObj = hitInformation.collider.GetComponent<Object>();

                // If the object has been tapped i want to be able to add an effect
                scriptofObj.dragObject();
            }
        }

        if (pinchtoZoom)
        {
            print("Print to zoom");

            //Creating a camera object to identify the position of the touch
            Ray laser = Camera.main.ScreenPointToRay(Input.touches[0].position);

            //Creating a RaycastHit object which is used to retrieve information from the raycast.
            RaycastHit hitInformation;

            if (Physics.Raycast(laser, out hitInformation))
            {

                // Obtain the script from the objects class and perform operations.
                Object scriptofObj = hitInformation.collider.GetComponent<Object>();

                // If the object has been tapped i want to be able to add an effect
                scriptofObj.pinchToZoom();
            }
            else
            {
                cameraZoom();
            }


        }


    }

    public void cameraZoom()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
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
}






    

