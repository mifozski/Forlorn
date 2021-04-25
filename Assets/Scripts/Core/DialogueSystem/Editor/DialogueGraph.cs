using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using UnityEditor.Experimental.GraphView;
using System.Linq;

public class DialogueGraph : EditorWindow
{
	private DialogueGraphView _graphView;
	private string _fileName = "New Narrative";

	[MenuItem("Graph/Dialogue Graph")]
	public static void OpenDialogueGraphWindow()
	{
		var window = GetWindow<DialogueGraph>();
		window.titleContent = new GUIContent("Dialogue Graph");
	}

	private void OnEnable()
	{
		ConstructGraphView();
		GenerateToolbar();
		// GenerateMinimap();
		GenerateBlackboard();
	}

	private void ConstructGraphView()
	{
		_graphView = new DialogueGraphView(this)
		{
			name = "Dialogue Graph"
		};

		_graphView.StretchToParentSize();
		rootVisualElement.Add(_graphView);
	}

	private void GenerateToolbar()
	{
		var toolbar = new Toolbar();

		var fileNameTextField = new TextField("File Name:");
		fileNameTextField.SetValueWithoutNotify(_fileName);
		fileNameTextField.MarkDirtyRepaint();
		fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
		toolbar.Add(fileNameTextField);

		toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });
		toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data" });

		rootVisualElement.Add(toolbar);
	}

	private void RequestDataOperation(bool save)
	{
		if (string.IsNullOrEmpty(_fileName))
		{
			EditorUtility.DisplayDialog(
				"Invalid file name!",
				"Please enter a valid file name",
				"OK"
			);
		}

		var saveUtility = GraphSaveUtility.GetInstance(_graphView);
		if (save)
		{
			saveUtility.SaveGraph(_fileName);
		}
		else
		{
			saveUtility.LoadGraph(_fileName);
		}
	}

	private void GenerateMinimap()
	{
		var minimap = new MiniMap { anchored = true };
		var coords = _graphView.contentViewContainer.WorldToLocal(new Vector2(this.position.size.x - 10, 30));
		minimap.SetPosition(new Rect(coords.x, coords.y, 200, 140));
		_graphView.Add(minimap);
	}

	private void GenerateBlackboard()
	{
		var blackboard = new Blackboard(_graphView);
		blackboard.Add(new BlackboardSection { title = "Exposed Properties" });
		blackboard.addItemRequested = _ =>
		{
			_graphView.AddPropertyToBlackboard(new ExposedProperty());
		};
		blackboard.editTextRequested = (_, element, newValue) =>
		{
			var oldPropertyName = ((BlackboardField)element).text;

			if (_graphView.ExposedProperties.Any(x => x.PropertyName == newValue))
			{
				EditorUtility.DisplayDialog("Error", "This property name already exists", "OK");
				return;
			}

			var property = _graphView.ExposedProperties.Find(x => x.PropertyName == oldPropertyName);
			property.PropertyName = newValue;
			((BlackboardField)element).text = newValue;
		};
		blackboard.SetPosition(new Rect(10, 30, 200, 300));
		_graphView.Blackboard = blackboard;
		_graphView.Add(blackboard);
	}

	private void OnDisable()
	{
		rootVisualElement.Remove(_graphView);
	}
}
