using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class sets up the players at the start of the game, assigning them which controller they will use.

public class GameManager : MonoBehaviour {

	[SerializeField] GameObject playerPrefab;

	//need to access the "CharacterIndex" variable from each player to select a prefab from "playerCharacters"

	int selectedPlayer;

	List<PlayerSetupItem> PSIs = PlayerLobbyManager.Instance.playerSetupItems;  //This is a list of the "PlayerSetupItem"s kept inside of the "PlayerLobbyManager".  "PlayerSetupItem" holds the "characterIndex" which chooses a prefab from the list of available players.

	void Start() {
		for(int i = 0; i < Coach.Instance.controllerIDs.Count; i++) {
			selectedPlayer = PSIs[i].characterIndex;
			Instantiate(/*playerPrefab*/Coach.Instance.playerCharacters[selectedPlayer], Vector3.up, Quaternion.identity).GetComponent<TopDownPlayerController>().SetUp(Coach.Instance.controllerIDs[i]);
			
			//Instantiate "player prefab" at "position" and "rotation" then Get the "TopDownPlayerController" component and use the "SetUp" method for each "controllerID" in the "controllerIDs" list inside of the "Coach"
		}
	}
}