using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public bool isPaused = false;
	public string currentMenu;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
			TogglePause();
			Screen.showCursor = true;
		} else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) {
			TogglePause();

			Screen.showCursor = false;
		}
	}

	void OnGUI() {
		if(currentMenu == "Paused") {
			PausedMenu();
		} else if(currentMenu == "Options") {
			OptionsMenu();
		}
	}


	/// <summary>
	/// Toggles the pause.
	/// </summary>
	public void TogglePause() {
		isPaused = !isPaused;
		if(isPaused) {
			Display("Paused");
		} else {
			Display(null);
		}
	}

	/// <summary>
	/// Display the specified menu.
	/// </summary>
	/// <param name="menu">Name of the menu to be displayed. May be null.</param>
	public void Display(string menu) {
		currentMenu = menu;
	}

	public void PausedMenu() {
		GUI.Label (new Rect(10, 10, 200, 50), "Paused");
		if(GUI.Button(new Rect(10, 70, 200, 50), "Options")) {
			Display("Options");
		}
	}

	public void OptionsMenu() {
		GUI.Label (new Rect(10, 10, 200, 50), "Options");
		if(GUI.Button(new Rect(10, 70, 200, 50), "Back")) {
			Display("Paused");
		}
	}

}
