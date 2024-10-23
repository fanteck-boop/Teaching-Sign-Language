using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelTemplate : MonoBehaviour
{
    public BlendShapeManager blendshapeManager;  // Reference to the BlendshapeManager
    public float levelCompleteDelay = 2.0f;      // Delay before returning to menu after completing a level
    protected abstract string GetWord();  // Each derived level specifies its word here


    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    // Function to start the level
    public void StartLevel()
    {
        string word = GetWord();  // Retrieve the word for this level
        StartCoroutine(SpellWordAndCompleteLevel(word));  // Start spelling the word using the BlendshapeManager
    }

    // Spell the word and then return to the menu after a delay
    private IEnumerator SpellWordAndCompleteLevel(string word)
    {
        // Spell the word
        yield return StartCoroutine(blendshapeManager.SpellWord(word));

        // Wait for a delay before transitioning
        yield return new WaitForSeconds(levelCompleteDelay);

        // Go back to the menu or load the next scene
        ReturnToMenu();
    }

    // Function to return to the main menu (you can customize this)
    private void ReturnToMenu()
    {
        Debug.Log("Level complete! Returning to menu...");
        // Add your scene transition logic here (e.g., SceneManager.LoadScene("MainMenu"));
    }
}




