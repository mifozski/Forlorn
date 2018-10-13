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

		void Update ()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				mainMenuCanvas.enabled = !mainMenuCanvas.enabled;

				store.dispatch(GameGeneral.ActionCreators.enterMainMenu(mainMenuCanvas.enabled));
			}
		}
	}
}