using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/********************************************************************************
*   Code creates a simple hard conical mesh (two triangles) using criteria      *
*   input during design time. Adds a mesh collider and sets it to trigger.      *
********************************************************************************/

public class EnemyVision : MonoBehaviour {

    //public variables
    public float fieldOfVision = 90f;
    public float visionRadius = 10f;
    public float meshHeight = .4f;

    //private variables
    private bool playerPresent = false;
    private GameObject playerRef;
    
    
    // Use this for initialization
    void Start() {

        //variables needed for mesh
        Vector3[] vertices = new Vector3[8];    //vertices for cone
        int[] tri = new int[] { 0,1,2,2,1,3,3,7,5,5,1,3,3,7,6,6,2,3,2,6,4,4,0,2,0,1,5,5,4,0,4,5,7,7,6,4};                 //12 triangles = 3 points each
        float halfAngle = (fieldOfVision * Mathf.Deg2Rad / 2); //convert angle to degrees and 90 degree offset
        float offset = Mathf.PI / 2;

        //create vertices
        vertices[0] = new Vector3(0, meshHeight / 2, 0);
        vertices[1] = new Vector3(Mathf.Cos(transform.localEulerAngles.y + offset - halfAngle) * visionRadius, meshHeight/2, Mathf.Sin(transform.localEulerAngles.y + offset - halfAngle) * visionRadius);
        vertices[2] = new Vector3(Mathf.Cos(transform.localEulerAngles.y+ offset + halfAngle) * visionRadius, meshHeight/2, Mathf.Sin(transform.localEulerAngles.y + offset + halfAngle) * visionRadius);
        vertices[3] = new Vector3(0, meshHeight / 2, visionRadius);
        vertices[4] = new Vector3(0, -1* meshHeight / 2, 0);
        vertices[5] = new Vector3(Mathf.Cos(transform.localEulerAngles.y + offset - halfAngle) * visionRadius, -1 * meshHeight / 2, Mathf.Sin(transform.localEulerAngles.y + offset - halfAngle) * visionRadius);
        vertices[6] = new Vector3(Mathf.Cos(transform.localEulerAngles.y + offset + halfAngle) * visionRadius, -1 * meshHeight / 2, Mathf.Sin(transform.localEulerAngles.y + offset + halfAngle) * visionRadius);
        vertices[7] = new Vector3(0, -1 * meshHeight / 2, visionRadius);

        //Debug.Log("Vertices:  1: " + vertices[0] + "  2: " + vertices[1] + "  3: " + vertices[2] + "  4: " + vertices[3]);

        //create mesh and add it to filter and collider
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = tri;
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;

    }
	
	
    void OnTriggerEnter(Collider other)
    {
        //add entered object to array
        //Debug.Log("Object entered = " + other.name);


        if (!playerPresent)
        {
            playerPresent = other.gameObject.CompareTag("Player"); //Only one player, if they are present than change the it to true
            playerRef = other.gameObject;
        } 

        //If object is player check if they are blocked by anything
        if(other.gameObject.CompareTag("Player"))
        {
            CanBeSeen(other.gameObject);
            Debug.Log("Player Entered");
        }
    }

    void OnTriggerExit(Collider other)
    {
        //remove exiting object from tracking list
        //Debug.Log("Object leaving: " + other.name);

        if(!other.gameObject.CompareTag("Player"))
        {
            if (playerPresent)
            {
                CanBeSeen(playerRef);
            }
        }
        else
        {
            Debug.Log("Player has left");
            playerPresent = false;
            playerRef = null;
            gameObject.GetComponentInParent<EnemyAI>().ContinuePatrol();  //Resume patrolling if player has left detection area
        }
    }

    void CanBeSeen(GameObject target)
    {
        //Variables
        Ray ray;
        RaycastHit hit;

        ray = new Ray(transform.position, target.transform.position - transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                gameObject.GetComponentInParent<EnemyAI>().ChaseTarget(hit.collider.gameObject);    //set the chase target and begin chasing
            }
        }
    }
}
