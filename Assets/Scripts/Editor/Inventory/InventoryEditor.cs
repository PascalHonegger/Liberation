using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
	private SerializedProperty itemHandlersProperty;

	private const string inventoryPropItemHandlersName = "itemHandlers";

	private void OnEnable()
	{
		// Cache the SerializedProperties.
		itemHandlersProperty = serializedObject.FindProperty(inventoryPropItemHandlersName);
	}

	public override void OnInspectorGUI()
	{
		// Pull all the information from the target into the serializedObject.
		serializedObject.Update();

		for (int i = 0; i < Inventory.numItemSlots; i++)
		{
			var itemHandler = itemHandlersProperty.GetArrayElementAtIndex(i);

			EditorGUILayout.PropertyField(itemHandler);
		}

		// Push all the information from the serializedObject back into the target.
		serializedObject.ApplyModifiedProperties();
	}
}
