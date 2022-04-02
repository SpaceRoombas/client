using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetCode : MonoBehaviour
{
    public TMP_InputField code;

    public void getCode()
    {
        Debug.Log(code.text);
    }
}
