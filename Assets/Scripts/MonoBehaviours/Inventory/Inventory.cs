using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public ItemDragDropHandler[] itemHandlers = new ItemDragDropHandler[numItemSlots];

	public const int numItemSlots = 3;

	// This function is called by the PickedUpItemReaction in order to add an item to the inventory.
	public void AddItem(Item itemToAdd)
	{
		// Go through all the item slots...
		var emptyItemHandler = itemHandlers.FirstOrDefault(h => h.Item == null);

		if(emptyItemHandler)
		{
			emptyItemHandler.Item = itemToAdd;
		}
	}

	// This function is called by the LostItemReaction in order to remove an item from the inventory.
	public void RemoveItem(Item itemToRemove)
	{
		var itemHandler = itemHandlers.FirstOrDefault(h => h.Item == itemToRemove);

		if(itemHandler)
		{
			itemHandler.Item = null;
		}
	}
}