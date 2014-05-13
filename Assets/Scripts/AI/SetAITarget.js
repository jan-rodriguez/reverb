#pragma strict

function Start () {

	var monster : GameObject = GameObject.FindWithTag("Monster");
	if( monster != null ){
		monster.transform.SendMessage("SetTarget", gameObject, SendMessageOptions.DontRequireReceiver);
	}

}

function Update () {

}