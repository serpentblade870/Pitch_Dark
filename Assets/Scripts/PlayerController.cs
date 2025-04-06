using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float turnSpeed = 720f; // Speed of turning
    public AudioClip WalkSound;

    void Update()
    {
        // Get input for movement
        float moveVertical = 0f;
        float moveHorizontal = 0f;

        // Desktop input
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");

        // Mobile input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                if (WalkSound != null)
                {
                    AudioSource.PlayClipAtPoint(WalkSound, transform.position);
                }
                moveHorizontal = touch.deltaPosition.x > 0 ? 1 : -1; // Simple left/right control
            }

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                moveVertical = touch.deltaPosition.y > 0 ? 1 : -1; // Forward/backward control
            }
        }

        // Get the camera's forward direction
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Flatten the forward vector to ignore the y component
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Calculate forward movement direction
        Vector3 moveDirection = forward * moveVertical;

        // Move the character forward and backward
        controller.Move(moveDirection * speed * Time.deltaTime);

        // Rotate only based on left/right input, affecting only the Z-axis
        if (moveHorizontal != 0)
        {
            float rotationZ = moveHorizontal * turnSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationZ);
        }
    }
}
