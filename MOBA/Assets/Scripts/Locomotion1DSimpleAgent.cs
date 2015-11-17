using UnityEngine;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class Locomotion1DSimpleAgent : MonoBehaviour {
	Animator anim;
	NavMeshAgent agent;
	float smoothDeltaPosition = 0;
	float velocity = 0;

	void Start ()
	{
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		agent.updatePosition = false;
	}
	
	void Update ()
	{
		Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
		float deltaPosition = worldDeltaPosition.magnitude;

		// Low-pass filter the deltaMove
		float smooth = Mathf.Min(1.0f, Time.deltaTime/0.15f);
		smoothDeltaPosition = Mathf.Lerp (smoothDeltaPosition, deltaPosition, smooth);

		// Update velocity if delta time is safe
		if (Time.deltaTime > 1e-5f)
			velocity = smoothDeltaPosition / Time.deltaTime;

		bool shouldMove = velocity > 0.5f && agent.remainingDistance > agent.radius;

		// Update animation parameters
		anim.SetBool("Moving", shouldMove);
		//anim.SetFloat ("Speed", velocity);

		//LookAt lookAt = GetComponent<LookAt> ();
		//if (lookAt)
		//	lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

//		// Pull character towards agent
		if (worldDeltaPosition.magnitude > agent.radius)
			transform.position = agent.nextPosition - 0.9f*worldDeltaPosition;

//		// Pull agent towards character
//		if (worldDeltaPosition.magnitude > agent.radius)
//			agent.nextPosition = transform.position + 0.9f*worldDeltaPosition;
	}

	void OnAnimatorMove ()
	{
		// Update postion to agent position
		transform.position = agent.nextPosition;

//		// Update position based on animation movement using navigation surface height
		Vector3 position = anim.rootPosition;
		position.y = agent.nextPosition.y;
		transform.position = position;
	}
}
