using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var up = transform.TransformDirection(Vector3.up);

        RaycastHit hit;

        Debug.DrawRay(transform.position, -up * 2, Color.green);

        if(Physics.Raycast(transform.position, -up, out hit, 2))
        {
            Debug.Log("Hit");

            if(hit.collider.gameObject.name == "floor")
            {
                print("Destroyed");
            }
        }
    }
}
