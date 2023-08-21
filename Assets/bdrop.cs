using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class bdrop : MonoBehaviour
{
    public XRSocketInteractor socketInteractor; // Reference to the XR Socket Interactor
    public Rigidbody ballRigidbody; // Reference to the ball's Rigidbody component
    public Transform dropPoint; // The point where you want the ball to fall

    private bool dropBall = false; // Flag to track if the ball is attached to the controller

    public float speed = 2;
    public float fallDuration = 2.0f; // Duration in seconds for the ball to fall

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

    public void DropBall()
    {
        if (!dropBall)
        {
            dropBall = true;
            socketInteractor.socketActive = false;

            // Calculate the initial position and direction for the ball's movement
            Vector3 initialPosition = ballRigidbody.position;
            Vector3 targetPosition = dropPoint.position;
            Vector3 direction = (targetPosition - initialPosition).normalized;

            // Calculate the distance the ball needs to travel
            float distanceToTravel = Vector3.Distance(initialPosition, targetPosition);

            // Calculate the speed based on the specified duration
            float calculatedSpeed = distanceToTravel / fallDuration;

            // Set the ball's velocity to move it to the drop point over the specified duration
            ballRigidbody.velocity = direction * calculatedSpeed;

            // Start a coroutine to release the socket after the specified duration
            StartCoroutine(ReleaseSocketAfterDuration(fallDuration));
        }
    }

    IEnumerator ReleaseSocketAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Code here will execute after waiting for the specified duration
        socketInteractor.socketActive = true;
        dropBall = false;
    }
}
