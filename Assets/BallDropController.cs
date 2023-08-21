using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallDropController : MonoBehaviour
{
    public XRSocketInteractor socketInteractor; // Reference to the XR Socket Interactor
    public Rigidbody AlumBall; // Reference to the ball's Rigidbody component
    public Rigidbody CopperBall;
    public Transform dropPoint; // The point where you want the ball to fall

    public float liquidDrag;

    private bool dropBall = false; // Flag to track if the ball is attached to the controller

    

    private void Start()
    {
        // Make sure the socket interactor is not null
        if (socketInteractor == null)
        {
            Debug.LogError("Socket Interactor not assigned!");
            enabled = false;
            return;
        }

        
    }
    




    public void DropBall()
    {
        if (!dropBall)
        {
            dropBall = true;

            AlumBall.drag +=  liquidDrag;
            CopperBall.drag += liquidDrag;

            socketInteractor.socketActive = false;
            Debug.Log(socketInteractor.socketActive);

            StartCoroutine(WaitForOneSecond());

            

        }
        else
        {
            // Reset dropBall flag and re-enable socket interactor immediately
            dropBall = false;
            socketInteractor.socketActive = true;
        }
    }

    IEnumerator WaitForOneSecond()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(5.0f);

        // Code here will execute after waiting for 1 second
        Debug.Log("One second has passed!");

        socketInteractor.socketActive = true;
        dropBall = false;
        Debug.Log(socketInteractor.socketActive);
    }
}