using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float speed;
    public bool playerControlled;

    private Animator animator;

    private IEnumerator cor;
    private bool corRunning;

    [SerializeField]
    private Vector3 goalPosition;

    public Vector3 GoalPosition { 
        get { return goalPosition; }
        set { goalPosition= value; }
    }


    private void Start()
    {
        corRunning = false;
        animator = GetComponent<Animator>();
        MoveToPos(goalPosition);
    }

    private void Update()
    {
        Vector3 dir = Vector3.zero;
        if (playerControlled) {
            if (Input.GetKey(KeyCode.A)) {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D)) {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W)) {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S)) {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }
            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            gameObject.transform.position += Time.deltaTime * speed * dir;
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            goalPosition.x += 10;
            MoveToPos(goalPosition);
        }

    }

    public void MoveToPos(Vector3 endPos) {
        if (corRunning == true) {
            StopCoroutine(cor);
        }

        cor = MoveToPosEnum(endPos);
        StartCoroutine(cor);
    }

    private IEnumerator MoveToPosEnum(Vector3 endPos)
    {
        Debug.Log("Corrotine start"+endPos.x+endPos.y);
        corRunning = true;
        Vector3 startPos = transform.position;
        float t = 0f;
        while (t < 1f) {
            transform.position = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime*4;
            yield return null;
        }
        Debug.Log("Corrotine finish");
        corRunning = false;
    }

    

}

