using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class HandheldTimer : MonoBehaviour

{
    public TextMeshProUGUI text;

    public BallDropController ballDropController; // Reference to the BallDropController script

    public InputAction aButtonAction;

    public XRGrabInteractable grabInteractable;


    private bool countStarted; // Flag to track whether the countdown has started

    public float time = 0f;
    

    private void OnEnable()
    {
        aButtonAction.Enable();
    }

    private void OnDisable()
    {
        aButtonAction.Disable();
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        countStarted = false;
        
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(StartStopCountdown);

        // Get the XRGrabInteractable component attached to this object
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable == null)
        {
            Debug.LogError("This object does not have an XRGrabInteractable component.");
        }

    }

    private void Update()
    {
        if (countStarted)
        {
            time += Time.deltaTime;
            //Debug.Log(time);
            //text.text = "00:" + time + "s
            ShowTime();
            
            ballDropController.DropBall();
            
        }

        if(!countStarted)
        {
            
            //Debug.Log(time);
        }

        if (aButtonAction.ReadValue<float>() > 0.5f && grabInteractable.isSelected)
        {
            Debug.Log("A button pressed!");
            // Your code here to respond to the A button press
            ResetTimer();
        }


    }

    public void StartStopCountdown(ActivateEventArgs arg)
    {
        if(!countStarted) { countStarted = true; }
        else if(countStarted) { countStarted = false; }
    }

    public void ResetTimer()
    {
        time = 0f;
        Debug.Log(time);
        //text.text = "00:" + time + "s";
        ShowTime();
    }

    private void ShowTime()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        string timeText = string.Format("{0:00}:{1:00}:{2:000}s", minutes, seconds, milliseconds / 10);


        text.text = timeText;
    }
}
