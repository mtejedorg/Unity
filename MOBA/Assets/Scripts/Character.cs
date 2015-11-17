using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class Character : MonoBehaviour {

	public int maxHealth;
	public int health;
	public int physDmg;
	public float attackSpeed;
	public bool dead = false;
	private Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
		anim.SetFloat ("AttackSpeed", attackSpeed);
	}
	
	public void TakeDmg(int dmg){
		health = health - dmg;
		if (health <= 0) {
			anim.SetBool ("Dead", true);
			dead = true;
		}
	}

	public float TimeBetweenBasicAttacks(){
		return 1 / attackSpeed;
	}

	public int outDmg(){
		return physDmg;
	}
}
