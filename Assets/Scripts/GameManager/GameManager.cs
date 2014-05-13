using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	GameObject player;
	readonly static Vector3 PLAYERSPAWN = new Vector3 (56.66432f, 464.0021f, 171.2229f);
	readonly static Vector3 PLAYERCITYSPAWN = new Vector3 (86.56039f, 335.4092f, 210.7125f);
	private bool spawned = false;


	// Use this for initialization
	void Start () {
		Screen.showCursor = true;
		Screen.lockCursor = false;
		if (Application.loadedLevelName == "CityStage") {
			SpawnPlayer();
		}
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
		Screen.showCursor = false;
		Screen.lockCursor = true;
		Object playerPrefab = Resources.Load ("Prefabs/FirstPersonController");
		Vector3 playerSpawnLocation = (Application.loadedLevelName == "CityStage") ? PLAYERCITYSPAWN : PLAYERSPAWN;
		player = (GameObject)GameObject.Instantiate (playerPrefab, playerSpawnLocation, Quaternion.identity);
		spawned = true;
	}

	// Respawn players if they fall
	void FixedUpdate() {
		
		// Spawn player in correct location if they fall off the map
		if (player != null) {
			if (player.transform.position.y < 300) {
				player.transform.position = (Application.loadedLevelName == "CityStage" ? PLAYERCITYSPAWN : PLAYERSPAWN);
			}
		}
	}
}
