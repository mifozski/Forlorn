using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class DialogueGraphView : GraphView
{
	public readonly Vector2 DefaultNodeSize = new Vector2(150, 200);

	public Blackboard Blackboard;
	public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
	private NodeSearchWindow _searchWindow;

	public DialogueGraphView(EditorWindow editorWindow)
	{
		styleSheets.Add(Resources.Load<StyleSheet>("DialogueGraph"));
		SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

		this.AddManipulator(new ContentDragger());
		this.AddManipulator(new SelectionDragger());
		this.AddManipulator(new RectangleSelector());

		var grid = new GridBackground();
		Insert(0, grid);
		grid.StretchToParentSize();

		AddElement(GenerateEntryPointNode());
		AddSearchWindow(editorWindow);
	}

	private DialogueNode GenerateEntryPointNode()
	{
		var node = new DialogueNode
		{
			title = "Start",
				GUID = Guid.NewGuid().ToString(),
				DialogueText = "ENTRYPOINT",
				EntryPoint = true,
		};

		var port = GeneratePort(node, Direction.Output);
		port.portName = "Next";
		node.outputContainer.Add(port);

		node.capabilities &= ~(Capabilities.Movable | Capabilities.Deletable);

		node.RefreshExpandedState();
		node.RefreshPorts();

		node.SetPosition(new Rect(100, 200, 100, 150));

		return node;
	}

	public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
	{
		var compatiblePorts = new List<Port>();

		ports.ForEach(port =>
		{
			if (startPort != port && startPort.node != port.node)
			{
				compatiblePorts.Add(port);
			}
		});

		return compatiblePorts;
	}

	private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
	{
		return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
	}

	public void CreateNode(string nodeName, Vector2 position)
	{
		AddElement(CreateDialogueNode(nodeName, position));
	}

	public DialogueNode CreateDialogueNode(string nodeName, Vector2 position)
	{
		var node = new DialogueNode
		{
			title = nodeName,
				DialogueText = nodeName,
				GUID = Guid.NewGuid().ToString(),
		};

		var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
		inputPort.name = "Input";
		node.inputContainer.Add(inputPort);

		node.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

		var button = new Button(() => { AddChoicePort(node); });
		button.text = "New Choice";
		node.titleContainer.Add(button);

		var textField = new TextField(string.Empty);
		textField.RegisterValueChangedCallback(evt =>
		{
			node.DialogueText = evt.newValue;
			node.title = evt.newValue;
		});
		textField.SetValueWithoutNotify(node.title);
		node.mainContainer.Add(textField);

		node.RefreshExpandedState();
		node.RefreshPorts();
		node.SetPosition(new Rect(position, DefaultNodeSize));

		return node;
	}

	public void AddChoicePort(DialogueNode node, string overriddenPortName = "")
	{
		var port = GeneratePort(node, Direction.Output);

		var oldLabel = port.contentContainer.Q<Label>("type");
		port.contentContainer.Remove(oldLabel);

		var outputPortCount = node.outputContainer.Query("connector").ToList().Count;

		var choicePortName = string.IsNullOrEmpty(overriddenPortName) ? $"Choice {outputPortCount}" : overriddenPortName;

		var textField = new TextField()
		{
			name = string.Empty,
				value = choicePortName,
		};
		textField.RegisterValueChangedCallback(evt => port.portName = evt.newValue);
		port.contentContainer.Add(new Label("  "));
		port.contentContainer.Add(textField);
		var deleteButton = new Button(() => RemovePort(node, port))
		{
			text = "X"
		};
		port.contentContainer.Add(deleteButton);

		port.portName = choicePortName;

		node.outputContainer.Add(port);
		node.RefreshExpandedState();
		node.RefreshPorts();
	}

	private void RemovePort(DialogueNode node, Port port)
	{
		var targetEdge = edges.ToList().Where(x => x.output.portName == port.portName && x.output.node == port.node);

		if (!targetEdge.Any())
		{
			return;
		}

		var edge = targetEdge.First();
		edge.input.Disconnect(edge);
		RemoveElement(targetEdge.First());

		node.outputContainer.Remove(port);
		node.RefreshPorts();
		node.RefreshExpandedState();
	}

	private void AddSearchWindow(EditorWindow editorWindow)
	{
		_searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
		_searchWindow.Init(editorWindow, this);
		nodeCreationRequest = context => SearchWindow.Open(
			new SearchWindowContext(context.screenMousePosition),
			_searchWindow
		);
	}

	public void AddPropertyToBlackboard(ExposedProperty exposedProperty)
	{
		var localPropertyName = exposedProperty.PropertyName;
		var localPropertyValue = exposedProperty.PropertyValue;
		var i = 1;
		while (ExposedProperties.Any(x => x.PropertyName == localPropertyName))
		{
			localPropertyName = $"{exposedProperty.PropertyName}({i})";
			i++;
		}

		var property = new ExposedProperty();
		property.PropertyName = localPropertyName;
		property.PropertyValue = localPropertyValue;
		ExposedProperties.Add(property);

		var container = new VisualElement();
		var blacboardField = new BlackboardField { text = property.PropertyName, typeText = "string property" };
		container.Add(blacboardField);

		var propertyValueTextField = new TextField("Value:")
		{
			value = localPropertyValue,
		};
		propertyValueTextField.RegisterValueChangedCallback(evt =>
		{
			var changingPropertyIndex = ExposedProperties.FindIndex(x => x.PropertyName == evt.newValue);
			ExposedProperties[changingPropertyIndex].PropertyValue = evt.newValue;
		});
		var blackboardValueRow = new BlackboardRow(blacboardField, propertyValueTextField);
		container.Add(blackboardValueRow);

		Blackboard.Add(container);
	}

	public void ClearBlackboardAndExposedProperties()
	{
		ExposedProperties.Clear();
		Blackboard.Clear();
	}
}
