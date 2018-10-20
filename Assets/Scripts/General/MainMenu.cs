using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Forlorn;

namespace Forlorn
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] Canvas mainMenuCanvas;

		public Redux.Store store;

		void Start()
		{
			mainMenuCanvas.enabled = false;
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				mainMenuCanvas.enabled = !mainMenuCanvas.enabled;

				store.dispatch(ActionCreators.GameGeneral.enterMainMenu(mainMenuCanvas.enabled));
			}
		}
	}
}