// ClickToMove.cs
using UnityEngine;

[RequireComponent (typeof (NavMeshAgent))]
public class ClickToMove : MonoBehaviour {
	RaycastHit hitInfo = new RaycastHit();
	NavMeshAgent agent;
	
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	void Update () {

		if(Input.GetMouseButtonDown(1)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))

				transform.rotation = Quaternion.LookRotation(hitInfo.point - transform.position);

				agent.destination = hitInfo.point;
		}
	}
}