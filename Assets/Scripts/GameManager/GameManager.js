#pragma strict

function Start () {

	Screen.showCursor = true;

}

function Update () {

	if(Input.GetKeyDown("escape")) {
	
		Screen.showCursor = !Screen.showCursor;
	
	}

}
