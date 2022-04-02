using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyntaxHighlighting : MonoBehaviour
{
    private string text;

   public void highlight(string userCode)
    {
        userCode.ToLower();
    }
}
