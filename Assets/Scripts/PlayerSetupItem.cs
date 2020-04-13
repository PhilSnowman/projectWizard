using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSetupItem : MonoBehaviour {

	[SerializeField] TMP_Text titleText;
	[SerializeField] Image backgroundImage;
	[SerializeField] Color backgroundReadyColor;
	[SerializeField] Color borderReadyColor;
	Color backgroundDefaultColor;
	Color borderDefaultColor;
	[SerializeField] Image readyImage;
	[SerializeField] Image backImage;
	[SerializeField] Image deviceImage;
	[SerializeField] Image borderImage;
	[SerializeField] Sprite keyboardSprite;
	[SerializeField] Sprite controllerSprite;

	[SerializeField] List<Sprite> characterSelection;
	[HideInInspector] public int characterIndex = 0;

	[HideInInspector] public int controllerID;

	bool removed;
	bool ready;

	//Variables for reading Dpad input as a button
	public static bool IsLeft, IsRight, IsUp, IsDown;
	private float _LastX, _LastY;

	void Awake() {
		backgroundDefaultColor = backgroundImage.color;
		borderDefaultColor = borderImage.color;
	}

	public void SetUp(int _controllerID) {
		controllerID = _controllerID;
		titleText.text = string.Format("- player {0} -\n{1}", controllerID, controllerID == 0 || controllerID == 5 ? "keyboard" : "controller");
		readyImage.sprite = InputHelper.Instance.GetSpriteForButton("A", controllerID);
		backImage.sprite = InputHelper.Instance.GetSpriteForButton("B", controllerID);
		deviceImage.sprite = characterSelection[characterIndex];
	}

	//Read input from the controller
	void Update() 
	{

		if (!ready)
		{
			DpadInput();
			SelectCharacter();
		}

		//Debug.Log("Player " + controllerID + " selects " + Coach.Instance.playerCharacters[characterIndex]);

		if (removed)
			return;

		if(InputManager.GetButtonDown("B", controllerID)) {
			if(ready) {
				SetReadyState(false);
				FindObjectOfType<AudioManager>().Play("leave");
			} else if (!ready){
				PlayerLobbyManager.Instance.RemoveUser(controllerID);
				FindObjectOfType<AudioManager>().Play("leave");
			}
			else
			{
				//MenuManager.Back();
				Debug.Log("go back to main menu");
			}
		} else if(InputManager.GetButtonDown("A", controllerID) && !ready) {
			SetReadyState(true);
		}
	}

	//Read Horizontal Axis as GetButtonDown()
	void DpadInput()
	{
		float x = InputManager.GetAxis("Horizontal", controllerID);
		float y = InputManager.GetAxis("Vertical", controllerID);

		IsLeft = false;
		IsRight = false;
		IsUp = false;
		IsDown = false;

		if (_LastX != x)
		{
			if (x == -1)
				IsLeft = true;
			else if (x == 1)
				IsRight = true;
		}

		if (_LastY != y)
		{
			if (y == -1)
				IsDown = true;
			else if (y == 1)
				IsUp = true;
		}

		_LastX = x;
		_LastY = y;
	}

	void SelectCharacter()
	{
		//if (InputManager.GetButtonDown("Horizontal", controllerID))
		if (IsRight)
		{
			characterIndex += 1;
			if (characterIndex >= characterSelection.Count)
			{
				characterIndex = 0;
			}
			deviceImage.sprite = characterSelection[characterIndex];
			Debug.Log("player " + controllerID + " selected " + characterSelection[characterIndex]);
		}
		//else if (InputManager.GetButtonDown("Horizontal", controllerID))
		else if (IsLeft)
		{
			characterIndex -= 1;
			if (characterIndex <= -1)
			{
				characterIndex = characterSelection.Count - 1;
			}
			deviceImage.sprite = characterSelection[characterIndex];
			Debug.Log("player " + controllerID + " selected " + characterSelection[characterIndex]);
		}
	}


	public void OnLeave() {
		removed = true;
		Destroy(gameObject);
	}

	void SetReadyState(bool _ready) {
		ready = _ready;
		PlayerLobbyManager.Instance.OnPlayerReadyStatusChanged(controllerID, ready);
		if(ready) {
			deviceImage.color = Color.white;
			borderImage.color = borderReadyColor;
			backgroundImage.color = backgroundReadyColor;
			titleText.text = string.Format("- <color=#FFD313>player {0}</color> -\n{1}", controllerID, controllerID == 0 || controllerID == 5 ? "keyboard" : "controller");
			FindObjectOfType<AudioManager>().Play("readyUp");
		} else {
			deviceImage.color = Color.white;
			backgroundImage.color = backgroundDefaultColor;
			borderImage.color = borderDefaultColor;
			titleText.text = string.Format("- <color=#FFFFFF>player {0}</color> -\n{1}", controllerID, controllerID == 0 || controllerID == 5 ? "keyboard" : "controller");
		}
	}
}