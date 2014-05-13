using UnityEngine;
using System.Collections;

public class Sign : Activateable {

    bool GUIEnabled = false;
    public string text = "This is some sign text.";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnActivation() {

        Debug.Log("Activated Sign");

        GUIEnabled = true;

    }

    void OnGUI() {

        if (GUIEnabled) {

            GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, 2 * Screen.width / 4, 2 * Screen.height / 4), text, "textarea");

            GUI.Label(new Rect(Screen.width / 4, 3 * Screen.height / 4, 2 * Screen.width / 4, 30), "Press Q to close.", "textarea");

            if (Input.GetKeyDown("q")) {
                GUIEnabled = false;
            }

        }

    }
}
