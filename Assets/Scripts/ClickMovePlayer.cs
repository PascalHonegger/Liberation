using UnityEngine;
using UnityEngine.AI;

public class ClickMovePlayer : MonoBehaviour
{
	private const float NavMeshSampleDistance = 4f;

	public NavMeshAgent AgentToMove;

	private void OnMouseDown()
	{
		NavMeshHit hit;

		var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		destination.y = 0;

		AgentToMove.SetDestination(NavMesh.SamplePosition(destination, out hit,
			NavMeshSampleDistance, NavMesh.AllAreas)
			? hit.position
			: destination);
	}
}