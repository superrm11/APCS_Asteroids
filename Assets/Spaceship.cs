using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {


	public BoxCollider2D hitbox;

	void Start()
	{
		print (name + "_hb");
		hitbox = gameObject.GetComponent<BoxCollider2D> ();

	}

	/**
	 * The main thread where everything is run
	 */
	void Update ()
	{

		move ();

		World.checkBounderies (this.gameObject, 0f);

		fire (Input.GetKey (KeyCode.Space));

	}

	public const float missileFireRate = .7f;

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
			Instantiate (GameObject.Find ("missile"));
			currentTime = missileFireRate;
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
		if (Input.GetKey (KeyCode.UpArrow) && moveSpeed < maxMoveSpeed) {
			moveSpeed += accelSpeed;
		} else if (moveSpeed > 0.0)
			moveSpeed -= decelSpeed;

		if (moveSpeed < 0.0)
			moveSpeed = 0;

		if(Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.forward, -turnSpeed * Time.deltaTime);

		transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
	}


	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Equals ("Asteroid"))
			Destroy (gameObject);
	}


}
