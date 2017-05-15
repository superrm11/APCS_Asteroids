using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public const float speed = .75f;

	public float bounderyOffset = 1.5f;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 360)));

	}

	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2 (0, 1) * Time.deltaTime * speed);

		World.checkBounderies (gameObject, bounderyOffset);


	}
}
