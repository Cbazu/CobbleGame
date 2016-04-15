#pragma strict

/********************************************************
* Author: Bret Lowe										*
* Script: PointAndFollowObject.js						*
* Purpose: This script takes in a GameObject set at		*
* design time and distances away from starting object.	*
* The GameObject that this script is attached to will	*
* follow it and point at it the entire time.			*
********************************************************/

public class PointAndFollowObject extends MonoBehaviour
{

//Programmer Variables
public var target: Transform;
public var startLocationFromTarget: Vector3;
public var lookAtTarget: boolean = true;

//Script Variables
private var lastTargetPosition: Vector3;


function Start () {
	
	//sets up this GameObject to a predetermined position away from target
	transform.position = target.transform.position + startLocationFromTarget;
	
	//records initial position of target
	lastTargetPosition = target.position;
	
}

function Update () {

	//moves GameObject same amount as target position
	transform.position += target.transform.position - lastTargetPosition;
	
    //Keeps camera looking at target if selected at Design Time
	if(lookAtTarget)
	{
	    transform.LookAt(target);
	}
	
	//updates last position for next update
	lastTargetPosition = target.transform.position;

}

//changes the state of Look at target from true to false or false to true
public function OscillateTarget() {

lookAtTarget = !lookAtTarget;

}

}