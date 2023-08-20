using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallDropController : MonoBehaviour
{
    public XRSocketInteractor socketInteractor; // Reference to the XR Socket Interactor
    public Rigidbody ballRigidbody; // Reference to the ball's Rigidbody component
    public Transform dropPoint; // The point where you want the ball to drop

    private bool dropBall = false; // Flag to track if the ball is attached to the controller

    public float speed = 2;

    private void Start()
    {
        // Make sure the socket interactor is not null
        if (socketInteractor == null)
        {
            Debug.LogError("Socket Interactor not assigned!");
            enabled = false;
            return;
        }

        // Make sure the ballRigidbody is not null
        if (ballRigidbody == null)
        {
            Debug.LogError("Ball Rigidbody not assigned!");
            enabled = false;
            return;
        }
    }
    /*
    public void Update()
    {
        if (!dropBall)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    */


    public void DropBall()
    {
        if (!dropBall)
        {
            dropBall = true;
            socketInteractor.socketActive = false;
            Debug.Log(socketInteractor.socketActive);

            StartCoroutine(WaitForOneSecond());

            //ballRigidbody.isKinematic = true;  - this is physics

        }
    }

    IEnumerator WaitForOneSecond()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1.0f);

        // Code here will execute after waiting for 1 second
        Debug.Log("One second has passed!");

        socketInteractor.socketActive = true;
        dropBall = false;
        Debug.Log(socketInteractor.socketActive);
    }
}
