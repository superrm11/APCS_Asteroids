using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Trail : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.SetParent (GameObject.FindGameObjectWithTag ("Player").transform);
		GetComponent<AudioSource> ().Play ();
	}

	public bool enable = true;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow) && enable) {
			GetComponent<SpriteRenderer> ().enabled = true;
			GetComponent<AudioSource> ().mute = false;

		} else {
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<AudioSource> ().mute = true;
		}
		}
}
