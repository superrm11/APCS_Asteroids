using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	public const float speed = 15.0f;

	private float timeAlive = 0f;

	private bool isOriginal = false;
	private static bool hasCreatedOriginal = false;

	void Start(){

		transform.SetParent(GameObject.Find("Spaceship").transform);
		transform.localPosition = new Vector2 (0, 1);

		if (hasCreatedOriginal) {
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			transform.SetParent (null);
			gameObject.GetComponent<Collider2D> ().enabled = true;

			transform.rotation = GameObject.Find ("Spaceship").transform.rotation;
		} else {
			hasCreatedOriginal = true;
			isOriginal = true;
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			gameObject.GetComponent<Collider2D> ().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (isOriginal)
			return;
		
		transform.Translate (Vector2.up * Time.deltaTime * speed);

		if (timeAlive > 2) {
			Destroy (gameObject);
		}
		else
			timeAlive += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Equals ("Asteroid")) {
			Destroy (gameObject);
		}
	}
}
