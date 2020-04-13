using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The purpose of this class is to carry controllerIDs through scenes.

public class Coach : MonoBehaviour {

	public static Coach Instance;

	[HideInInspector] public List<int> controllerIDs = new List<int>();

	//This is a list of the player prefabs, persists through scenes.
	[SerializeField] public List<GameObject> playerCharacters;


	void Awake() {
		if(Instance) {
			Destroy(gameObject);
		} else {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void AddUser(int controllerID) {
		controllerIDs.Add(controllerID);
	}

	public void RemoveUser(int controllerID) {
		controllerIDs.Remove(controllerID);
	}
}