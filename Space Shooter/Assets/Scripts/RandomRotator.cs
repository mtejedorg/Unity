using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour 
{
	public float temble;

	void Start()
	{
		Rigidbody asteroid = GetComponent<Rigidbody>();
		asteroid.angularVelocity = Random.insideUnitSphere * temble;
	}
}
