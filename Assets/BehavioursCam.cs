
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehavioursCam : MonoBehaviour {

	public float perspectiveZoomSpeed = 0.5f;        
	public float orthoZoomSpeed = 0.5f;
	private Camera cam;

    // Use this for initialization
    void Start ()
    {
	    cam = GetComponent<Camera>();
    }
	
    // Update is called once per frame
    void Update () {
	    
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

