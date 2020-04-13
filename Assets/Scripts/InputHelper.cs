using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The purpose of this class is to provide button sprites for control info.
// For example, in the main menu when a player joins they can see the A and B button controls.
// You can assign a sprite in the inspector after clicking on the object this script is on.

public class InputHelper : MonoBehaviour {

	public static InputHelper Instance;

	public ButtonSprite[] buttonSprites;

	void Awake() {
		if(Instance) {
			Destroy(gameObject);
		} else {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public Sprite GetSpriteForButton(string buttonName, int controllerID) {
		for(int i = 0; i < buttonSprites.Length; i++) {
			if(buttonSprites[i].name == buttonName) {
				for(int a = 0; a < buttonSprites[i].controllerButtonSprites.Length; a++) {
					if(buttonSprites[i].controllerButtonSprites[a].controllerID == controllerID) {
						return buttonSprites[i].controllerButtonSprites[a].sprite;
					}
				}
				return buttonSprites[i].controllerButtonSprites[1].sprite;
			}
		}
		return null;
	}
}

[System.Serializable]
public class ButtonSprite {
	public string name;
	public ControllerButtonSprite[] controllerButtonSprites;
}

[System.Serializable]
public class ControllerButtonSprite {
	public int controllerID;
	public Sprite sprite;
}