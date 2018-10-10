using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Forlorn;

namespace Forlorn {
public class MainMenu : MonoBehaviour
{
	[SerializeField] Canvas mainMenuCanvas;
	[SerializeField] PlayerController playerController;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			mainMenuCanvas.enabled = !mainMenuCanvas.enabled;

			playerController.isPaused = mainMenuCanvas.enabled;
		}
	}
}
}