using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
	public int level = 3;

	public int health = 4;
	public float speed = .75f;

	public float bounderyOffset = 0f;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 360)));
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2 (0, 1) * Time.deltaTime * speed);

		World.checkBounderies (gameObject, bounderyOffset);

		if (health <= 0)
			selfDestruct ();

	}

	private void selfDestruct()
	{
		if(level > 1)
		for (int i = 0; i < 2; i++) {
			string levelName = (level == 3) ? "med_Asteroid" : "sm_Asteroid";
			Asteroid a = ((GameObject) Instantiate (Resources.Load (levelName), transform.position, new Quaternion ())).GetComponent<Asteroid>();
		}
		
		GameObject.Find("Background").gameObject.GetComponents<AudioSource> () [level - 1].Play ();
		Destroy (gameObject);
	}


	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Equals ("Missile"))
			health -= 1;
	}

}
