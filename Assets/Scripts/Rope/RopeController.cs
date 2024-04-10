using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class RopeController : MonoBehaviour
{
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private EdgeCollider2D edgeCollider;
	[SerializeField] private int ropePrecision;
	[SerializeField] private float bendRadius;
	[SerializeField] private float bendStrength;
	[SerializeField] private Transform player;
	[SerializeField] private float ropeSpawnThreshold;
	[SerializeField] private float ropePointsRefreshDelta;

	public bool Enabled
	{
		get => isEnabled;
		set
		{
			isEnabled = value;
			if (value)
			{
				Touch.onFingerDown += RopeFingerDown;
				Touch.onFingerMove += RopeFignerMove;
			}
			else
			{
				Touch.onFingerDown -= RopeFingerDown;
				Touch.onFingerMove -= RopeFignerMove;
			}
		}
	}

	private bool isEnabled;

	private Vector2 screenSize;
	private Vector2 startBendPosition;
	private Vector3[] currentRopePoints;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		Application.targetFrameRate = 30;
		screenSize = CustomExtensions.sizeOfTheScreen;
		lineRenderer.positionCount = ropePrecision;

		SetInitialRope();
	}

	private void Update()
	{
		if (!isEnabled) return;

		if (player.position.y + screenSize.y + ropePointsRefreshDelta >= lineRenderer.GetPosition(lineRenderer.positionCount - 1).y)
		{
			currentRopePoints[lineRenderer.positionCount - 1] = new Vector2(0, lineRenderer.GetPosition(lineRenderer.positionCount - 1).y + ropeSpawnThreshold * Time.deltaTime * 100);

			for (int i = lineRenderer.positionCount - 2; i >= 0; i--)
			{
				currentRopePoints[i] = lineRenderer.GetPosition(i + 1);
			}

			lineRenderer.SetPositions(currentRopePoints);
			GenerateCollider();
		}
	}

	private void RopeFingerDown(Finger finger)
	{
		startBendPosition = CustomExtensions.ScreenPointFromWorldPoint(finger.screenPosition);
	}

	private void RopeFignerMove(Finger finger)
	{
		RopePointsUpdate(CustomExtensions.ScreenPointFromWorldPoint(finger.screenPosition));
	}

	private void RopePointsUpdate(Vector2 fingerMovePosition)
	{
		Debug.Log(fingerMovePosition);
		int direction = 0;

		if (fingerMovePosition.x - startBendPosition.x > 0)
		{
			direction = 1;
		}
		else
		{
			direction = -1;
		}

		for (int i = 0; i < lineRenderer.positionCount; i++)
		{
			var point = lineRenderer.GetPosition(i);
			var bendValue = Mathf.Exp(-Mathf.Pow(point.y - fingerMovePosition.y, 2) / (2 * bendRadius * bendRadius));

			point.x += direction * bendStrength * bendValue * Time.deltaTime;

			currentRopePoints[i] = point;
		}
		startBendPosition = fingerMovePosition;

		lineRenderer.SetPositions(currentRopePoints);
		GenerateCollider();
	}

	private void OnDestroy()
	{
		Enabled = false;
	}

	public void SetInitialRope()
	{
		Vector3[] positions = new Vector3[ropePrecision];

		float step = 2 * screenSize.y / (ropePrecision - 8);
		float yPos = -screenSize.y;

		for (int i = 0; i < positions.Length; i++)
		{
			positions[i].x = 0;
			positions[i].y = yPos;
			yPos += step;
		}

		ropeSpawnThreshold = step;
		lineRenderer.SetPositions(positions);
		currentRopePoints = positions;
		GenerateCollider();
	}

	public void GenerateCollider()
	{
		List<Vector2> edges = new List<Vector2>();

		for (int i = 0; i < lineRenderer.positionCount; i++)
		{
			Vector3 point = lineRenderer.GetPosition(i);
			edges.Add(new Vector2(point.x, point.y));
		}

		edgeCollider.SetPoints(edges);
	}
}
