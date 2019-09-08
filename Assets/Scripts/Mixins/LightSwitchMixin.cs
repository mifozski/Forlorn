using UnityEngine;
using System.Linq;

using Forlorn;

namespace Forlorn
{
	[RequireComponent(typeof(BoxCollider), typeof(InteractiveMixin))]
	[RequireComponent(typeof(AudioSource))]
	public class LightSwitchMixin : MonoBehaviour
	{
		[SerializeField] SwitchableLightMixin[] lights;

		[SerializeField] string switchedOffSubtitles;
		[SerializeField] string switchedOnSubtitles;

		private AudioSource clicking;

		Animator toggleAnimator;

		string emissiveColorParam = "_EmissiveColor";
		Material indicatorMat;
		Color indicatorColor;

		protected InteractiveMixin interactive;

		void Awake()
		{
			clicking = GetComponent<AudioSource>();
			toggleAnimator = GetComponent<Animator>();
			interactive = GetComponent<InteractiveMixin>();
			indicatorMat = GetComponent<MeshRenderer>().material;
			indicatorColor = indicatorMat.GetColor(emissiveColorParam);
		}

		void Start()
		{
			if (lights == null || lights.Length == 0)
			{
				Debug.LogWarning($"No lights set for switcher '{gameObject.name}'");
				return;
			}

			bool isOn = false;
			try
			{
				/* bool  */
				isOn = lights.Where(light => light.IsOn()).Count() > 0;
			}
			catch
			{
				// var we = 5;
			}
			// Sync all lights to be in the same switch state
			foreach (SwitchableLightMixin light in lights)
			{
				light.lightIsOn = isOn;
			}

			toggleAnimator.SetBool("TurnedOn", isOn);
			interactive.onHoverSubtitles = toggleAnimator.GetBool("TurnedOn") ? switchedOnSubtitles : switchedOffSubtitles;
			indicatorMat.SetColor(emissiveColorParam, !isOn ? indicatorColor : Color.black);
		}

		public void OnInteracted()
		{
			foreach (SwitchableLightMixin light in lights)
				light.lightIsOn = !light.lightIsOn;

			clicking.Play();

			toggleAnimator.SetBool("TurnedOn", !toggleAnimator.GetBool("TurnedOn"));

			interactive.onHoverSubtitles = toggleAnimator.GetBool("TurnedOn") ? switchedOnSubtitles : switchedOffSubtitles;
			indicatorMat.SetColor(emissiveColorParam, !isOn ? indicatorColor : Color.black);
		}

		bool isOn
		{
			get
			{
				return lights.Where(light => light.IsOn()).Count() > 0;
			}
		}
	}
}