using UnityEngine;
using System.Collections;

public class GameManagerC : MonoBehaviour {

    public bool twoPlayers = false;
    public bool pausedOrMenu = true;
    public bool spawnedPlayer = false;

    public NetworkManager networkManager;

	// Use this for initialization
    void Start() {
	
	}
	
	// Update is called once per frame
    void Update() {

        if (networkManager == null && GameObject.FindGameObjectWithTag("Player") != null) {
            networkManager = (NetworkManager)GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkManager>();
        }

        //If we are in city stage and already connected
        if (Application.loadedLevelName == "CityStage" && (Network.isClient || Network.isServer) && !spawnedPlayer && networkManager != null) {
            networkManager.SpawnPlayer();
            pausedOrMenu = false;
        }

        if (!pausedOrMenu && Screen.lockCursor == false) {

            Screen.showCursor = false;
            Screen.lockCursor = true;

        }
	
	}
}
