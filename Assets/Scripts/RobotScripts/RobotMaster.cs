using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMaster : MonoBehaviour
{
    public GameObject robotPref;
    public Dictionary<string, RobotController> robots;
    public Dictionary<string, string> serverFirmwares;
    // Start is called before the first frame update
    void Start()
    {
        robots = new Dictionary<string, RobotController>();
        serverFirmwares = new Dictionary<string, string>();
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

            if (serverFirmwares.ContainsKey(name))
            {
                controller.SetFirmware(serverFirmwares[name]);
            }

            robots.Add(name, controller);
        }
        else {

            RobotController controller = gameObject.transform.Find(name).GetComponent<RobotController>();
            controller.MoveToPos(new Vector3( position.x+offset.x, -position.y+offset.y));
        }
    }
    public void MoveRobot(string name, (int x, int y) position, string sector,string firmware)
    {
        (int x, int y) offset = RenderWorld.ParseSector(sector);
        if (robots.ContainsKey(name) == false) {
            GameObject r = Instantiate(robotPref, this.transform);
            r.name = name;

            Vector3 spawnpoint = new Vector3(position.x, -position.y);
            r.transform.position = spawnpoint;

            RobotController controller = r.GetComponent<RobotController>();
            controller.GoalPosition = spawnpoint;
            if (serverFirmwares.ContainsKey(name))
            {
                controller.SetFirmware(serverFirmwares[name]);
            }
            robots.Add(name, controller);
        }
        else {

            RobotController controller = gameObject.transform.Find(name).GetComponent<RobotController>();
            controller.MoveToPos(new Vector3(position.x + offset.x, -position.y + offset.y));
        }
    }

    public void ErrorRobot(string name)
    {
        if (robots.ContainsKey(name) == true)
        {
            RobotController controller = gameObject.transform.Find(name).GetComponent<RobotController>();
            controller.SetError();
        }
    }

    public void AddFirmwareFromServer(string robot_id, string firmware)
    {
        RobotController controller;
        
        serverFirmwares[robot_id] =firmware;
        
        if(robots.ContainsKey(robot_id))
        {
            controller = robots[robot_id];

            if(controller.GetFirmware() == string.Empty)
            {
                controller.SetFirmware(firmware);
            }
        }
    }

}
