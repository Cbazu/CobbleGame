using UnityEngine;
using System.Collections;

public class ObstructionFader : MonoBehaviour {

	public Transform playerTrans;
	private Obstructor m_LastObstructor;
	
	void Start () 
	{
		StartCoroutine(DetectPlayerObstructions());
	}
	
	IEnumerator DetectPlayerObstructions()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			
			Vector3 direction = (playerTrans.position - Camera.main.transform.position).normalized;
			RaycastHit rayCastHit;
			
			if(Physics.Raycast(Camera.main.transform.position, direction, out rayCastHit, Mathf.Infinity))
			{
				Obstructor obs = rayCastHit.collider.gameObject.GetComponent<Obstructor>();
				if(obs)
				{
					obs.SetTransparent();
					m_LastObstructor = obs;
				}
				else 
				{
					if(m_LastObstructor)
					{
						m_LastObstructor.SetToNormal();
						m_LastObstructor = null;
					}
				}
			}
			
		}
	}
}
