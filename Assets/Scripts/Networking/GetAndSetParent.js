#pragma strict

var otherPlayer : GameObject;

function Start () {

}

function Update () {

}

@RPC
function SetOtherPlayerParent( parentName : String ) {
	SetOtherPlayer();
	
	otherPlayer.transform.parent = GameObject.Find( parentName ).transform;

}

@RPC
function RemoveOtherPlayerParent( parentName : String ) {
	
	otherPlayer.transform.parent = null;

}


//Set the other player's camera
function SetOtherPlayer() {
	if (otherPlayer == null) {
		otherPlayer = GameObject.FindGameObjectWithTag ("Player").transform.root.gameObject;
	}
}