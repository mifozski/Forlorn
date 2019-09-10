using UnityEngine;
using UnityEditor;

namespace Forlorn
{
	[CustomEditor(typeof(Interactable))]
	public class InteractableEditor : Editor
	{
		private Interactable interactable;

		private void OnEnable()
		{
			interactable = (Interactable)target;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			if (LayerMask.LayerToName(interactable.gameObject.layer) != "Interactive")
			{
				EditorGUILayout.HelpBox("This object isn't a part of Interactive layer", MessageType.Error);
				if (GUILayout.Button("Fix", GUILayout.Width(30)))
				{
					interactable.gameObject.layer = LayerMask.NameToLayer("Interactive");

				}
			}

			base.OnInspectorGUI();
			serializedObject.ApplyModifiedProperties();
		}
	}

	// [CustomEditor(typeof(Interactable))]
	// public class InteractableEditor : EditorWithSubEditors<ConditionCollectionEditor, ConditionCollection>
	// {
	// 	private Interactable interactable;
	// 	private SerializedProperty interactionLocationProperty;
	// 	private SerializedProperty collectionsProperty;
	// 	private SerializedProperty defaultReactionCollectionProperty;

	// 	private const float collectionButtonWidth = 125f;
	// 	private const string interactablePropInteractionLocationName = "interactionLocation";
	// 	private const string interactablePropConditionCollectionsName = "conditionCollections";
	// 	private const string interactablePropDefaultReactionCollectionName = "defaultReactionCollection";

	// 	private void OnEnable()
	// 	{
	// 		interactable = (Interactable)target;

	// 		collectionsProperty = serializedObject.FindProperty(interactablePropConditionCollectionsName);
	// 		interactionLocationProperty = serializedObject.FindProperty(interactablePropInteractionLocationName);
	// 		defaultReactionCollectionProperty = serializedObject.FindProperty(interactablePropDefaultReactionCollectionName);

	// 		CheckAndCreateSubEditors(interactable.conditionCollections);
	// 	}


	// 	private void OnDisable()
	// 	{
	// 		CleanupEditors();
	// 	}


	// 	protected override void SubEditorSetup(ConditionCollectionEditor editor)
	// 	{
	// 		editor.collectionsProperty = collectionsProperty;
	// 	}


	// 	public override void OnInspectorGUI()
	// 	{
	// 		serializedObject.Update();

	// 		if (LayerMask.LayerToName(interactable.gameObject.layer) != "Interactive")
	// 		{
	// 			EditorGUILayout.HelpBox("This object isn't a part of Interactive layer", MessageType.Warning);
	// 		}

	// 		CheckAndCreateSubEditors(interactable.conditionCollections);

	// 		EditorGUILayout.PropertyField(interactionLocationProperty);

	// 		for (int i = 0; i < subEditors.Length; i++)
	// 		{
	// 			subEditors[i].OnInspectorGUI();
	// 			EditorGUILayout.Space();
	// 		}

	// 		EditorGUILayout.BeginHorizontal();
	// 		GUILayout.FlexibleSpace();
	// 		if (GUILayout.Button("Add Collection", GUILayout.Width(collectionButtonWidth)))
	// 		{
	// 			ConditionCollection newCollection = ConditionCollectionEditor.CreateConditionCollection();
	// 			collectionsProperty.AddToObjectArray(newCollection);
	// 		}
	// 		EditorGUILayout.EndHorizontal();

	// 		EditorGUILayout.Space();

	// 		EditorGUILayout.PropertyField(defaultReactionCollectionProperty);

	// 		serializedObject.ApplyModifiedProperties();
	// 	}
	// }
}
