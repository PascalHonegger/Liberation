using UnityEngine;

// This Reaction has a delay but is not a DelayedReaction.
// This is because the TextManager component handles the
// delay instead of the Reaction.
public class TextReaction : DelayedReaction
{
	public string message; // The text to be displayed to the screen.

	public Color textColor = Color.white;
		// The color of the text when it's displayed (different colors for different characters talking).


	private TextManager textManager; // Reference to the component to display the text.


	protected override void SpecificInit()
	{
		textManager = FindObjectOfType<TextManager>();
	}


	protected override void ImmediateReaction()
	{
		textManager.DisplayMessage(message, textColor);
	}
}