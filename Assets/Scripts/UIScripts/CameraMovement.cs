using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float zoomSpeed = 1f;
    private float zoom;



    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position += Vector3.up, Time.deltaTime * moveSpeed);
           
        }
        if (Input.GetKey("a"))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position += Vector3.left, Time.deltaTime * moveSpeed);
            
        }
        if (Input.GetKey("s"))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position += Vector3.down, Time.deltaTime * moveSpeed);
           
        }
        if (Input.GetKey("d"))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position += Vector3.right, Time.deltaTime * moveSpeed);
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize <= 25)
        {
            Camera.main.orthographicSize += zoomSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 5)
        {
            Camera.main.orthographicSize -= zoomSpeed;
        }
    }

}
