using UnityEngine;

public class Mover : MonoBehaviour
{
	[SerializeField] float moveSpeed = -0.1f;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float xValue = Time.deltaTime * moveSpeed;

		transform.Translate(xValue, 0, 0);
	}

}
