using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

	public float destroyTime = 2.00f;

	// Use this for initialization
	void Start () {
		Invoke ("Die", destroyTime);
	}

	void Die() {
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
