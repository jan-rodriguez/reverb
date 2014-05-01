#pragma strict


function Start () {

	Screen.showCursor = false;

}

function Update () {

	if(Input.GetKeyDown("escape")) {
	
		Screen.showCursor = !Screen.showCursor;
		Screen.lockCursor = !Screen.lockCursor;
	
	}
}
