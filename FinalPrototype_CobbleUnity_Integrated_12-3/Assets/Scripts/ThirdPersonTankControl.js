#pragma strict

/********************************************************
* Author: Bret Lowe										*
* Script: ThirdPersonTankControl.js						*
* Purpose: This script implements tank controls using	*
* vertical axis for forward and backward movement and	*
* horizontal axis for turning right or left.			*
********************************************************/

@script RequireComponent(BoxCollider);

//Design time variables
public var speed: float;
public var rotationSpeed: float;
public var grabObjectKey: KeyCode;
public var jumpKey: KeyCode;
public var maxClimbAngle: float;
/*
<<<<<<< HEAD

=======
>>>>>>> refs/remotes/origin/master
*/

//public var maxJumpTime: float = 1.5; 
public var thrust: float = 1000;

//Script Variables
private var forwardMovement: float;
private var moveForward: Vector3;
private var rotateObject: float;
private var rotationAngle: Quaternion;
private var myTransform: Transform;
private var currentRotation: float = 0;
private var rb: Rigidbody;
private var distToGround: float;
private var speedMultiplier: float = 10000;
private var lastGroundState: boolean;
private var climbPercent: float;
//private var jumpTime: float = 0.0;
//private var groundedChange: boolean = true;

//triggered object to grab so only 1 object is grabbed at a time
private var objectToGrab: GameObject = null;

function Start () {
	
	//Caching transform
	myTransform = this.transform;
	rb = GetComponent.<Rigidbody>();
	distToGround = GetComponent.<Collider>().bounds.extents.y;
	lastGroundState = IsGrounded();
	
	
}

function FixedUpdate() {

	//Receive inputs from player
	forwardMovement = Input.GetAxis("Vertical");
	//rotateObject = Mathf.Clamp(Input.GetAxis("Horizontal"),-1.0,1.0);
	rotateObject = Input.GetAxis("Horizontal");
	
//<<<<<<< HEAD
	//Put an angle limit on character
	climbPercent = myTransform.localEulerAngles.x;
	if(forwardMovement > 0)
	{
		if((climbPercent <= 360) && (climbPercent >= (360-maxClimbAngle)))
		{
			climbPercent = 1 - ((360-climbPercent)/maxClimbAngle);
		}
		else if ((climbPercent > maxClimbAngle) && (climbPercent < (360-maxClimbAngle)))
			{
				climbPercent = 0;
			}
			else
			{
				climbPercent = 1;
			}
	}
	else if (forwardMovement < 0 )
		{
			if((climbPercent >= 0) && (climbPercent <= maxClimbAngle))
			{
				climbPercent = 1- (climbPercent/maxClimbAngle);
			}
			else if ((climbPercent > maxClimbAngle) && (climbPercent < (360-maxClimbAngle)))
				{
					climbPercent = 0;
				}
				else
				{
					climbPercent = 1;
				}
		}
	//Move GameObject
//=======
    //Implement Angle restriction
	climbPercent = myTransform.localEulerAngles.x;
	if(forwardMovement > 0)
	{
	    if((climbPercent <= 360) && (climbPercent >= (360-maxClimbAngle)))
	    {
	        climbPercent = 1 - ((360-climbPercent)/maxClimbAngle);
	    }
	    else if ((climbPercent < (360-maxClimbAngle)) && (climbPercent > maxClimbAngle))
	    {
	        climbPercent = 0;
	    }
	    else
	    {
	        climbPercent = 1;
	    }
	}
	else if (forwardMovement < 0)
	{
	    if((climbPercent <= maxClimbAngle) && (climbPercent >= 0))
	    {
	        climbPercent = 1 - (climbPercent/maxClimbAngle);
	    }
	    else if ((climbPercent > (maxClimbAngle)) && (climbPercent < (360-maxClimbAngle)))
	    {
	        climbPercent = 0;
	    }
	    else
	    {
	        climbPercent = 1;
	    }
	}
//	Debug.Log("Rotation is: "+myTransform.localEulerAngles.x +" Climb percent is: "+climbPercent+" Forward Movement is: "+forwardMovement);

    //Move GameObject
//>>>>>>> refs/remotes/origin/master
	moveForward = forwardMovement * Vector3.forward * speed * Time.deltaTime * climbPercent /* speedMultiplier*/;
	//currentRotation = ClampAngle(currentRotation + (rotateObject * rotationSpeed * Time.deltaTime));
	//rotationAngle = Quaternion.Euler(0.0, currentRotation, 0.0);
	rb.AddRelativeForce(moveForward, ForceMode.VelocityChange);
	//myTransform.rotation = rotationAngle;
	rb.AddTorque(myTransform.up * rotationSpeed /* speedMultiplier*/ * rotateObject* Time.deltaTime, ForceMode.VelocityChange);
}

function Update () {

	/*//Check distance to ground
	distToGround = GetComponent.<Collider>().bounds.extents.y;

	//Receive inputs from player
	forwardMovement = Input.GetAxis("Vertical");
	//rotateObject = Mathf.Clamp(Input.GetAxis("Horizontal"),-1.0,1.0);
	rotateObject = Input.GetAxis("Horizontal");
	
	//Move GameObject
	moveForward = forwardMovement * Vector3.forward * speed * Time.deltaTime /* speedMultiplier;*/
	//currentRotation = ClampAngle(currentRotation + (rotateObject * rotationSpeed * Time.deltaTime));
	//rotationAngle = Quaternion.Euler(0.0, currentRotation, 0.0);
	/*rb.AddRelativeForce(moveForward, ForceMode.VelocityChange);
	//myTransform.rotation = rotationAngle;
	rb.AddRelativeTorque(myTransform.up * rotationSpeed /* speedMultiplier*/ /** rotateObject* Time.deltaTime, ForceMode.VelocityChange); */
	
	//Grab Object
	
	if(IsGrounded() && !lastGroundState)
	{
		myTransform.transform.rotation.Set(myTransform.transform.localRotation.x, myTransform.transform.localRotation.y, 0f, myTransform.transform.localRotation.w);
	}
	lastGroundState = IsGrounded();
	
	//Jump
	if(Input.GetKeyDown(jumpKey) && IsGrounded()) 
	{ 
		jumpUp(); 
	}
	
}

function IsGrounded(): boolean {
   return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1);
 }

function ClampAngle( theAngle : float ) : float 
 {
     if ( theAngle < -360.0 )
     {
         theAngle += 360.0;
     }
     else if ( theAngle > 360.0 )
     {
         theAngle -= 360.0;
     }
     
     return theAngle;
 }
 
 //causes object this is attached to move up with a certain force
 function jumpUp(){
 	
 	//applies and upward force to character
 	rb.AddForce(0.0, thrust, 0.0); 
 	 
 }