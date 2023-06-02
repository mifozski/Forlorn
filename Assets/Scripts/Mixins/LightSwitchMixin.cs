#pragma warning disable 649

using UnityEngine;
using System.Linq;
using Serialization;
using System.Runtime.Serialization;

namespace Forlorn
{
	[RequireComponent(typeof(AudioSource))]
	public class LightSwitchMixin : MonoBehaviour, OnDeserializedCallback, OnSerializeCallback
	{
		[SerializeField] SwitchableLightMixin[] lights;

		[SerializeField] string switchedOffSubtitles;
		[SerializeField] string switchedOnSubtitles;

		private AudioSource clicking;

		Animator toggleAnimator;

		string emissiveColorParam = "_EmissionColor";
		Material indicatorMat;
		Color indicatorColor;

		string turnedOnParamKey = "TurnedOn";

		protected InteractiveMixin interactive;

		void Awake()
		{
			clicking = GetComponent<AudioSource>();
			toggleAnimator = GetComponent<Animator>();
			interactive = GetComponentInChildren<InteractiveMixin>();

			interactive.onInteractedThisFrame += OnInteractedThisFrame;

			indicatorMat = GetComponentsInChildren<MeshRenderer>()[1].material;
			indicatorColor = indicatorMat.GetColor(emissiveColorParam);
		}

		void Start()
		{
			if (lights == null || lights.Length == 0)
			{
				Debug.LogError($"No lights set for switcher '{gameObject.name}'");
				return;
			}

			bool heuristicallyTurnedOn = false;
			try
			{
				heuristicallyTurnedOn = lights.Where(light => light.lightIsOn).Count() > 0;
			}
			catch
			{
				// var we = 5;
			}
			// Sync all lights to be in the same switch state
			toggleAnimator.SetBool(turnedOnParamKey, heuristicallyTurnedOn);

			UpdateState();
		}

		public void OnInteractedThisFrame()
		{
			Debug.LogError("interacted");
			toggleAnimator.SetBool(turnedOnParamKey, !IsOn);

			clicking.Play();

			UpdateState();
		}

		void UpdateState()
		{
			foreach (SwitchableLightMixin light in lights)
			{
				light.lightIsOn = IsOn;
			}

			interactive.onHoverSubtitles = IsOn ? switchedOnSubtitles : switchedOffSubtitles;
			indicatorMat.SetColor(emissiveColorParam, !IsOn ? indicatorColor : Color.black);
		}

		public void OnDeserialized()
		{
			UpdateState();
		}

		public void OnSerialize(ref SerializationInfo info)
		{
			info.AddValue(turnedOnParamKey, IsOn);
		}

		public void OnDeserialize(SerializationInfo info)
		{
			toggleAnimator.SetBool(turnedOnParamKey, (bool)info.GetValue(turnedOnParamKey, typeof(bool)));
			UpdateState();
		}

		bool IsOn => toggleAnimator.GetBool(turnedOnParamKey);
	}
}
