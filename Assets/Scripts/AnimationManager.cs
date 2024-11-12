using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator handAnimator; // Reference to the hand's Animator
    public float displayDuration = 3.0f; // How long each letter animation will play before the next
    private Dictionary<char, string> letterAnimations; // Dictionary mapping each letter to its state name

    void Awake()
    {
        // Initialize the dictionary with state names for each letter
        letterAnimations = new Dictionary<char, string>
        {
            { 'A', "AnimateA" },
            { 'B', "AnimateB" },
            { 'C', "AnimateC" },
            { 'D', "AnimateD" },
            { 'E', "AnimateE" },
            { 'F', "AnimateF" },
            // Add more letters and corresponding state names here
        };
    }

    // Coroutine to play a letter animation (based on the state name)
    public IEnumerator PlayLetterAnimation(char letter)
    {
        letter = char.ToUpper(letter); // Ensure the letter is uppercase

        yield return new WaitForSeconds(displayDuration);

        if (letterAnimations.TryGetValue(letter, out string stateName))
        {

            // Transition to the specific animation state using CrossFade
            handAnimator.CrossFade(stateName, 0.2f);  // Adjust the transition duration as needed

            // Wait for the specified duration to allow the pose to display fully
            yield return new WaitForSeconds(displayDuration);
        }
        else
        {
            Debug.LogWarning($"No animation found for letter: {letter}");
        }
    }

    // Optional: Stop the letter animation and reset to a neutral pose (idle pose)
    public void StopLetterAnimation()
    {
        //StopAllCoroutines();
        // Crossfade to the idle state
        handAnimator.Play("Idle"); // Transition to idle
        Debug.Log("Hand animation stopped");
    }
}
