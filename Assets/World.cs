﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World : MonoBehaviour {

	public enum menuType
	{
		MENU, LEVELUP_SCREEN,LIFEDOWN_SCREEN, GAME, GAME_OVER
	}

	public static menuType menuStatus = menuType.MENU;

	private static int level = 0;
	private static int lives = 3;

	private static Canvas levelScreen;

	public const float lowerBound = -5.1f;
	public const float upperBound = 5.1f;
	public const float leftBound = -8.75f;
	public const float rightBound = 8.75f;

	void Start()
	{
		levelScreen = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		levelScreen.enabled = true;
		GameObject.Find ("LevelDisplay").GetComponent<Text> ().enabled = false;
		GameObject.Find ("LifeCounter").GetComponent<Text> ().enabled = false;

	}
		
	void Update()
	{
		if (menuStatus == menuType.MENU) {
			GameObject.FindGameObjectWithTag ("RocketTrail").GetComponent<Rocket_Trail> ().enable = false;
			GameObject.Find ("MenuName").GetComponent<Text> ().enabled = true;
			GameObject.Find ("PressSpace").GetComponent<Text> ().enabled = true;
			if (Input.GetKey (KeyCode.Space)) {
				menuStatus = menuType.LEVELUP_SCREEN;
			}

		} else if (menuStatus == menuType.LEVELUP_SCREEN) {
			GameObject.FindGameObjectWithTag ("RocketTrail").GetComponent<Rocket_Trail> ().enable = false;
			if (GameObject.Find ("MenuName").GetComponent<Text> ().enabled == true ||
			    GameObject.Find ("PressSpace").GetComponent<Text> ().enabled == true) {
				GameObject.Find ("PressSpace").GetComponent<Text> ().enabled = false;
				GameObject.Find ("MenuName").GetComponent<Text> ().enabled = false;
			} 
			displayLevelScreen (levelScreenType.LEVELUP);
		} else if (menuStatus == menuType.LIFEDOWN_SCREEN) {
			GameObject.FindGameObjectWithTag ("RocketTrail").GetComponent<Rocket_Trail> ().enable = false;
			displayLevelScreen (levelScreenType.LIFEDOWN);

		} else if (menuStatus == menuType.GAME) {
			GameObject.FindGameObjectWithTag ("RocketTrail").GetComponent<Rocket_Trail> ().enable = true;
			if (lives < 1) {
				menuStatus = menuType.GAME_OVER;
			} else if (GameObject.FindGameObjectsWithTag ("Asteroid").Length == 0) {
				menuStatus = menuType.LEVELUP_SCREEN;
				displayLevelScreen (levelScreenType.LEVELUP);
			} else if (GameObject.FindGameObjectWithTag ("Player") == null) {
				menuStatus = menuType.LIFEDOWN_SCREEN;
			}
			
		} else if (menuStatus == menuType.GAME_OVER) {
			GameObject.FindGameObjectWithTag ("RocketTrail").GetComponent<Rocket_Trail> ().enable = false;
			enableGameOverScreen (true);
			if (Input.GetKey (KeyCode.R)) {
				enableGameOverScreen (false);
				lives = 3;
				level = 0;
				menuStatus = menuType.LEVELUP_SCREEN;
			}
		}


		if (checkForCheatCode ())
			print ("Cheat Successful!");

	}

	private static float timePassed1 = 0f,timePassed2 = 0f,timePassed3 = 0f;
	private static bool finishedWait1 = false, finishedWait2 = false;
	private static bool displayLevelScreenFirstRun = true;

	public enum levelScreenType
	{
		LEVELUP, LIFEDOWN
	}

	private static void displayLevelScreen(levelScreenType type)
	{

		if (displayLevelScreenFirstRun) {
			displayLevelScreenFirstRun = false;
			if (type == levelScreenType.LEVELUP) {
				GameObject.Find ("LevelDisplay").GetComponent<Text> ().text = "Level " + ++level;
				GameObject.Find ("LifeCounter").GetComponent<Text> ().text = "Lives   x" + lives;
			} else if (type == levelScreenType.LIFEDOWN) {
				GameObject.Find ("LifeCounter").GetComponent<Text> ().text = "Lives   x" + --lives;
				GameObject.Find ("LevelDisplay").GetComponent<Text> ().text = "Level " + level;
			}
		}
		//Find and destroy any remaining asteroids that may be on the screen for some reason
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag ("Asteroid");
	
		foreach(GameObject g in asteroids)
		{
			Destroy (g);
		}

		//Wait for 1 second without blocking code
		timePassed1 += Time.deltaTime;
		if (timePassed1 < 1 && !finishedWait1) {
			return;
		}
		finishedWait1 = true;
		//Finished waiting

		enableLevelScreen (true);

		//Wait for 2 seconds without blocking code
		timePassed2 += Time.deltaTime;
		if (timePassed2 < 2 && !finishedWait2) {
			return;
		}
		finishedWait2 = true;
		//Finished Waiting

		enableLevelScreen (false);

		//Wait for 1 second without blocking
		timePassed3 += Time.deltaTime;
		if (timePassed3 < 1)
			return;
		//Finished waiting

		//Create a number of asteroids based on the player's level
		for (int i = 0; i < level; i++)
			createNewAsteroid();

		//Restart the time passed back to zero
		timePassed1 = 0f;
		timePassed2 = 0f;
		timePassed3 = 0f;
		finishedWait1 = false;
		finishedWait2 = false;

		//If the 
		if (GameObject.FindGameObjectWithTag ("Player") == null)
			Instantiate (Resources.Load ("Spaceship"), new Vector2 (), new Quaternion ());
		else
		{

			GameObject.FindGameObjectWithTag ("Player").transform.SetPositionAndRotation (new Vector2 (), new Quaternion ());

			GameObject.FindGameObjectWithTag ("Player").GetComponent<SpriteRenderer> ().enabled = true;
		}

		menuStatus = menuType.GAME;

		displayLevelScreenFirstRun = true;
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

	private static void createNewAsteroid()
	{
		Instantiate (Resources.Load ("lg_Asteroid"),
			new Vector2 ((Random.Range (0f, 1f) > .5) ? Random.Range (-2.5f, leftBound) : 
				Random.Range (2.5f, rightBound), (Random.Range (0f, 1f) > .5) ?
				Random.Range (2f, upperBound) : Random.Range (lowerBound, -2f)), new Quaternion ());
	}

	private static void enableLevelScreen(bool enabled)
	{
		GameObject.Find ("LevelDisplay").GetComponent<Text> ().enabled = enabled;
		GameObject.Find ("LifeCounter").GetComponent<Text> ().enabled = enabled;
	}

	private static void enableGameOverScreen(bool enabled)
	{
		GameObject.Find ("GameOver").GetComponent<Text> ().enabled = enabled;
		GameObject.Find ("Restart").GetComponent<Text> ().enabled = enabled;
	}


	private const float cheatCodeTime = 1f;
	private static float currentCheatTime = 0;
	private enum konamiCode
	{
		UP1, UP2, DOWN1, DOWN2, LEFT1, RIGHT1, LEFT2, RIGHT2, B, A, ENTER, DONE
	}

	private static konamiCode cheatStatus = konamiCode.UP1;
	private static bool checkForCheatCode()
	{

		if (currentCheatTime > cheatCodeTime) {
			cheatStatus = konamiCode.UP1;
			currentCheatTime = 0f;
		}

		if (cheatStatus == konamiCode.UP1 && Input.GetKey (KeyCode.UpArrow)) {
			cheatStatus = konamiCode.UP2;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.UP2 && Input.GetKey (KeyCode.UpArrow)) {
			cheatStatus = konamiCode.DOWN1;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.DOWN1 && Input.GetKey (KeyCode.DownArrow)) {
			cheatStatus = konamiCode.DOWN2;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.DOWN2 && Input.GetKey (KeyCode.DownArrow)) {
			cheatStatus = konamiCode.LEFT1;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.LEFT1 && Input.GetKey (KeyCode.LeftArrow)) {
			cheatStatus = konamiCode.RIGHT1;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.RIGHT1 && Input.GetKey (KeyCode.RightArrow)) {
			cheatStatus = konamiCode.LEFT2;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.LEFT2 && Input.GetKey (KeyCode.LeftArrow)) {
			cheatStatus = konamiCode.RIGHT2;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.RIGHT2 && Input.GetKey (KeyCode.RightArrow)) {
			cheatStatus = konamiCode.B;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.B && Input.GetKey (KeyCode.B)) {
			cheatStatus = konamiCode.A;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.A && Input.GetKey (KeyCode.A)) {
			cheatStatus = konamiCode.ENTER;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.ENTER && Input.GetKey (KeyCode.Return)) {
			cheatStatus = konamiCode.DONE;
			currentCheatTime = 0f;
		} else if (cheatStatus == konamiCode.DONE) {
			cheatStatus = konamiCode.UP1;
			return true;
		}

		currentCheatTime += Time.deltaTime;

		return false;
	}

	private static bool isRunningCheat = false;

	private static float ellapsedTime1 = 0f;
	private static bool finishedStage1 = false;

	private static void runCheatSequence(bool enable)
	{
		if (enable) {
			isRunningCheat = true;
		}

		if (!isRunningCheat)
			return;



	}


}
