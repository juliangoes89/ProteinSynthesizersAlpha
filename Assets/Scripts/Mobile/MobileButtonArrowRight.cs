using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileButtonArrowRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	private bool isRightArrowPressed = false;

	Movement movement;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		if (isRightArrowPressed) {
			GameObject currentRocket = GameObject.FindWithTag("CurrentRocket");
			movement = currentRocket.GetComponent<Movement>();
			movement.ProcessRotation(false,isRightArrowPressed);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{ 
		isRightArrowPressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isRightArrowPressed = false;
	}

}
