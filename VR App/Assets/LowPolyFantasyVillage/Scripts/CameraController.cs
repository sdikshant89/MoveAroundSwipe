using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 startpos;
    private Vector2 movedPos;
    //private Vector3 cameraInitialPos;
    //private float speed = 5;
    //public int pixelDistToDetect = 20;
    private bool fingerDown;

    private float xRotation = 180;
    private float yRotation = 0;
    private float zRotation = 0;
    private bool isTap = false;
    private Vector3 prevAccState;
    private bool flag;

    //public Transform orientation;


    // Start is called before the first frame update
    void Start()
    {
        prevAccState = Input.acceleration;
        Debug.Log(prevAccState);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //BELOW IS THE IMPLEMENTATION OF VR
        //Vector3 currAccState = Input.acceleration;
        
        ////Debug.Log(Vector3.Distance(currAccState, prevAccState));

        //Vector3 V3 = currAccState - prevAccState;
        //Debug.Log(V3);
        //float movex = V3.x * 7f;
        //float movez = V3.z * 2f;

        //xRotation += movex;
        //zRotation += movez;

        //zRotation = Mathf.Clamp(zRotation, -90f, 90f);

        //transform.rotation = Quaternion.Euler(-zRotation, -xRotation, 0);

        //Debug.Log(Input.acceleration);        
        if (Input.touches != null && Input.touches.GetLength(0) != 0)
        {
            if (Input.touches.Length == 1)
            {
                Touch touch = Input.touches[0];
                if (fingerDown == false && Input.touchCount > 0 && touch.phase == TouchPhase.Began)
                {
                    startpos = touch.position;
                    fingerDown = true;
                    isTap = true;
                }
                if (fingerDown && touch.phase == TouchPhase.Moved)
                {
                    isTap = false;
                    movedPos = touch.position - startpos;
                    float touchx = movedPos.x * 0.01f;
                    float touchy = movedPos.y * 0.01f;

                    xRotation += touchx;
                    yRotation += touchy;

                    yRotation = Mathf.Clamp(yRotation, -90f, 90f);

                    transform.rotation = Quaternion.Euler(-(yRotation), xRotation, 0);
                    
                }
                if (fingerDown && touch.phase == TouchPhase.Stationary)
                {
                    transform.Translate(Vector3.forward * 10 * Time.deltaTime);
                }
                if (fingerDown && touch.phase == TouchPhase.Ended)
                {
                    fingerDown = false;
                    startpos = Vector2.zero;
                    if (isTap)
                    {
                        //Also check for the distance and add a threshold
                        Debug.Log("Doneeeee");
                        // This considers the current position of object as origin and then works on it
                        transform.Translate(Vector3.forward * 10 * Time.deltaTime);
                    }
                }
            }
        }
    }
}

//if (Input.touches != null && Input.touches.GetLength(0) != 0) {
//    Touch touch = Input.touches[0];
//    if (fingerDown == false && Input.touchCount > 0 && touch.phase == TouchPhase.Began)
//    {
//        startpos = touch.position;
//        fingerDown = true;
//    }
//    if (fingerDown && touch.phase == TouchPhase.Moved)
//    {
//        movedPos = touch.position - startpos;
//        Vector3 movement = new Vector3((float)(movedPos.x*0.001), 0.0f, (float)(movedPos.x * 0.001));
//        //rb.AddForce(movement * speed);
//        transform.Translate(movement * speed, Space.Self);
//    }
//}


//if (currAccState.x != prevAccState.x || currAccState.y != prevAccState.y)
//{
//    Vector3 V3 = currAccState - prevAccState;
//    float touchx = V3.x * 50f;
//    float touchy = V3.y * 50f;

//    xRotation += touchx;
//    yRotation += touchy;

//    transform.rotation = Quaternion.Euler(0, -xRotation, 0);
//}
//prevAccState = currAccState;


//Debug.Log(touchx);
//Debug.Log(touchy);

// Rotate the cube by converting the angles into a quaternion.
//Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

//// Dampen towards the target rotation
//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
