﻿using UnityEngine;
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
	}

	private void Update()
	{
		//TODO Maybe there is a better solution?
		transform.eulerAngles = new Vector3(0, 0, 0);
	}
}