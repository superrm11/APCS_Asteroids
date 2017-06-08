﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButton : MonoBehaviour {

	public float width = 1f;
	public float height = 1f;

	Color grayed = new Color(50,50,50,128);
	Color white = new Color(0,0,0,128);

	public void Update()
	{
		if (isPressed ()) {
			GetComponent<SpriteRenderer> ().color = grayed;
		} else {
			GetComponent<SpriteRenderer> ().color = white;
		}
	}

	public bool isPressed()
	{
		for (int i = 0; i < Input.touchCount; i++) {
			if (Input.GetTouch (i).position.x > transform.position.x - (width / 2)
				&& Input.GetTouch(i).position.x < transform.position.x + (width / 2)
				&& Input.GetTouch(i).position.y > transform.position.y - (height / 2)
				&& Input.GetTouch(i).position.y < transform.position.y - (height / 2)){
				return true;
			}
		}
		return false;
	}
}
