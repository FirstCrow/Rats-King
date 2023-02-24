using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target;            //The target which the camera follows
    private GameObject player;          
    public float size = 6f;             //The size of the camera
    public float smoothSpeed = 10f;     //Determines how "snappy" the camera is to the target
    public Vector3 offset;              //Offsets the camera by a certain distance

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset.z = transform.position.z;
    }

    void FixedUpdate()
    {
        //GetComponent<Camera>().orthographicSize = size;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public void followPlayer()
    {
        target = player.transform;
    }
}