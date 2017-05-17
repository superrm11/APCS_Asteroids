using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public int level = 1;
	public int lives = 3;

	private Canvas levelScreen;

	public const float lowerBound = -5.1f;
	public const float upperBound = 6.1f;
	public const float leftBound = -10.4f;
	public const float rightBound = 10.4f;

	void Start()
	{
		levelScreen = GetComponent<Canvas> ();
	}

	void Update()
	{
		displayLevelScreen (GameObject.FindGameObjectsWithTag ("Asteroid").Length < 1 || isShowingLevelScreen);
	}

	private bool isShowingLevelScreen = false;

	private void displayLevelScreen(bool enabled)
	{
		if (!enabled)
			return;
		isShowingLevelScreen = true;
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag ("Asteroid");
		GameObject ship = GameObject.FindGameObjectWithTag ("Player");

		ship.GetComponent<SpriteRenderer> ().enabled = false;
		foreach(GameObject g in asteroids)
		{
			g.GetComponent<SpriteRenderer> ().enabled = false;
		}

		float start = Time.time;

		if (Time.time - start < 1)
			return;
		
		Component[] c = levelScreen.GetComponents<Component> ();
		foreach (Component co in c)
			print (c.GetType ());
		levelScreen.enabled = true;


		isShowingLevelScreen = false;
	}

	/**
	 * Checks to see if the ship is outside the bounderies of the screen.
	 * If it is, then it teleports to the other side.
	 */
	public static void checkBounderies(GameObject g, float offset)
	{
		if (g.transform.position.y > upperBound + offset)
			g.transform.position = new Vector2 (g.transform.position.x, lowerBound + .1f - offset);
		else if (g.transform.position.y < lowerBound - offset)
			g.transform.position = new Vector2 (g.transform.position.x, upperBound - .1f + offset);


		if (g.transform.position.x > rightBound + offset) 
			g.transform.position = new Vector2 (leftBound + .1f - offset, g.transform.position.y);
		else if (g.transform.position.x < leftBound - offset)
			g.transform.position = new Vector2 (rightBound - .1f + offset, g.transform.position.y);


	}
}
