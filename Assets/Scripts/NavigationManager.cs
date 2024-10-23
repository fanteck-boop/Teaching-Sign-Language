using UnityEngine;

public class NavigationManager : MonoBehaviour
{

    [SerializeField] private GameObject levelMenu;
    public void ToggleLevelMenu()
    {
        // Check if Canvaslvl is currently active
        if (levelMenu.activeInHierarchy)
        {
            // Deactivate the Canvaslvl GameObject
            levelMenu.SetActive(false);

            // Iterate through each child of Canvaslvl and deactivate it
            foreach (Transform child in levelMenu.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            //activate the Canvaslvl and its children
            levelMenu.SetActive(true);

            //Activate all children
            foreach (Transform child in levelMenu.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

}