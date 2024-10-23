using UnityEngine;
using System.Collections.Generic; // Ensure you include this for Dictionary

public class NavigationManager : MonoBehaviour
{
    [SerializeField] private GameObject levelMenu;

    // Assuming you have a dictionary to track exercises and their completion state
    private Dictionary<string, bool> CurrentExercises = new Dictionary<string, bool>();

    public void ToggleLevelMenu()
    {
        // Check if levelMenu is currently active
        if (levelMenu.activeInHierarchy)
        {
            // Deactivate the levelMenu GameObject
            levelMenu.SetActive(false);

            // Deactivate all children
            foreach (Transform child in levelMenu.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            // Activate the levelMenu and its children
            levelMenu.SetActive(true);

            // Activate all children
            foreach (Transform child in levelMenu.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public bool CheckIfLocked(GameObject exercise)
    {
        // Check if the tag can be converted to an int (custom logic needed)
        if (int.TryParse(exercise.tag, out int intTag)) 
        {
            Debug.Log("Can be converted to int");

            if (GameManager.Instance.CurrentExerciseCollection >= intTag)
            {
                Debug.Log("Exercise is in the current or a prior exercise collection"); 
                // Check if exercise is "Introduction" or is completed
                if (exercise.name == "Introduction" || 
                    (CurrentExercises.TryGetValue("Introduction", out bool isCompleted) && isCompleted))
                {
                    Debug.Log("Exercise is Introduction or Introduction is completed");
                    return false;
                }
            }
            else
            {
                Debug.Log("Exercise is not in the current or a prior exercise collection"); 
                return true;
            }
        }
        Debug.Log("Can not be converted to int");
        return true; // If not a valid int or doesn't pass checks, treat as locked
    }


    public bool CheckIfCompleted(GameObject exercise)
    {  
        string key = exercise.name + "Completed";

        if (CurrentExercises.TryGetValue(key, out bool isCompleted))
        {
            return isCompleted; // Simplified return
        }
       
    }

    public void OpenExercise(GameObject exercise)
    {
        if (CheckIfLocked(exercise) == false)
        {
            // Deactivate the level menu
            ToggleLevelMenu();

            if (CheckIfCompleted(exercise) == false)
            {
                // Activate the exercise GameObject
                exercise.gameObject.SetActive(true);
            }
            else
            {
                // Display a message saying the exercise is already completed
                Debug.Log("Exercise is already completed.");
            }
        }
        else
        {
            // Display a message saying the exercise is locked
            Debug.Log("Exercise is locked.");
        }
    }
}
