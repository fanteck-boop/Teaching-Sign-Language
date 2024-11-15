using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator handAnimator; // Reference to the hand's Animator
    private float displayDuration = 1.6f; // How long each letter animation will play before the next
    private Dictionary<char, string> letterAnimations; // Dictionary mapping each letter to its state name
    private float crossFadeTime = 0.2f;

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
            handAnimator.CrossFade(stateName, crossFadeTime);  // Adjust the transition duration as needed
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

    public void GoToIdle()
    {
        handAnimator.CrossFade("Idle", crossFadeTime); // Transition to idle
    }
}
