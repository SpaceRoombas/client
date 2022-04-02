using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenPreferredEditor : MonoBehaviour
{
    public TMP_InputField userIde;
    public void startIde()
    {
        System.Diagnostics.Process.Start(@"" + userIde.text);
       
        //@"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe"
    }
}
