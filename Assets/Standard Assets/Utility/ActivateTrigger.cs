using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace UnityStandardAssets.Utility
{
	public class ActivateTrigger : MonoBehaviour
	{
		// A multi-purpose script which causes an action to occur when
		// a trigger collider is entered.
		public enum Mode
		{
			Trigger = 0,    // Just broadcast the action on to the target
			Replace = 1,    // replace target with source
			Activate = 2,   // Activate the target GameObject
			Enable = 3,     // Enable a component
			Animate = 4,    // Start animation on target
			Deactivate = 5,  // Decativate target GameObject
			Action = 6,
		}

		public bool active = true;

		public Mode action = Mode.Activate;         // The action to accomplish
		public Object target;                       // The game object to affect. If none, the trigger work on this game object
		public GameObject source;
		public int triggerCount = 1;
		public bool repeatTrigger = false;
		public UnityEvent onTrigger;

		private void DoActivateTrigger()
		{
			triggerCount--;

			if (triggerCount == 0 || repeatTrigger)
			{
				Object currentTarget = target ?? gameObject;
				Behaviour targetBehaviour = currentTarget as Behaviour;
				GameObject targetGameObject = currentTarget as GameObject;
				if (targetBehaviour != null)
				{
					targetGameObject = targetBehaviour.gameObject;
				}

				switch (action)
				{
					case Mode.Trigger:
						if (targetGameObject != null)
						{
							targetGameObject.BroadcastMessage("DoActivateTrigger");
						}
						break;
					case Mode.Replace:
						if (source != null)
						{
							if (targetGameObject != null)
							{
								Instantiate(source, targetGameObject.transform.position,
											targetGameObject.transform.rotation);
								Destroy(targetGameObject);
							}
						}
						break;
					case Mode.Activate:
						if (targetBehaviour != null)
						{
							targetBehaviour.enabled = true;

						}
						else if (targetGameObject != null)
						{
							targetGameObject.SetActive(true);
						}
						break;
					case Mode.Enable:
						if (targetBehaviour != null)
						{
							targetBehaviour.enabled = true;
						}
						break;
					case Mode.Animate:
						if (targetGameObject != null)
						{
							targetGameObject.GetComponent<Animation>().Play();
						}
						break;
					case Mode.Deactivate:
						if (targetGameObject != null)
						{
							targetGameObject.SetActive(false);
						}
						break;
					case Mode.Action:
						onTrigger.Invoke();
						break;
				}
			}
		}

		private void DeactivateTrigger()
		{
			Object currentTarget = target ?? gameObject;
			Behaviour targetBehaviour = currentTarget as Behaviour;
			GameObject targetGameObject = currentTarget as GameObject;
			if (targetBehaviour != null)
			{
				targetGameObject = targetBehaviour.gameObject;
			}

			switch (action)
			{
				case Mode.Activate:
					{
						if (targetBehaviour != null)
						{
							targetBehaviour.enabled = false;

						}
						else if (targetGameObject != null)
						{
							targetGameObject.SetActive(false);
						}
						break;
					}
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == ("Player"))
			{
				DoActivateTrigger();
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.tag == ("Player"))
			{
				DeactivateTrigger();
			}
		}
	}
}
