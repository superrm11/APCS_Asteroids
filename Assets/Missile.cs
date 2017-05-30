using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	public const float speed = 15.0f;

	private float timeAlive = 0f;

	void Start(){

		transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
		transform.localPosition = new Vector2 (0, 1);

		transform.SetParent (null);
		gameObject.GetComponent<Collider2D> ().enabled = true;

		transform.rotation = GameObject.FindGameObjectWithTag ("Player").transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {

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
