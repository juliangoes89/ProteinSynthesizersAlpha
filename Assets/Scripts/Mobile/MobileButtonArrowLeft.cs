using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileButtonArrowLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	private bool isLeftArrowPressed = false;

	Movement movement;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		if (isLeftArrowPressed) {
			GameObject currentRocket = GameObject.FindWithTag("CurrentRocket");
			movement = currentRocket.GetComponent<Movement>();
			movement.ProcessRotation(isLeftArrowPressed);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{ 
		isLeftArrowPressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isLeftArrowPressed = false;
	}

}
