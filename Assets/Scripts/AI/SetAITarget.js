#pragma strict

function Start () {

	var monster : GameObject = GameObject.FindWithTag("Monster");
	
	monster.transform.SendMessage("SetTarget", gameObject, SendMessageOptions.DontRequireReceiver);

}

function Update () {

}