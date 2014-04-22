using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	string registeredGameName = "Reverb_TestServer_Tutorial";
//	bool isRefreshing = false;
	float refreshRequestLength = 3.0f;
	HostData[] hostData;
	public bool DisplayingNetworkGUI;

	readonly Vector3 PLAYER1SPAWN = new Vector3 (56.66432f, 464.0021f, 171.2229f);
	readonly Vector3 PLAYER2SPAWN = new Vector3 (113.32864f, 464.0021f, 171.2229f);

	private GameObject player1Object;
	private GameObject player2Object;

	//TODO: get this to be the server we setup
	public void Start() {
		MasterServer.ipAddress = "18.250.7.56";
	}

	//Display the overlay that the user will see when connecting/creating a server
	public void OnGUI()
	{
		DisplayingNetworkGUI = true;
		//Test display. Just show what type of connection you have
		if(Network.isServer)
		{
			GUILayout.Label("Running as a server.");
		}else if(Network.isClient)
		{
			GUILayout.Label("Running as a client");

			//Add button for clients to start server
			if(GUI.Button(new Rect(Screen.width/2 - 75f, 25f, 150f, 30f), "Spawn"))
			{
				SpawnPlayer();
				gameObject.camera.enabled = false;
			}
		}


		//Don't display buttons if we are connected
		if (Network.isClient || Network.isServer) 
		{
			DisplayingNetworkGUI = false;
			return;
		}

		//Start server button
		if (GUI.Button (new Rect (25f, 25f, 150f, 30f), "Start New Server")) 
		{
			StartServer();
		}

		//Refresing visible servers
		if (GUI.Button (new Rect (25f, 65f, 150f, 30f), "Refresh Server List")) 
		{
			StartCoroutine("RefreshHostList");
		}

		//If we find servers, display them
		if(hostData != null)
		{
			for(int i = 0; i < hostData.Length; i++)
			{
				//Create buttons for each server
				if(GUI.Button( new Rect(Screen.width/2, 65f + (30f * i), 300f, 30f), hostData[i].gameName ) )
				{
					Debug.Log("Connecting to server");
					//Connect to the button clicked
					Network.Connect (hostData[i]);

				}
				      
			}
		}
	}

	// Respawn players if they fall
	void FixedUpdate() {

		// Spawn player in correct location if they fall off the map
		if (player1Object != null) {
			if (player1Object.transform.position.y < 400) {
				player1Object.transform.position = PLAYER1SPAWN;
			}
		}
		if (player2Object != null) {
			if (player2Object.transform.position.y < 400) {
				player2Object.transform.position = PLAYER2SPAWN;
			}
		}
	}

	private void StartServer()
	{
		//Initialize server with up to 2 players, port 25002, and no NAT
		Network.InitializeServer(2, 23466, false);
		MasterServer.RegisterHost(registeredGameName, "Reverb Server", "Just testing");
	}

	//Get list of hosts currently running our game
	public IEnumerator RefreshHostList()
	{
		Debug.Log ("Refreshing...");
		MasterServer.RequestHostList (registeredGameName);

		float timeEnd = Time.time + refreshRequestLength;

		while (Time.time < timeEnd) {
			hostData = MasterServer.PollHostList ();
			yield return new WaitForEndOfFrame();
		}

		if(hostData == null || hostData.Length == 0)
		{
			Debug.Log ("No servers found");
		}else{
			Debug.Log ("Found " + hostData.Length + " servers");
		}

	}

	//Spawn a player on top of the building
	private void SpawnPlayer()
	{
		Debug.Log ("Spawning player");



		Object playerPrefab = Resources.Load ("Prefabs/FirstPersonController");
		if(playerPrefab != null)
		{
			//Spawn player in correct location
			if( Network.isServer){
				player1Object = (GameObject)Network.Instantiate (playerPrefab, PLAYER1SPAWN, Quaternion.identity, 0);
			} else {
				player2Object = (GameObject)Network.Instantiate (playerPrefab, PLAYER2SPAWN, Quaternion.identity, 0);
			}
		}else{
			Debug.Log("error getting prefab");
		}
	}

	//----------------------------Call Backs from client and server---------------------------

	void OnServerInitialized()
	{
		Debug.Log ("Server has been initialized");
		SpawnPlayer ();
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.RegistrationSucceeded)
		{
			Debug.Log ("Registration Successful");
		}
	}

	//Clean up player's stuff
	void OnPlayerDisconnect(NetworkPlayer player)
	{
		Debug.Log ("Player disconnected from: " + player.ipAddress + ":" + player.port);
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}

	void OnApplicationQuit()
	{
		//Have the sever disconnect and remove it from the lists of hosts
		if(Network.isServer)
		{
			Network.Disconnect (200);
			MasterServer.UnregisterHost();
		}

		if(Network.isClient)
		{
			Network.Disconnect(200);
		}
	}

}
