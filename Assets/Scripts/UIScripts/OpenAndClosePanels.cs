using UnityEngine;

public class OpenAndClosePanels : MonoBehaviour
{
    public GameObject codeEditor;
    public GameObject robotsEditor;
    public CameraMovement camera;

    public void openOrCloseEditor()
    {
        if (codeEditor.activeSelf == false) {
            codeEditor.SetActive(true);
            camera.SetEditorOpen(true);
        }
        else {
            codeEditor.SetActive(false);
            robotsEditor.SetActive(false);
            camera.SetEditorOpen(false);
        }
        
    }
    public void openOrCloseRobots()
    {
        if (robotsEditor.activeSelf == false) {
            robotsEditor.SetActive(true);
            codeEditor.SetActive(true);
            camera.SetEditorOpen(true);
        }
        else {
            robotsEditor.SetActive(false);
        }

    }
}
