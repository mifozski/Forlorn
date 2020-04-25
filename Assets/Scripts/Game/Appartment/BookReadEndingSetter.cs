using UnityEngine;

using Forlorn;

public class BookReadEndingSetter : MonoBehaviour
{
	public void Set()
	{
		SceneController.Instance.FadeAndLoadScene("Basement");
	}
}
