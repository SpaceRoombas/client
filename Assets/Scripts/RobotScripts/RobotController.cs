using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    
    public float speed;
    public bool playerControlled;
    public GameObject errorText;

    private Animator animator;
    public Transform particleTransform;
    public ParticleSystem mineParticle;

    private IEnumerator cor;
    private bool corRunning;

    [SerializeField]
    public string firmware;

    public string GetFirmware() {
        return firmware;
    }
    public void SetFirmware(string firm) {
        firmware = firm;
    }

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

    }

    public void SetError(){
        errorText.SetActive(true);
        Invoke("ResetError", 10.0f);
    }

    public void ResetError(){
        errorText.SetActive(false);
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
        //Debug.Log("Corrotine start"+endPos.x+endPos.y);
        corRunning = true;
        Vector3 startPos = transform.position;
        float t = 0f;
        while (t < 1f) {
            
            transform.position = Vector3.Lerp(startPos, endPos, t);
            Vector3 change = transform.position - startPos;
            if (Mathf.Abs(change.x) > Mathf.Abs(change.y)) {
                if (change.x > 0) {
                    animator.SetInteger("Direction", 2);
                }
                else {
                    animator.SetInteger("Direction", 3);
                }
            }
            else {
                if (change.y > 0) {
                    animator.SetInteger("Direction", 1);
                }
                else {
                    animator.SetInteger("Direction", 0);
                }
            }
            t += Time.deltaTime*4;
            yield return null;
        }
        animator.SetInteger("Direction", 0);
        //Debug.Log("Corrotine finish");
        corRunning = false;
    }

    public void Mine(float mine_x, float mine_y){ //0 north, 1 east, 2 south, 3west  
        float x = transform.position.x;
        float y = transform.position.y;
        int direction = 0;
        if (Mathf.Abs(mine_x - x) > Mathf.Abs(mine_y - y)) {
            if (mine_x > x) {
                direction = 1;
            }
            else {
                direction = 3;
            }
        }
        else {
            if (mine_y > y) {
                direction = 2;
            }
            else {
                direction = 0;
            }
        }


        if (direction == 2) {
            particleTransform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction == 1) {
            particleTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction == 0) {
            particleTransform.localRotation = Quaternion.Euler(0, 0, 270);
        }
        else if (direction == 3) {
            particleTransform.localRotation = Quaternion.Euler(0, 0, 180);
        }

        mineParticle.Play();

    }

   


}

