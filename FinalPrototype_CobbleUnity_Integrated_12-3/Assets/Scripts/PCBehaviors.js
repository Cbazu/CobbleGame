#pragma strict

import System.Linq;

/********************************************************
* Author: Bret Lowe										*
* Script: PointAndFollowObject.js						*
* Purpose: This script takes in a GameObject set at		*
* design time and distances away from starting object.	*
* The GameObject that this script is attached to will	*
* follow it and point at it the entire time.			*
********************************************************/

@script RequireComponent(BoxCollider);

//variables
//script variables
private var interactiveObject: GameObject;
private var myBoxCollider: BoxCollider;
private var isItemPickedUp: boolean;
private var pickUpDelay: float;
private var pickUpTime: float;

function Start () {

	//initialize array of interactive objects
	myBoxCollider = this.gameObject.GetComponent(BoxCollider);
	
	//isItemPickedUp to false
	isItemPickedUp = false;
	pickUpDelay = 0.25f;
	pickUpTime = 1.0f;
	
}


function Update () {

	//how long since last pickup
	pickUpTime += Time.deltaTime;
	
	//check for interact key press
	if(pickUpTime >= pickUpDelay)
	{
		if(Input.GetButton("Interact"))
		{
			if(isItemPickedUp)
			{
				SetDown();
			}
			else
			{
				PickUp();
			}
		}
		pickUpTime = 0.0f;
	}
}

private function PickUp(){

	Debug.Log(pickUpTime);
	if(interactiveObject != null)
	{
		//interactiveObject.transform.position = myBoxCollider.transform.position;
		interactiveObject.transform.parent=this.transform.parent;
		isItemPickedUp = true;
		pickUpTime = 0.0f;
		Debug.Log("Can Pick Up Object");
	}
	
}

private function SetDown(){

	if(pickUpTime >= pickUpDelay)
	{
		interactiveObject.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform.parent);
		isItemPickedUp = false;
	}
}

/*
//Returns the InstanceID of the closet object within a specified bounding box and returns the
//ID of the object
private function FindClosestInteractiveObject():GameObject{

	//variables
	var distanceFromObject: float = 100;
	var closestGameObject: GameObject;
	
	//main code
	//locates the distance
	for(var obj:GameObject in allInteractiveObjects)
	{
		if(Vector3.Distance(obj.transform.position, this.transform.parent.position) < distanceFromObject)
		{
			distanceFromObject = Vector3.Distance(obj.transform.position, this.transform.parent.position);
			closestGameObject = obj;	
		}
	}
	return closestGameObject;
}
*/

//Adds Interactive Objects to array that enter the trigger area
function OnTriggerEnter(other: Collider)
{
	
	//add name of interactive object to interactive array as long as collider is not a trigger
	if(!other.isTrigger && (other.gameObject.tag == "IntObject"))
	{
		//add object to array
		interactiveObject = other.gameObject;
		Debug.Log(interactiveObject.gameObject.name);
		
	}
}

//Deletes Interactive Objects from array that leave trigger area
function OnTriggerExit(other: Collider)
{
	//If object is in the array remove it from the arrayList
	if(!other.isTrigger && (other.tag == "IntObject"))
	{
		interactiveObject=null;
	}
	
}
	
	


