using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Collider))]

public class CharacterControl : MonoBehaviour {
	RaycastHit hitInfo = new RaycastHit();
	NavMeshAgent agent;
	Collider target;
	Animator anim;
	Character self;
	Character targetChar;
	public GUIText dmgText;

	bool attacking = false;
	float lastBasicAttack;
	int basicAttackNumber;
	public bool inAttackRange = false;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		self = GetComponent<Character> ();
		lastBasicAttack = 0.0f;
		basicAttackNumber = 0;
	}

	void Update () {
		
		if (Input.GetMouseButtonDown (1)) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray.origin, ray.direction, out hitInfo)) {

				if (hitInfo.collider != null & hitInfo.collider.tag == "Enemy") {
					// The agent hit a target.
					// Falta completar
					if (!inAttackRange)
						agent.destination = hitInfo.point;

					target = hitInfo.collider;
					attacking = true;
					transform.rotation = Quaternion.LookRotation (target.transform.position - transform.position);

				} else {
					agent.destination = hitInfo.point;
					target = null;
					transform.rotation = Quaternion.LookRotation (hitInfo.point - transform.position);
					agent.Resume();
					inAttackRange = false;
					attacking = false;
					basicAttackNumber = 0;
					anim.SetBool ("Attack", false);
					anim.SetBool ("Moving", true);
				}
			} else if (attacking) {
				agent.destination = target.transform.position;
				transform.rotation = Quaternion.LookRotation (target.transform.position - transform.position);
			}
					
		}
	}

	void DealDmg(){
		targetChar.TakeDmg (self.outDmg ());
		dmgText.text = "" + self.outDmg ();
	}

	void AttackTarget(){
		targetChar = target.GetComponent<Character> ();

		if (basicAttackNumber == 0) {
			anim.SetBool ("Attack", true);
			lastBasicAttack = Time.time;
			basicAttackNumber ++;

		} else if (Time.time - lastBasicAttack > self.TimeBetweenBasicAttacks()){
			anim.SetTrigger ("ChangeAttackAnim");
			lastBasicAttack = Time.time;
			basicAttackNumber ++;
			//DealDmg ();
		}

		if (targetChar.dead) {
			target = null;
			attacking = false;
			anim.SetBool ("Attack", false);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other == target){
			agent.Stop();
			anim.SetBool ("Moving", false);
			AttackTarget();
		}
	}

	void OnTriggerStay(Collider other){
		if(other == target){
			inAttackRange = true;
			AttackTarget ();
		}
	}

	void OnTriggerExit(Collider other){
		if(other == target){
			inAttackRange = false;
		}
	}
}