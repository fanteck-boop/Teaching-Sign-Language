using UnityEngine;
using System.Collections.Generic; // Required for using Dictionary

public class NavigationManager : MonoBehaviour
{
    // Reference to the level menu UI
    [SerializeField] private GameObject levelMenu;

    // Function to toggle the visibility of the level menu and its children
    public void ToggleLevelMenu()
    {
        // Check if levelMenu is currently active
        if (levelMenu.activeInHierarchy)
        {
            // If active, deactivate the levelMenu and its children
            levelMenu.SetActive(false);
            foreach (Transform child in levelMenu.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            // If inactive, activate the levelMenu and its children
            levelMenu.SetActive(true);
            foreach (Transform child in levelMenu.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    // Function to check if an exercise is locked based on the exercise's collection number and progress
    public bool CheckIfLocked(GameObject exercise)
    {
        // Get the Exercise script component from the exercise GameObject

        /*
         * Dette skal laves om, således at den kan grabbe det rigtige script baseret på, om det er reading, spelling eller introduction
         * Vi tænker at lave tags!, enten reading, spelling eller introduction, som den skal tjekke efter og derfra gennem if eller switch til
         * enten at referere til enten ReadingExercise, SpellingExercise eller IntroductionExercise
        */
        ReadingExercise exerciseScript = exercise.GetComponent<ReadingExercise>();

        // Check if the exercise collection number is within or lower than the current collection
        if (GameManager.Instance.CurrentExerciseCollection >= exerciseScript.exerciseCollectionNumber)
        {
            // Check if exercise is "Introduction" or "Introduction" is completed
            if (exercise.name == "Introduction" || 
                (GameManager.Instance.CurrentExercises.TryGetValue("IntroductionCompleted", out bool isIntroductionCompleted) && isIntroductionCompleted))
            {
                return false; // Not locked 
            }
            else
            {
                return true; // Locked if "Introduction" is not completed
            }
        }
        else
        {
            return true; // Loked because it's in a future exercise collection
        }
    }

    // Function to check if an exercise is completed by looking it up in the GameManager's dictionary
    public bool CheckIfCompleted(GameObject exercise)
    {  
        string key = exercise.name + "Completed"; // The key used to get the key value pair corresponding to the exercise's name

        // Try to get the completion status of the exercise from the GameManager's dictionary
        if (GameManager.Instance.CurrentExercises.TryGetValue(key, out bool isCompleted) && isCompleted)
        {
            return true; // Exercise is completed
        }
        else
        {
            return false;  // Exercise is not completed
        }
       
    }

    // Function to open an exercise if it's not locked and check if it has been completed
    public void OpenExercise(GameObject exercise)
    {
        // Check if the exercise is not locked
        if (!CheckIfLocked(exercise))
        {
            // Deactivate the level menu
            ToggleLevelMenu();

            // Check if the exercise is not completed yet
            if (!CheckIfCompleted(exercise))
            {
                // Activate the exercise GameObject
                exercise.SetActive(true);
            }
            else
            {
                // Display a message saying the exercise is already completed
                // If already completed, just display the exercise
                Debug.Log("Exercise is already completed.");
                exercise.SetActive(true);
            }
        }
        else
        {
            // If the exercise is locked, display a message
            Debug.Log("Exercise is locked.");
        }
    }
}
