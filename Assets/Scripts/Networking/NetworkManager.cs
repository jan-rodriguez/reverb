using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	string registeredGameName = "Reverb_TestServer_Tutorial";
//	bool isRefreshing = false;
	float refreshRequestLength = 3.0f;
	HostData[] hostData;

	//Display the overlay that the user will see when connecting/creating a server
	public void OnGUI()
	{

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
			}
		}


		//Don't display buttons if we are connected
		if (Network.isClient || Network.isServer) 
		{
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

	private void StartServer()
	{
		//Initialize server with up to 2 players, port 25002, and no NAT
		Network.InitializeServer(2, 25002, false);
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
			Network.Instantiate (playerPrefab, new Vector3 (145.5427f, 373.8164f, 225.1191f), Quaternion.identity, 0);
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
