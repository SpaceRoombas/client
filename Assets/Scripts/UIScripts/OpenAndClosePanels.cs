using UnityEngine;

public class OpenAndClosePanels : MonoBehaviour
{
    public GameObject panel;

    public void openOrClose()
    {
        if (panel.activeSelf == false)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
