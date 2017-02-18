using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody theBody = null;
	private Transform theTransform = null;

	public bool mouseLook = true;
	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";
	public string fireAxis = "Fire1";

	public float maxSpeed = 4.0f;
	public float reloadDelay = 0.2f;
	public bool canFire = true;

	public Transform[] weaponTransforms;

	void Awake () {
		theBody = GetComponent<Rigidbody> ();
		theTransform = GetComponent<Transform> ();
	}

	void FixedUpdate () {
		float horizontal = Input.GetAxis (horizontalAxis);
		float vertical = Input.GetAxis (verticalAxis);

		Vector3 direction = new Vector3(horizontal, 0, vertical);
		theBody.AddForce (direction.normalized * maxSpeed);

		theBody.velocity = new Vector3 (
			Mathf.Clamp(theBody.velocity.x, -maxSpeed, maxSpeed),
			Mathf.Clamp(theBody.velocity.y, -maxSpeed, maxSpeed),
			Mathf.Clamp(theBody.velocity.z, -maxSpeed, maxSpeed));

		if (mouseLook) {
			Vector3 mousePositionInScreen = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f);
			Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionInScreen);
			mousePositionInWorld = new Vector3 (mousePositionInWorld.x, 0.0f, mousePositionInWorld.z);
			Vector3 positionToLook = mousePositionInWorld - theTransform.position;

			theTransform.localRotation = Quaternion.LookRotation (positionToLook.normalized, Vector3.up);
		}

		if (Input.GetButtonDown (fireAxis) && canFire) {
			foreach (Transform transform in weaponTransforms) {
				AmmoManager.SpawnAmmo (transform.position, transform.rotation);
			}
			canFire = false;
			Invoke ("EnableFire", reloadDelay);
		}
	}

	void EnableFire() {
		canFire = true;
	}

	public void Die() {
		Destroy (gameObject);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
