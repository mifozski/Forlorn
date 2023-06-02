using UnityEngine;
using UnityEngine.Events;

namespace Forlorn
{
	[RequireComponent(typeof(Collider))]
	public class InteractOnTrigger : MonoBehaviour
	{
		public LayerMask layers;
		public UnityEvent OnEnter, OnExit;
		new Collider collider;

		void Reset()
		{
			layers = LayerMask.NameToLayer("Everything");
			collider = GetComponent<Collider>();
			collider.isTrigger = true;
		}

		void OnTriggerEnter(Collider other)
		{
			if (_triggered)
			{
				return;
			}

			if (0 != (layers.value & 1 << other.gameObject.layer))
			{

				_triggered = true;
				ExecuteOnEnter(other);
			}
		}

		protected virtual void ExecuteOnEnter(Collider other)
		{
			OnEnter.Invoke();
		}

		void OnTriggerExit(Collider other)
		{
			if (0 != (layers.value & 1 << other.gameObject.layer))
			{
				ExecuteOnExit(other);
			}
		}

		protected virtual void ExecuteOnExit(Collider other)
		{
			OnExit.Invoke();
		}

		void OnDrawGizmos()
		{
			Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
		}

		void OnDrawGizmosSelected()
		{
			//need to inspect events and draw arrows to relevant gameObjects.
		}

		bool _triggered = false;
	}
}
