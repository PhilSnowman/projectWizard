using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* controllerID info:
 * 
 * 0: keyboard
 * 1: controller
 * 2: controller
 * 3: controller
 * 4: controller
 * 5: keyboard
 * 
 * This can be adjusted in the input tab of the project settings
 */

public class InputManager : MonoBehaviour {

	public static bool GetButtonDown(string buttonName, int controller) {
		return Input.GetButtonDown(string.Format("J{0}{1}", controller, buttonName));
	}

	public static bool GetButtonUp(string buttonName, int controller) {
		return Input.GetButtonUp(string.Format("J{0}{1}", controller, buttonName));
	}

	public static bool GetButton(string buttonName, int controller) {
		return Input.GetButton(string.Format("J{0}{1}", controller, buttonName));
	}

	public static float GetAxis(string axisName, int controller) {
		return Input.GetAxisRaw(string.Format("J{0}{1}", controller, axisName));
	}
}