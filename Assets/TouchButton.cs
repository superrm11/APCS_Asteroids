using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButton : MonoBehaviour {

	public float width = 1f;
	public float height = 1f;

	Color grayed = new Color(200f,200f,200f,200f);
	Color white = new Color(255f,255f,255f,255f);

	public void Update()
	{
		if (isPressed ()) {
			GetComponent<SpriteRenderer> ().color = grayed;
		} else {
			GetComponent<SpriteRenderer> ().color = white;
		}

		if (Input.touchCount > 0)
			print (absolutePosition(Input.GetTouch (0).position).x);
	}

	public bool isPressed()
	{
		for (int i = 0; i < Input.touchCount; i++) {
			if (absolutePosition(Input.GetTouch (i).position).x > transform.position.x - (width / 2)
				&& absolutePosition(Input.GetTouch(i).position).x < transform.position.x + (width / 2)
				&& absolutePosition(Input.GetTouch(i).position).y > transform.position.y - (height / 2)
				&& absolutePosition(Input.GetTouch(i).position).y < transform.position.y - (height / 2)){
				return true;
			}
		}
		return false;
	}

	public static Vector2 absolutePosition(Vector2 original)
	{
		Vector2 newPos = new Vector2 ();
		newPos.x = (original.x * (15.978f / Screen.currentResolution.width)) - 7.989f;
		newPos.y = (original.y * (10.01f / Screen.currentResolution.height)) - 5.005f;
		return newPos;
	}
}
