using System;
using UnityEngine;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    #region fields

    [Header("Preferences")]
    [SerializeField, Range(1, 15)] int maxInput = 5;

    [Header("Setup Fields")]
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Transform player;

    [SerializeField] AudioSource UIAudio;
    [SerializeField] AudioClip correctCommand_clip;
    [SerializeField] AudioClip errorCommand_clip;

    #endregion

    void Update()
    {
        //code only continues if we press return/enter during runtime
        if (!Input.GetKeyDown(KeyCode.Return)) 
            return;

        //code only continues if input field has some text
        if (inputField.text == "") 
            return;

        ProcessCommand();
    }

    void MovePlayer(int dir)
    {
        //move player on the right * magnitude (- for left, + for right);
        player.Translate(transform.right * dir); 
    }

    void ProcessCommand()
    {
        //get words from input field
        string [] words = inputField.text.Split(" ").Length > 0 ? inputField.text.Split(" ") : new string [] { inputField.text };

        foreach (string s in words)
            print(s);

        //clear input field
        inputField.text = "";

        //var to say whether we have correct commands in the field
        bool correctInput = false;

        //check through all words/word in the input field whether we have passable commands
        for (int i = 0; i < words.Length; i++) {
            string currentWord = words[i].ToLower();

            if (currentWord == "right" || currentWord == "left") {
                correctInput = true;
                break;
            }
        }

        //only continue to execute commands if we have any in the input field
        if (!correctInput)
            return;

        //main command execution
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].ToLower() == "right" || words[i].ToLower() == "left")
            {
                int dir = 1;

                if (words[i].ToLower() == "right") dir = 1;
                else dir = -1;
                
                if (string.IsNullOrEmpty(words[i+1]))
                    return;

                if (int.TryParse(words[i + 1], out _))
                {
                    int amount = int.Parse(words[i+1]);

                    if (amount > maxInput) amount = maxInput;
            
                    MovePlayer(amount * dir);
                }

                else 
                    MovePlayer(dir);
            }
        }
    }
}
