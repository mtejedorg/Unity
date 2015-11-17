using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class CameraController : MonoBehaviour {

	private GameObject player;
	private bool locked;
	private Vector3 offset;
	private Vector3 originalOffset;
	private Camera cameraComp;
	private Vector3 newPosition;
	public float camMovingSpeed = 10;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		offset = transform.position - player.transform.position;
		originalOffset = offset;
		cameraComp = GetComponent<Camera> ();
	}

	void Update(){
		if (Input.GetButtonDown ("LockCamera")) {
			locked = !locked;
			if (locked){
				offset = transform.position - player.transform.position;
			}
		}

		if (Input.GetButtonDown ("ResetCamera")){
			transform.position = player.transform.position + originalOffset;
			offset = originalOffset;
		}

		if (locked) {
			transform.position = player.transform.position + offset;
		} else {
			Vector2 mouseScreenPos = Input.mousePosition;
			if (mouseScreenPos.x >= cameraComp.pixelWidth * 9/10)
				transform.Translate(Vector3.right * camMovingSpeed * Time.deltaTime);

			if (mouseScreenPos.x <= cameraComp.pixelWidth * 1/10)
				transform.Translate(-Vector3.right * camMovingSpeed * Time.deltaTime);
		
			if (mouseScreenPos.y >= cameraComp.pixelHeight * 9/10)
				transform.Translate(Vector3.up * camMovingSpeed * Time.deltaTime);
		
			if (mouseScreenPos.y <= cameraComp.pixelHeight * 1/10)
				transform.Translate(-Vector3.up * camMovingSpeed * Time.deltaTime);
		
		}
	}
}
