using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

	[SerializeField] bool allowMultiple;
	[SerializeField] MenuInfo[] menues;
	MenuInfo activeMenu;
	List<MenuInfo> menuPath = new List<MenuInfo>();

	public GameObject mainFirstButton;  //store the the button that should be selected when the main menu is active

	private GameObject activeSelection;

	void Start() {
		RequestMenu("main");
	}

	public void RequestMenu(string menuName) {
		if(activeMenu != null) {
			if(menuName == activeMenu.name) {
				return;
			} else {
				activeMenu.Exit();
			}
		}
		for(int i = 0; i < menues.Length; i++) {
			if(menues[i].name == menuName) {
				StartCoroutine(MenuDelay(1f));
				activeMenu = menues[i];
				activeMenu.Enter();
				menuPath.Add(activeMenu);
			}
		}
	}

	public void HideMenu(string menuName) {
		for(int i = 0; i < menues.Length; i++) {
			if(menues[i].name == menuName) {
				menues[i].Exit();
			}
		}
	}

	public void Back() {
		if(menuPath.Count <= 1) {
			return;
		}

		MenuInfo last = menuPath[menuPath.Count - 1];
		MenuInfo secondToLast = menuPath[menuPath.Count - 2];

		last.Exit();
		secondToLast.Enter();

		menuPath.RemoveAt(menuPath.Count - 1);
		activeMenu = secondToLast;

		//clear the selected object
		EventSystem.current.SetSelectedGameObject(null);
		//set the selected menu button
		EventSystem.current.SetSelectedGameObject(mainFirstButton);
	}

	void Update() {
		if(Input.GetButtonDown("Cancel") && PlayerLobbyManager.Instance.howManyPlayers() == 0) {
			Back();
		}
	}

	public void QuitGame() {
		Application.Quit();
	}

	//Use this to stop the instant transition somehow??
	IEnumerator MenuDelay(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}

}

[System.Serializable]
public class MenuInfo {
	public string name;
	public CanvasGroup canvasGroup;
	public Selectable selectable;

	public void Enter() {
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
		canvasGroup.alpha = 1;
	}

	public void Exit() {
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
		canvasGroup.alpha = 0;
	}

}
