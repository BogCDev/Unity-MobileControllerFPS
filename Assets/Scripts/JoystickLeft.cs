using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickLeft : MonoBehaviour {

	public GameObject stick;
	private RectTransform stickRectTransform;
	public GameObject backgroundImage;
	public GameObject leftAreaForStickyJoystick;
	[Range(1, 10)]
	public int stickMovementThreshold = 4;
	private int stickMovement = 0;
	public bool sticky = false;
	public bool moveJoystickBaseOnDrag = false;
	public static float positionX;
	public static float positionY;

	void Start() {		
		Init();
	}

	public void Init() {
		stickMovement = stickMovementThreshold * (Screen.width + Screen.height) / 100;
		stickRectTransform = stick.GetComponent<RectTransform>();

		if(sticky) {
			backgroundImage.SetActive(false);
			stick.SetActive(false);
			leftAreaForStickyJoystick.SetActive(true);
		}else {
			backgroundImage.SetActive(true);
			stick.SetActive(true);
			leftAreaForStickyJoystick.SetActive(false);
		}
	}

	
	public void Move(BaseEventData data) {
		PointerEventData pointerData = data as PointerEventData;

		float x = backgroundImage.transform.position.x - pointerData.position.x;
		float y = backgroundImage.transform.position.y - pointerData.position.y;

		float angle = Mathf.Atan2(x, y);

		float joystickXPosition = x;
		float joystickYPosition = y;

		if(Vector2.Distance(backgroundImage.transform.position, pointerData.position) > stickMovement) {
			joystickXPosition = stickMovement * Mathf.Sin(angle);
			joystickYPosition = stickMovement * Mathf.Cos(angle);
		}

		positionX = -joystickXPosition / stickMovement;
		positionY = -joystickYPosition / stickMovement;

		stick.transform.position = new Vector2(backgroundImage.transform.position.x - joystickXPosition, backgroundImage.transform.position.y - joystickYPosition);

		if(moveJoystickBaseOnDrag) {
			float joysticXkBaseMovement = backgroundImage.transform.position.x - (stick.transform.position.x - pointerData.position.x);
			float joystickYBaseMovement = backgroundImage.transform.position.y - (stick.transform.position.y - pointerData.position.y);
			backgroundImage.transform.position = new Vector2(joysticXkBaseMovement, joystickYBaseMovement);
		}
	}

	public void ReturnToNormalPosition() {
		stickRectTransform.anchoredPosition = new Vector2(0,0);
		positionX = 0;
		positionY = 0;
	}

	//Methods bellow are used if sticky joystick option is enabled
	public void OnStickyPointerDown(BaseEventData data) {
		PointerEventData pointerData = data as PointerEventData;
		backgroundImage.SetActive(true);
		stick.SetActive(true);
		backgroundImage.transform.position = pointerData.position;
	}

	public void OnStickyPointerUp(BaseEventData data) {
		ReturnToNormalPosition();
		backgroundImage.SetActive(false);
		stick.SetActive(false);
	}
}
