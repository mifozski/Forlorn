using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityStandardAssets.Characters.FirstPerson;

namespace Forlorn.Core
{
	public class FootstepsSystem : MonoBehaviour
	{
		[SerializeField] AudioClip[] m_DefaultFootsteps;
		[SerializeField] GroundType[] m_GroundTypes;

		private Dictionary<string, GroundType> m_GroundTypeDict = new Dictionary<string, GroundType>();
		private string m_CurrentType = null;
		private FirstPersonController firstPersonController;

		void Start()
		{
			m_GroundTypeDict = m_GroundTypes.ToDictionary(type => type.tagName, type => type);
			firstPersonController = FindObjectOfType<FirstPersonController>();
		}

		public void OnStepOnObjectWithTag(string tagName)
		{
			if (m_GroundTypeDict.ContainsKey(tagName))
			{
				GroundType type = m_GroundTypeDict[tagName];

				if (m_CurrentType != tagName)
				{
					Debug.Log($"STEPPED ON {tagName}");
					firstPersonController.SetFootsteps(type.footsteps, type.volumeMultiplier);
					m_CurrentType = tagName;
				}
			}
			else if (m_CurrentType != null)
			{
				firstPersonController.SetFootsteps(m_DefaultFootsteps, 1.0f);
				m_CurrentType = null;
			}
		}
	}

	[System.Serializable]
	public class GroundType
	{
		public string tagName;
		public AudioClip[] footsteps;
		[Range(0, 2)]
		public float volumeMultiplier = 1.0f;
	}
}