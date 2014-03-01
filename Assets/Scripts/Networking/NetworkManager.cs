using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	string registeredGameName = "Reverb_TestServer_Tutorial";
//	bool isRefreshing = false;
	float refreshRequestLength = 3.0f;
	HostData[] hostData;

	public void OnGUI()
	{

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
					//Connect to the button clicked
					Network.Connect (hostData[i]);
				}
				      
			}
		}
	}

	void OnServerInitialized()
	{
		Debug.Log ("Server has been initialized");

	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.RegistrationSucceeded)
		{
			Debug.Log ("Registration Successful");
		}
	}

	private void StartServer()
	{
		//Initialize server with up to 2 players, port 25002, and no NAT
		Network.InitializeServer(2, 25002, false);
		MasterServer.RegisterHost(registeredGameName, "Reverb Server", "Just testing");
	}

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
}
