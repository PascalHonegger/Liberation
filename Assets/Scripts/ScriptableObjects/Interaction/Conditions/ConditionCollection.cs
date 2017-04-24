using System.Linq;
using UnityEngine;

// This class represents a single outcome from clicking
// on an interactable.  It has an array of Conditions
// and if they are all met an ReactionCollection that
// will happen.
public class ConditionCollection : ScriptableObject
{
	public string description;
		// Description of the ConditionCollection.  This is used purely for identification in the inspector.

	public Condition[] requiredConditions = new Condition[0];
		// The Conditions that need to be met in order for the ReactionCollection to React.

	public ReactionCollection reactionCollection;
		// Reference to the ReactionCollection that will React should all the Conditions be met.

	public Item dragDropItem;
		// Reference to the Item which has to be drag-and-dropped.

	// This is called by the Interactable one at a time for each of its ConditionCollections until one returns true.
	public bool CheckAndReact(Item dragDropItem)
	{
		if(!Equals(this.dragDropItem, dragDropItem))
		{
			return false;
		}

		// Go through all Conditions...
		if (requiredConditions.Any(c => !AllConditions.CheckCondition(c)))
		{
			return false;
		}

		// If there is an ReactionCollection assigned, call its React function.
		if (reactionCollection)
			reactionCollection.React();

		// A Reaction happened so return true.
		return true;
	}
}