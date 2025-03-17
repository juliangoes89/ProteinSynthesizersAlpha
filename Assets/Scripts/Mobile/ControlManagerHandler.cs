using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlManagerHandler : MonoBehaviour
{
    public Button arrowLeft;
    public Button arrowRight;
    public Button arrowUp;

    public Button toggleControlsButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggleControlsButton.onClick.AddListener(ToggleControls);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ToggleControls()
	{
		arrowLeft.gameObject.SetActive(!arrowLeft.gameObject.activeSelf);
		arrowRight.gameObject.SetActive(!arrowRight.gameObject.activeSelf);
		arrowUp.gameObject.SetActive(!arrowUp.gameObject.activeSelf);
		EventSystem.current.SetSelectedGameObject(null);
	}
}
