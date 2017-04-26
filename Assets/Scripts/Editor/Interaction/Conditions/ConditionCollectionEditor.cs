using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConditionCollection))]
public class ConditionCollectionEditor : Editor
{
	public SerializedProperty collectionsProperty;              // Represents the array of ConditionCollections that the target belongs to.


	private ConditionCollection conditionCollection;            // Reference to the target.
	private SerializedProperty descriptionProperty;             // Represents a string description for the target.
	private SerializedProperty dragDropItemProperty;            // Represents the item which has to be drag-and-dropped that is referenced by the target.
	private SerializedProperty reactionCollectionProperty;      // Represents the ReactionCollection that is referenced by the target.


	private const float conditionButtonWidth = 30f;             // Width of the button for adding a new Condition.
	private const float collectionButtonWidth = 125f;           // Width of the button for removing the target from it's Interactable.
	private const string conditionCollectionPropDescriptionName = "description";
	// Name of the field that represents an array of Conditions for the target.
	private const string conditionCollectionPropReactionCollectionName = "reactionCollection";
	// Name of the field that represents the ReactionCollection that is referenced by the target.
	private const string dragDropItemPropReactionCollectionName = "dragDropItem";


	private void OnEnable()
	{
		// Cache a reference to the target.
		conditionCollection = (ConditionCollection)target;

		// If this Editor exists but isn't targeting anything destroy it.
		if (target == null)
		{
			DestroyImmediate(this);
			return;
		}

		// Cache the SerializedProperties.
		descriptionProperty = serializedObject.FindProperty(conditionCollectionPropDescriptionName);
		reactionCollectionProperty = serializedObject.FindProperty(conditionCollectionPropReactionCollectionName);
		dragDropItemProperty = serializedObject.FindProperty(dragDropItemPropReactionCollectionName);
	}


	public override void OnInspectorGUI()
	{
		// Pull the information from the target into the serializedObject.
		serializedObject.Update();

		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUI.indentLevel++;

		EditorGUILayout.BeginHorizontal();

		// Use the isExpanded bool for the descriptionProperty to store whether the foldout is open or closed.
		descriptionProperty.isExpanded = EditorGUILayout.Foldout(descriptionProperty.isExpanded, descriptionProperty.stringValue);

		// Display a button showing 'Remove Collection' which removes the target from the Interactable when clicked.
		if (GUILayout.Button("Remove Collection", GUILayout.Width(collectionButtonWidth)))
		{
			collectionsProperty.RemoveFromObjectArray(conditionCollection);
		}

		EditorGUILayout.EndHorizontal();

		// If the foldout is open show the expanded GUI.
		if (descriptionProperty.isExpanded)
		{
			ExpandedGUI();
		}

		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical();

		// Push all changes made on the serializedObject back to the target.
		serializedObject.ApplyModifiedProperties();
	}


	private void ExpandedGUI()
	{
		EditorGUILayout.Space();

		// Display the description for editing.
		EditorGUILayout.PropertyField(descriptionProperty);

		// Display the reference to the item which has to be drag-and-dropped onto this interactable.
		EditorGUILayout.PropertyField(dragDropItemProperty);

		// Display the reference to the ReactionCollection for editing.
		EditorGUILayout.PropertyField(reactionCollectionProperty);
	}


	// This function is static such that it can be called without an editor being instanced.
	public static ConditionCollection CreateConditionCollection()
	{
		// Create a new instance of ConditionCollection.
		ConditionCollection newConditionCollection = CreateInstance<ConditionCollection>();

		// Give it a default description.
		newConditionCollection.description = "New condition collection";

		return newConditionCollection;
	}
}
