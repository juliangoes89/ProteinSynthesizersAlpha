using UnityEngine;

public class PlayerTwoMover : MonoBehaviour
{
    public float speed = 50.0f; // Speed of the movement
	public float maxSpeed = 100.0f; // Maximum speed limit
	public float dampingFactor = 0.1f; // Damping factor to smooth out the movement

	private Vector3 movement;
	// Start is called once before the first execution of Update after the MonoBehaviour is created

	// Update is called once per frame
	void Update()
    {
		ProcessHorizontalMovement();

	}

	void ProcessHorizontalMovement()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			ApplyHorizontalMovement(-1f);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			ApplyHorizontalMovement(1f);
		}
	}
	private void ApplyHorizontalMovement(float horizontalInput)
	{
		movement = new Vector3(horizontalInput, 0, 0) * speed * Time.deltaTime; // Calculate movement vector with damping

		// Apply damping factor
		movement = Vector3.Lerp(Vector3.zero, movement, dampingFactor);

		// Clamp the movement speed to avoid moving too fast
		movement = Vector3.ClampMagnitude(movement, maxSpeed * Time.deltaTime);

		// Move the object
		transform.Translate(movement);
	}
}
