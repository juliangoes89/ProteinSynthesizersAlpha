using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileButtonArrowUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	private bool isUpArrowPressed = false;

	Movement movement;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		if (isUpArrowPressed) {
			GameObject currentRocket = GameObject.FindWithTag("CurrentRocket");
			movement = currentRocket.GetComponent<Movement>();
			movement.ProcessThrust(isUpArrowPressed);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{ 
		isUpArrowPressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isUpArrowPressed = false;
	}

}
