using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator handAnimator; // Reference to the hand's Animator
    public float displayDuration = 1.0f; // How long each letter animation will play before the next
    private Dictionary<char, float> letterAnimations; // Dictionary mapping each letter to its animation index

    // Calling the courotine for the level scripts:
    // StartCoroutine(animationManager.PlayLetterAnimation('A'));

    void Awake()
    {
        // Upon awake, initialize the dictionary mapping letters to their blendshape index / animation flag
        // Initialize the dictionary with animation index values for each letter
        letterAnimations = new Dictionary<char, float>
        {
            { 'A', 0 },  // Animation index 0 for A
            { 'B', 1 },
            { 'C', 2 },
            { 'D', 3 },
            { 'E', 4 },
            { 'F', 5 },
            { 'G', 6 },
            { 'H', 7 },
            { 'I', 8 },
            { 'J', 9 },
            { 'K', 10 },
            { 'L', 11 },
            { 'M', 12 },
            { 'N', 13 },
            { 'O', 14 },
            { 'P', 15 },
            { 'Q', 16 },
            { 'R', 17 },
            { 'S', 18 },
            { 'T', 19 },
            { 'U', 20 },
            { 'V', 21 },
            { 'W', 22 },
            { 'X', 23 },
            { 'Y', 24 },
            { 'Z', 25 },
            { 'Æ', 26 }, // Animation for ?
            { 'Ø', 27 }, // Animation for ?
            { 'Å', 28 }  // Animation for ?
        };
    }

    // Since we want there to be "pauses" between each letter when spelling a letter
    // We make use of an Enumerator and couroutine. 
    public IEnumerator PlayLetterAnimation(char letter)
    {
        letter = char.ToUpper(letter);  // Ensure the letter is uppercase

        if (letterAnimations.ContainsKey(letter))
        {
            float animationIndex = letterAnimations[letter];
            handAnimator.SetFloat("AnimationIndex", animationIndex);
            handAnimator.SetBool("PlayAnimation", true);

            // Wait for the duration of the current animation
            yield return new WaitForSeconds(handAnimator.GetCurrentAnimatorStateInfo(0).length);
            //yield return new WaitForEndOfFrame();

            // Optionally, set PlayAnimation to false after the animation completes to reset it
            handAnimator.SetBool("PlayAnimation", false);
        }
        else
        {
            Debug.LogWarning($"No animation found for letter: {letter}");
        }
    }      
    
}
