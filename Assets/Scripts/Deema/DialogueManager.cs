using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    public bool dialogueIsPlaying { get; private set; }
    //read only outside scripts

    private Story currentStory;
    private static DialogueManager instance;


    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }
    private void Update()
    {
        //return if dialogue is not playing 
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (InputManager.GetInstance().GetInteractPressed())
        {
            Debug.Log("Interact button pressed"); // Add this line
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        Debug.Log("ENtered dialogue mode");
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }


    private void ExitDialogueMode()
    {
        Debug.Log("EXITED dialogue mode");
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}