using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public const float lowerBound = -5.1f;
	public const float upperBound = 6.1f;
	public const float leftBound = -10.4f;
	public const float rightBound = 10.4f;

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
