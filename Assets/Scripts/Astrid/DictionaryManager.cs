using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class DictionaryManager : MonoBehaviour
{
    public GameObject dictionaryPanel;
    public VideoPlayer videoPlayer;
    public VideoClip[] letterClips;
    public Button[] letterButtons;
    public TextMeshProUGUI letterText;

    private void Start()
    {
        // Initially hide the dictionary panel
        dictionaryPanel.SetActive(false);

        // Assign OnClick listeners to each letter button
        for (int i = 0; i < letterButtons.Length; i++)
        {
            int index = i; // Local variable to avoid closure issue
            letterButtons[i].onClick.AddListener(() => PlayLetterVideo(index));
        }
    }

    // Toggles the visibility of the dictionary panel
    public void ToggleDictionary()
    {
        dictionaryPanel.SetActive(!dictionaryPanel.activeSelf);
    }

    // Plays the video clip corresponding to the clicked letter
    public void PlayLetterVideo(int letterIndex)
    {
        if (letterIndex >= 0 && letterIndex < letterClips.Length)
        {
            videoPlayer.clip = letterClips[letterIndex];
            videoPlayer.Play();
            videoPlayer.isLooping = true;

            //  Update the text at the top of the page
            letterText.text = GetLetterName(letterIndex);
        }
    }

    // Method to get the letter name based on the index
    private string GetLetterName(int index)
    {
        // Getting the letter buttons: "A", "B", "C", etc.
        return letterButtons[index].name;
    }
}

