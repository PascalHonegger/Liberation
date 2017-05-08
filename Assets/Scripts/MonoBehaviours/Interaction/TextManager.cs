using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// This class is used to manage the text that is
// displayed on screen.  In situations where many
// messages are triggered one after another it
// makes sure they are played in the correct order.
public class TextManager : MonoBehaviour
{
	public Text text; // Reference to the Text component that will display the message.

	public float displayTimePerCharacter = 0.1f;
		// The amount of time that each character in a message adds to the amount of time it is displayed for.

	public float additionalDisplayTime = 0.5f; // The additional time that is added to the message is displayed for.


	// Collection of instructions that are ordered by their startTime.
	private float _clearTime; // The time at which there should no longer be any text on screen.


	private void Update()
	{
		// Otherwise, if the time is beyond the clear time, clear the text component's text.
		if (Time.time >= _clearTime)
		{
			text.text = string.Empty;
		}
	}


	// This function is called from TextReactions in order to display a message to the screen.
	public void DisplayMessage(string message, Color textColor)
	{
		// Calculate how long the message should be displayed for based on the number of characters.
		float displayDuration = message.Length * displayTimePerCharacter + additionalDisplayTime;

		// Create a new clear time
		_clearTime = Time.time + displayDuration;

		// Set the Text component to display the instruction's message in the correct color.
		text.text = message;
		text.color = textColor;
	}
}