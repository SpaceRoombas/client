using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMaster : MonoBehaviour
{
    public GameObject robotPref;
    public Dictionary<string, RobotController> robots; 
    // Start is called before the first frame update
    void Start()
    {
        robots = new Dictionary<string, RobotController>();

    }


    public void MoveRobot(string name, (int x, int y) position) {
        Debug.Log("Move Robot");
        if (robots.ContainsKey(name) == false) {
            GameObject r = Instantiate(robotPref, this.transform);
            r.name = name;

            Vector3 spawnpoint = new Vector3(position.y, -position.x);
            r.transform.position = spawnpoint;

            RobotController controller = r.GetComponent<RobotController>();
            controller.GoalPosition = spawnpoint;

            robots.Add(name, controller);
        }
        else {
            Debug.Log("robot already created,moving");
            RobotController controller = gameObject.transform.Find(name).GetComponent<RobotController>();
            controller.MoveToPos(new Vector3(position.y, -position.x));
        }
    }
   
}
