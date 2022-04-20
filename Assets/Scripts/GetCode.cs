using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetCode : MonoBehaviour
{
    public TMP_InputField code;
    public NetworkInterface network;

    public void getCode()
    {
        Debug.Log("submit code:"+code.text);
        network.SendFirmwareChange("r0", code.text);
    }
}
