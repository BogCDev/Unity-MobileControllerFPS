using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

	public GameObject LeftStickMovementTrashold;
	public GameObject LeftSticky;
	public GameObject LeftMoveBaseOnDrag;

	public void RestartScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LeftStickySettings (){
		JoystickLeft jl = GameObject.Find("Joystick").GetComponent<JoystickLeft> ();
		if(LeftSticky.GetComponent<Toggle>().isOn) {
			jl.sticky = true;
			jl.Init();
		}else {
			jl.sticky = false;
			jl.Init();
		}
	}

	public void LeftMoveBaseOnDragSettings (){
		JoystickLeft jl = GameObject.Find("Joystick").GetComponent<JoystickLeft> ();
		if(LeftMoveBaseOnDrag.GetComponent<Toggle>().isOn) {
			jl.moveJoystickBaseOnDrag = true;
			jl.Init();
		}else {
			jl.moveJoystickBaseOnDrag = false;
			jl.Init();
		}
	}

	public void LeftStickMovementTrasholdSettings (){
		JoystickLeft jl = GameObject.Find("Joystick").GetComponent<JoystickLeft> ();
		jl.stickMovementThreshold = (int)LeftStickMovementTrashold.GetComponent<Slider>().value;
		jl.Init();
	}

}
