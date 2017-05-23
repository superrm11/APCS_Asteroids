using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		levelScreen = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		levelScreen.enabled = false;
		print (levelScreen);
	}

	void Update()
	{
		displayLevelScreen (GameObject.FindGameObjectsWithTag ("Asteroid").Length < 1 || isShowingLevelScreen);
	}

	public static bool isShowingLevelScreen = false;

	private float timePassed1 = 0f,timePassed2 = 0f,timePassed3 = 0f;
	private bool finishedWait1 = false, finishedWait2 = false;
	private bool displayLevelScreenFirstRun = true;

	private void displayLevelScreen(bool enabled)
	{
		if (!enabled)
			return;

		if (displayLevelScreenFirstRun) {
			displayLevelScreenFirstRun = false;
			GameObject.Find ("LevelDisplay").GetComponent<Text> ().text = "Level " + ++level;
		}

		isShowingLevelScreen = true;
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag ("Asteroid");
		GameObject ship = GameObject.FindGameObjectWithTag ("Player");

		ship.GetComponent<SpriteRenderer> ().enabled = false;
		foreach(GameObject g in asteroids)
		{
			g.GetComponent<SpriteRenderer> ().enabled = false;
		}

		timePassed1 += Time.deltaTime;
		if (timePassed1 < 1 && !finishedWait1) {
			return;
		}

		finishedWait1 = true;
		levelScreen.enabled = true;

		timePassed2 += Time.deltaTime;
		if (timePassed2 < 2 && !finishedWait2) {
			return;
		}
		finishedWait2 = true;

		levelScreen.enabled = false;

		timePassed3 += Time.deltaTime;
		if (timePassed3 < 1)
			return;

		for (int i = 0; i < level; i++)
			Instantiate (Resources.Load ("lg_Asteroid"),
				new Vector2 ((Random.Range (0f, 1f) > .5) ? Random.Range (-4, leftBound) : 
					Random.Range (4, rightBound), (Random.Range (0f, 1f) > .5) ?
					Random.Range (3f, upperBound) : Random.Range (lowerBound, -3f)), new Quaternion());

		timePassed1 = 0f;
		timePassed2 = 0f;
		timePassed3 = 0f;
		finishedWait1 = false;
		finishedWait2 = false;

		GameObject.Find("Spaceship").transform.SetPositionAndRotation(new Vector2(), new Quaternion());

		GameObject.Find ("Spaceship").GetComponent<SpriteRenderer> ().enabled = true;

		displayLevelScreenFirstRun = true;
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
