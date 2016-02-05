/********************************************************
* Author: Nick Gravelyn									*
* Script: SnapToSurface.cs								*
* Purpose: Small Unity editor window that helps with 	*
* moving objects until they're touching a surface.		*
* Edits: Unity 5.21 updated script to get rid of		*
* obsolete APIs.										*
* Location Retrieved from:								*
* https://gist.github.com/nickgravelyn/4539788			*
********************************************************/

using UnityEngine;
using UnityEditor;

public class SnapToSurface : EditorWindow
{
	static SnapToSurface instance;
	
	void OnGUI()
	{
		if (GUILayout.Button("X"))
		{
			Drop(new Vector3(1, 0, 0));
		}
		if (GUILayout.Button("-X"))
		{
			Drop(new Vector3(-1, 0, 0));
		}
		if (GUILayout.Button("Y"))
		{
			Drop(new Vector3(0, 1, 0));
		}
		if (GUILayout.Button("-Y"))
		{
			Drop(new Vector3(0, -1, 0));
		}
		if (GUILayout.Button("Z"))
		{
			Drop(new Vector3(0, 0, 1));
		}
		if (GUILayout.Button("-Z"))
		{
			Drop(new Vector3(0, 0, -1));
		}
	}
	
	[MenuItem("Window/Snap to Surface")]
	static void ShowWindow()
	{
		if (instance == null)
		{
			instance = ScriptableObject.CreateInstance<SnapToSurface>();
		}
		instance.Show();
	}
	
	static void Drop(Vector3 dir)
	{
		foreach (GameObject go in Selection.gameObjects)
		{
			// if the object has a collider we can do a nice sweep test for accurate placement
			if (go.GetComponent<Collider>() != null && !(go.GetComponent<Collider>() is CharacterController))
			{
				// figure out if we need a temp rigid body and add it if needed
				bool addedRigidBody = false;
				if (go.GetComponent<Rigidbody>() == null)
				{
					go.AddComponent<Rigidbody>();
					addedRigidBody = true;
				}
				
				// sweep the rigid body downwards and, if we hit something, move the object the distance  		
				RaycastHit hit;
				if (go.GetComponent<Rigidbody>().SweepTest(dir, out hit))
				{
					go.transform.position += dir * hit.distance;
				}
				
				// if we added a rigid body for this test, remove it now
				if (addedRigidBody)
				{
					DestroyImmediate(go.GetComponent<Rigidbody>());
				}
			}
			// without a collider, we do a simple raycast from the transform
			else
			{
				// change the object to the "ignore raycast" layer so it doesn't get hit
				int savedLayer = go.layer;
				go.layer = 2;
				
				// do the raycast and move the object down if it hit something			
				RaycastHit hit;
				if (Physics.Raycast(go.transform.position, dir, out hit))
				{
					go.transform.position = hit.point;
				}
				
				// restore layer for the object
				go.layer = savedLayer;
			}
		}
	}
}