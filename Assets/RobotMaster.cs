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
        //MoveRobot("R0", (0, 0));
        //MoveRobot("R1", (1, 3));

    }


    public void MoveRobot(string name, (int x, int y) position,string sector) {
        (int x, int y) offset = RenderWorld.ParseSector(sector);
        if (robots.ContainsKey(name) == false) {
            GameObject r = Instantiate(robotPref, this.transform);
            r.name = name;

            Vector3 spawnpoint = new Vector3(position.x, -position.y);
            r.transform.position = spawnpoint;

            RobotController controller = r.GetComponent<RobotController>();
            controller.GoalPosition = spawnpoint;

            robots.Add(name, controller);
        }
        else {

            RobotController controller = gameObject.transform.Find(name).GetComponent<RobotController>();
            controller.MoveToPos(new Vector3( position.x+offset.x, -position.y+offset.y));
        }
    }

    
   
}
