#pragma strict

var goal : Transform;
var nav : NavMeshAgent;

function Start () {

}

function Update () {
	
	if(goal != null){
		nav.destination = goal.position;
	}

}

function SetTarget(target : GameObject){
	nav = GetComponent(NavMeshAgent);
	goal = target.transform;
}