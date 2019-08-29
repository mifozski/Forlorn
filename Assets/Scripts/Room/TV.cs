using UnityEngine;
using System.Linq;

using Forlorn;

namespace Forlorn
{
	[RequireComponent(typeof(BoxCollider), typeof(InteractiveMixin))]
	public class TV : MonoBehaviour
	{
		protected InteractiveMixin interactive;

		void Awake()
		{
        }

		void Start()
		{
		}

		public void OnInteracted()
		{
			GameController.ShowSubtitles("I don't really wanna watch TV now");
		}
	}
}