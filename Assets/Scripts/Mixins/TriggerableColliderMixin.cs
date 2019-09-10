using UnityEngine;
using UnityEngine.Events;

namespace Forlorn
{
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(InteractiveMixin))]
	public class TriggerableColliderMixin : MonoBehaviour
	{
		[SerializeField] UnityAction onTrigger;

		bool triggered = false;

		// protected

		// void Awake()
		// {
		// 	doorAnimator = GetComponentInParent<Animator>();
		// 	if (doorAnimator == null)
		// 		Debug.LogError("No animator on a door");
		// 	interactive = GetComponent<InteractiveMixin>();
		// }

		// void OnTriggerEnter(Collider other)
		// {
		// 	if (0 != (layers.value & 1 << other.gameObject.layer))
		// 	{
		// 		ExecuteOnEnter(other);
		// 	}
		// }

		// void OnTriggerExit(Collider other)
		// {
		// 	if (0 != (layers.value & 1 << other.gameObject.layer))
		// 	{
		// 		ExecuteOnExit(other);
		// 	}
		// }
	}
}