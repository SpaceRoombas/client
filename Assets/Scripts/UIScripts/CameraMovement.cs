using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float speedMultiplyer;
    public float zoomSpeed = 1f;
    private float zoom;
   
    public Transform focus;
    public void SetFocus(Transform t) {
        focus = t;
    }

    private bool editorOpen = false;
    public void SetEditorOpen(bool open) {
        editorOpen = open;
    }


    void Update()
    {
        if (editorOpen == false) {
            float speed = moveSpeed;
            if (Input.GetKey(KeyCode.LeftShift)) {
                speed = moveSpeed * speedMultiplyer;
                Debug.Log(moveSpeed + " " + speed);
            }
            

            if (Input.GetKey("w")) {
                transform.position = Vector3.Lerp(transform.position, transform.position += Vector3.up, Time.deltaTime * speed);
                focus = null;
            }
            if (Input.GetKey("a")) {
                transform.position = Vector3.Lerp(transform.position, transform.position += Vector3.left, Time.deltaTime * speed);
                focus = null;
            }
            if (Input.GetKey("s")) {
                transform.position = Vector3.Lerp(transform.position, transform.position += Vector3.down, Time.deltaTime * speed);
                focus = null;
            }
            if (Input.GetKey("d")) {
                transform.position = Vector3.Lerp(transform.position, transform.position += Vector3.right, Time.deltaTime * speed);
                focus = null;
            }
           
        }
        if (focus != null) {
            transform.position = new Vector3(focus.position.x, focus.position.y, transform.position.z);
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
