using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject Canvaslvl; // Assuming this is a reference to a Canvas GameObject

    private void Start()
    {
        // Assign button objects if not already done in the Inspector
        if (buttons == null || buttons.Length == 0)
        {
            buttons = GameObject.FindGameObjectsWithTag("Button"); // Replace "Button" with actual tag
        }
    }

    public void ButtonPress(int buttonIndex)
    {
        if (buttonIndex >= 1 && buttonIndex <= 4)
        {
            Canvaslvl.SetActive(false); // Hide Canvaslvl
        }
        else if (buttonIndex == 5) // Check for button index 4 specifically
        {
            Canvaslvl.SetActive(true); // Show Canvaslvl
        }
    }

    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}