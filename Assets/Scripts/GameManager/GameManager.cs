using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	readonly static Vector3 PLAYERSPAWN = new Vector3 (56.66432f, 464.0021f, 171.2229f);
	readonly static Vector3 PLAYERCITYSPAWN = new Vector3 (86.56039f, 335.4092f, 210.7125f);
	private bool spawned = false;


	// Use this for initialization
	void Start () {
		Screen.showCursor = true;
		Screen.lockCursor = false;
	}

	public void OnGUI () {
		if (!spawned) {
			if (GUI.Button ( new Rect ( Screen.width / 2  - 150f, Screen.height / 2 - 15f, 300f, 30f), "Start Game" )){
				SpawnPlayer ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown("escape")) {
			
			Screen.showCursor = !Screen.showCursor;
			Screen.lockCursor = !Screen.lockCursor;
			
		}
	
	}

	void SpawnPlayer () {
		Object playerPrefab = Resources.Load ("Prefabs/FirstPersonController");
		GameObject.Instantiate (playerPrefab, PLAYERSPAWN, Quaternion.identity);
		spawned = true;
	}
}
