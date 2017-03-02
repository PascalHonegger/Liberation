using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	private const float NavMeshSampleDistance = 4f;

	private NavMeshAgent _agent;

	private void Start()
	{
		_agent = FindObjectOfType<NavMeshAgent>();
	}

	public void OnGroundClick(BaseEventData eventData)
	{
		var pointerEventData = (PointerEventData) eventData;

		NavMeshHit hit;

		_agent.SetDestination(NavMesh.SamplePosition(pointerEventData.pointerCurrentRaycast.worldPosition, out hit,
			NavMeshSampleDistance, NavMesh.AllAreas)
			? hit.position
			: pointerEventData.pointerCurrentRaycast.worldPosition);

		/*var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		destination.y = 0;
		_agent.SetDestination(destination);*/
	}

	private void Update()
	{
		//TODO Maybe there is a better solution?
		transform.eulerAngles = new Vector3(90, 0, 0);
	}
}