using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public Button buttonLeftArrow;
	public Button buttonRightArrow;
	public Button buttonUpArrow;

	private bool isLeftArrowPressed = false;
	private bool isRightArrowPressed = false;
	private bool isUpArrowPressed = false;

	Movement movement;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		buttonLeftArrow.onClick.AddListener(() => { });
		buttonRightArrow.onClick.AddListener(() => {});
		buttonUpArrow.onClick.AddListener(() => { });
		movement = GetComponent<Movement>();
	}

    // Update is called once per frame
    void Update()
    {
		if (isLeftArrowPressed)
		{
			movement.Thrust();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		// Verificar qué botón fue presionado
		if (eventData.pointerPress == buttonLeftArrow.gameObject)
		{
			isLeftArrowPressed = true;
		}
		else if (eventData.pointerPress == buttonRightArrow.gameObject)
		{
			isRightArrowPressed = true;
		}
		else if (eventData.pointerPress == buttonUpArrow.gameObject)
		{
			isUpArrowPressed = true;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		// Verificar qué botón dejó de ser presionado
		if (eventData.pointerPress == buttonLeftArrow.gameObject)
		{
			isLeftArrowPressed = false;
		}
		else if (eventData.pointerPress == buttonRightArrow.gameObject)
		{
			isRightArrowPressed = false;
		}
		else if (eventData.pointerPress == buttonUpArrow.gameObject)
		{
			isUpArrowPressed = false;
		}
	}


}
