#pragma strict

var goal : Transform;

function Start () {

}

function Update () {
	
	if(goal != null){
		GetComponent(NavMeshAgent).destination = goal.position;
	}

}

function SetTarget(target : GameObject){
	goal = target.transform;
}