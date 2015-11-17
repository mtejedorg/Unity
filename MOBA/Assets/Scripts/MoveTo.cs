// MoveToClickPoint.cs
using UnityEngine;

public class MoveTo : MonoBehaviour {
	NavMeshAgent agent;
	
	void Start() {
		agent = GetComponent<NavMeshAgent>();
	}
	
	void Update() {
		if (Input.GetMouseButtonDown(1)) {
			RaycastHit hit;
			GetComponent<Animator>().SetBool ("Moving", true);
			
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
				agent.destination = hit.point;
			}
		}
	}
}