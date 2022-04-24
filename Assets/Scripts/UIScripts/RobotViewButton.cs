using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotViewButton : MonoBehaviour
{
    public string name;
    public string firmware;
    public TMP_InputField input;
    
    public void MovetoCodeEditor()
    {
        GameObject r = GameObject.Find("Robots/" + name);
        r.GetComponent<TMP_InputField>().text = firmware;
    }
}
