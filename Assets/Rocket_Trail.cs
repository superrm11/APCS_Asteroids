using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Trail : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.SetParent (GameObject.FindGameObjectWithTag ("Player").transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow))
			GetComponent<SpriteRenderer> ().enabled = true;
		else
			GetComponent<SpriteRenderer> ().enabled = false;
	}
}
