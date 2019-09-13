using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Forlorn;

namespace Forlorn
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] Canvas mainMenuCanvas;

		private bool m_cursorIsLocked = true;

		void Start()
		{
			mainMenuCanvas.enabled = false;
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				mainMenuCanvas.enabled = !mainMenuCanvas.enabled;
				ImmediateGameState.isInMainMenu = mainMenuCanvas.enabled;

				m_cursorIsLocked = !mainMenuCanvas.enabled;
			}

			if (m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}
	}
}