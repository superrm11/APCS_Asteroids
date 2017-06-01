using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCheat : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		if (checkForCheatCode ())
			print ("Cheat Successful!");

		runCheatSequence (checkForCheatCode ());
	}



	private const float cheatCodeTime = 5f;
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

	private bool isRunningCheat = false;

	private float ellapsedTime1 = 0f, ellapsedTime2 = 0f, ellapsedTime3 = 0f,ellapsedTime4 = 0f;
	private bool finishedStage1 = false;

	private void runCheatSequence(bool enable)
	{
		if (enable) {
			isRunningCheat = true;
		}

		if (!isRunningCheat)
			return;

		if(!finishedStage1)
			GetComponents<AudioSource> () [3].Play ();

		finishedStage1 = true;

		if (ellapsedTime1 < 13.5) {
			ellapsedTime1 += Time.deltaTime;
			return;
		}

		print ("got this far");
		GameObject.Find ("white_screen").GetComponent<SpriteRenderer> ().enabled = true;


		if (ellapsedTime4 < 10) {
			ellapsedTime4 += Time.deltaTime;
			return;
		}
		GameObject.Find ("Background").GetComponents<AudioSource> () [3].Stop ();
		GameObject.Find ("white_screen").GetComponent<SpriteRenderer> ().enabled = false;
		GameObject.Find ("Bluescreen").GetComponent<SpriteRenderer> ().enabled = true;

		GameObject.Find ("Background").GetComponents<AudioSource> () [4].Play ();
		isRunningCheat = false;
	}
}
