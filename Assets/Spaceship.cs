using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {


	public BoxCollider2D hitbox;

	void Start()
	{
		hitbox = gameObject.GetComponent<BoxCollider2D> ();

	}

	/**
	 * The main thread where everything is run
	 */
	void Update ()
	{
		if (World.menuStatus == World.menuType.GAME) {
			if (GetComponent<SpriteRenderer> ().enabled == false)
				GetComponent<SpriteRenderer> ().enabled = true;

			move ();

			World.checkBounderies (this.gameObject, 0f);

			fire (Input.GetKey (KeyCode.Space));
		} else
			GetComponent<SpriteRenderer> ().enabled = false;
	}

	public const float missileFireRate = .3f;

	public float currentTime = 0.0f;

	/**
	 * Controls the firing of missiles with the given fire rate.
	 * The destruction of missiles is handled within the class.
	 */
	private void fire(bool keyPress)
	{
		if (currentTime > 0) {
			currentTime -= Time.deltaTime;
		}else if (Input.GetKey (KeyCode.Space)) {
			Instantiate (Resources.Load("Missile"));
			currentTime = missileFireRate;
			GetComponent<AudioSource> ().Play ();
		}
	}


	public float moveSpeed = 0f;
	public float maxMoveSpeed = 15f;
	public float turnSpeed = 150f;

	public float accelSpeed = .25f;
	public float decelSpeed = .15f;

	/**
	 * Controls movement of the spaceship: up arrow 
	 * controls the forward thrusters, left turns port,
	 * right turns starboard.
	 */
	private void move()
	{



		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			if (Input.GetKey (KeyCode.UpArrow) && moveSpeed < maxMoveSpeed) {
				moveSpeed += accelSpeed;
			} else if (moveSpeed > 0.0)
				moveSpeed -= decelSpeed;

			if (moveSpeed < 0.0)
				moveSpeed = 0;

			if (Input.GetKey (KeyCode.LeftArrow))
				transform.Rotate (Vector3.forward, turnSpeed * Time.deltaTime);

			if (Input.GetKey (KeyCode.RightArrow))
				transform.Rotate (Vector3.forward, -turnSpeed * Time.deltaTime);
		} else if (Application.platform == RuntimePlatform.Android) {
			for (int i = 0; i < Input.touchCount; i++) {
				//TODO working on Android support
			}
		}

		transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
	}


	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Equals ("Asteroid")) {
			
			Destroy (gameObject);
			for (int i = 0; i < 3; i++)
				GameObject.Find ("Background").GetComponents<AudioSource> () [i].Play ();
		}
	}


}
