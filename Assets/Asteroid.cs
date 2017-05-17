using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
	private int level = 3;

	private int health;
	private float speed;

	const float bounderyOffset = 1.5f;

	public Asteroid()
	{
		if (level == 3) {
			health = 4;
			speed = .75f;
		} else if (level == 2) {
			health = 2;
			speed = 1f;
		} else if (level == 1) {
			health = 1;
			speed = 1.25f;
		}
	}

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 360)));
	}

	// Update is called once per frame
	void Update () {
		print (health);
		transform.Translate (new Vector2 (0, 1) * Time.deltaTime * speed);

		World.checkBounderies (gameObject, bounderyOffset);

		if (health <= 0)
			selfDestruct ();

	}

	private void selfDestruct()
	{
		for (int i = 0; i < 2; i++) {
			Asteroid a = ((GameObject) Instantiate (Resources.Load ("med_Asteroid"), transform.position, new Quaternion ())).GetComponent<Asteroid>();

		}
		Destroy (gameObject);
	}


	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Equals ("Missile"))
			health -= 1;
	}

}
