using UnityEngine;

public class OpenAndClosePanels : MonoBehaviour
{
    public GameObject codeEditor;
    public GameObject robotsEditor;

    public void openOrCloseEditor()
    {
        if (codeEditor.activeSelf == false) {
            codeEditor.SetActive(true);
        }
        else {
            codeEditor.SetActive(false);
            robotsEditor.SetActive(false);
        }
        
    }
    public void openOrCloseRobots()
    {
        if (robotsEditor.activeSelf == false) {
            robotsEditor.SetActive(true);
            codeEditor.SetActive(true);
        }
        else {
            robotsEditor.SetActive(false);
        }

    }
}
