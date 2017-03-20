using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	public const string StartingPositionKey = "starting position";

	private const float NavMeshSampleDistance = 4f;

	private NavMeshAgent _agent;
	private Interactable _currentInteractable;

	private void Start()
	{
		_agent = FindObjectOfType<NavMeshAgent>();
	}

	public void OnGroundClick(BaseEventData eventData)
	{
		// The player is no longer headed for an interactable so set it to null.
		_currentInteractable = null;

		var pointerEventData = (PointerEventData) eventData;

		NavMeshHit hit;

		_agent.SetDestination(NavMesh.SamplePosition(pointerEventData.pointerCurrentRaycast.worldPosition, out hit,
			NavMeshSampleDistance, NavMesh.AllAreas)
			? hit.position
			: pointerEventData.pointerCurrentRaycast.worldPosition);
	}

	// This function is called by the EventTrigger on an Interactable, the Interactable component is passed into it.
	public void OnInteractableClick(Interactable interactable)
	{
		// Set the destination of the nav mesh agent to the found destination position and start the nav mesh agent going.
		_agent.SetDestination(interactable.interactionLocation.position);

		// Store the interactble that was clicked on.
		_currentInteractable = interactable;
	}

	private void Update()
	{
		//TODO Maybe there is a better solution?
		transform.eulerAngles = new Vector3(0, 0, 0);

		//Interact with the interactable
		if (_currentInteractable != null && !_agent.pathPending && Mathf.Approximately(_agent.remainingDistance, 0))
		{
			// Interact with the interactable and then null it out so this interaction only happens once.
			_currentInteractable.Interact();
			_currentInteractable = null;
		}
	}
}