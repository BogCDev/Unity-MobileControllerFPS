using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickRight : MonoBehaviour {

	public GameObject stick;
	private RectTransform stickRectTransform;
	public GameObject backgroundImage;
	[Range(1, 3)]
	public float sensitivity = 1;
	private int stickMovement = 0;
	public static float positionX;
	public static float positionY;
	public float rotationYMaxAngle = 45;

	public GameObject shotButton;
	private float shotButtonStartPosX;
	private float shotButtonStartPosY;

	public static bool shot = false;
	public static bool jump = false;

	private float tempPositionX = 0;
	private float tempPositionY = 0;

	public static float rotX = 0;
	public static float rotY = 0;

	private float startX = 0;
	private float startY = 0;

	void Start() {	
		shotButtonStartPosX = shotButton.transform.position.x;
		shotButtonStartPosY = shotButton.transform.position.y;	
		Init();
	}

	public void Init() {
		stickMovement = 4 * (Screen.width + Screen.height) / 100;
		stickRectTransform = stick.GetComponent<RectTransform>();
	}

	public void OnStartMoving(BaseEventData data) {
		PointerEventData pointerData = data as PointerEventData;
		startX = backgroundImage.transform.position.x;
		startY = backgroundImage.transform.position.y;
	}
	
	public void Move(BaseEventData data) {
		PointerEventData pointerData = data as PointerEventData;

		float joysticXkBaseMovement = backgroundImage.transform.position.x - (stick.transform.position.x - pointerData.position.x);
		float joystickYBaseMovement = backgroundImage.transform.position.y - (stick.transform.position.y - pointerData.position.y);

		positionX = -(startX - pointerData.position.x) / stickMovement;
		positionY = -(startY - pointerData.position.y) / stickMovement;

		positionX *= sensitivity;
		positionY *= sensitivity;

		CalcRotation();
		backgroundImage.transform.position = new Vector2(joysticXkBaseMovement, joystickYBaseMovement);
	}

	public void ReturnToNormalPosition() {
		stickRectTransform.anchoredPosition = new Vector2(0,0);
		positionX = 0;
		positionY = 0;
	}


	public void OnStickyPointerDown(BaseEventData data) {
		PointerEventData pointerData = data as PointerEventData;
		backgroundImage.transform.position = pointerData.position;
	}

	public void OnStickyPointerUp(BaseEventData data) {
		ReturnToNormalPosition();
		CalcRotation();
	}

	private void CalcRotation () {
		if(positionX != tempPositionX) {
			if(tempPositionX == 0) tempPositionX = JoystickRight.positionX;
			if(JoystickRight.positionX == 0) tempPositionX = 0;
			
			rotX -= (tempPositionX - positionX) * 10;
			if(rotX > 360) rotX = rotX - 360;
			if(rotX < -360) rotX = rotX + 360;
			
			tempPositionX = positionX;
		}

		if(positionY != tempPositionY) {
			if(tempPositionY == 0) tempPositionY = positionY;
			if(positionY == 0) tempPositionY = 0;
		
			rotY += (tempPositionY - positionY) * 10;
			if(rotY > rotationYMaxAngle) rotY = rotationYMaxAngle;
			if(rotY < -rotationYMaxAngle) rotY = -rotationYMaxAngle;
			
			tempPositionY = positionY;
		}
	}

	public void ShotPress(BaseEventData data) {
		PointerEventData pointerData = data as PointerEventData;
		shotButton.transform.position = pointerData.position;
		shot = true;
    }

    public void ShotRelease() {
		shot = false;
		shotButton.transform.position = new Vector2(shotButtonStartPosX, shotButtonStartPosY);
    }

}
