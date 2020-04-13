using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

// The purpose of this class is to handle players leaving and joining in the main menu lobby.

public class PlayerLobbyManager : MonoBehaviour {

	public static PlayerLobbyManager Instance;

	[SerializeField] Button backButton;
	[SerializeField] TMP_Text statusText;
	[SerializeField] GameObject playerSetupItemPrefab;
	[SerializeField] Transform playerSetupContainer;

	[HideInInspector] public bool allowJoining = false;

	[SerializeField] public List<PlayerSetupItem> playerSetupItems = new List<PlayerSetupItem>();

	Dictionary<int, bool> controllerIDs = new Dictionary<int, bool>();

	int readyPlayers;

	void Awake() {
		Instance = this;
	}

	public void EnableJoining() {
		SetStatusText("press <color=#FFD313>a</color> / <color=#FFD313>spacebar</color> / <color=#FFD313>return</color> to join");
		allowJoining = true;
	}

	public void DisableJoining() {
		allowJoining = false;
	}

	void AddUser(int controllerID) {
		SetStatusText(string.Format("<color=#FFD313>{0}</color> joined", controllerID == 0 || controllerID == 5 ? "keyboard" : string.Format("controller {0}", controllerID)));
		if(controllerIDs.Count == 0) {
			backButton.interactable = false;
		}
		controllerIDs.Add(controllerID, false);
		PlayerSetupItem psi = Instantiate(playerSetupItemPrefab, playerSetupContainer).GetComponent<PlayerSetupItem>();
		psi.SetUp(controllerID);
		playerSetupItems.Add(psi);
		Coach.Instance.AddUser(controllerID); // Add the user to the coach, which handles between-scenes transfer of player data
		OnReadyStatusUpdated();
	}

	public void RemoveUser(int controllerID) {
		controllerIDs.Remove(controllerID);
		Coach.Instance.RemoveUser(controllerID); // Remove the user to the coach, which handles between-scenes transfer of player data
		if(controllerIDs.Count == 0) {
			SetStatusText("press <color=#FFD313>a</color> / <color=#FFD313>spacebar</color> / <color=#FFD313>return</color> to join");
		} else {
			SetStatusText(string.Format("<color=#FFD313>{0}</color> left", controllerID == 0 || controllerID == 5 ? "keyboard" : string.Format("controller {0}", controllerID)));
		}
		if(controllerIDs.Count == 0) {
			backButton.interactable = true;
		}
		OnReadyStatusUpdated();
		for(int i = 0; i < playerSetupItems.Count; i++) {
			if(playerSetupItems[i].controllerID == controllerID) {
				playerSetupItems[i].OnLeave();
				playerSetupItems.RemoveAt(i);
				return;
			}
		}
	}

	public void OnPlayerReadyStatusChanged(int controllerID, bool ready) {
		controllerIDs[controllerID] = ready;
		OnReadyStatusUpdated();
	}

	void OnReadyStatusUpdated() {
		if(controllerIDs.Count == 0) {
			return;
		}
		bool readyToStart = true;
		for(int i = 0; i < controllerIDs.Count; i++) {
			if(!controllerIDs.ElementAt(i).Value) {
				readyToStart = false;
			}
		}

		if(readyToStart) {
			CountdownText.Instance.StartCounting(3, OnGameStart);
			SetStatusText("<color=#FFD313>game starting</color>");
		} else {
			CountdownText.Instance.StopCounting();
		}
	}

	public void OnGameStart() {
		SceneManager.LoadScene(1);
	}

	public int howManyPlayers()
	{
		return controllerIDs.Count;
	}

	void SetStatusText(string text) {
		statusText.text = text;
	}

	void Update() {
		if(!allowJoining) {
			return;
		}

		for(int i = 0; i < 6; i++) {
			if(!controllerIDs.ContainsKey(i)) {
				if(InputManager.GetButtonDown("A", i)) {
					AddUser(i);
					FindObjectOfType<AudioManager>().Play("addPlayer");
				}
			}
		}
	}
}