using System;
using System.Collections.Generic;
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
	
	public bool Enabled 
	{
		get => isEnabled;
		set
		{
			isEnabled = value;
			if (value)
			{
				Touch.onFingerDown += OnFingerDownHandler;
				Touch.onFingerMove += OnFingerMoveHandler;
			}
			else
			{
				Touch.onFingerDown -= OnFingerDownHandler;
				Touch.onFingerMove -= OnFingerMoveHandler;
			}
		}
	}
	
	private bool isEnabled;
	
	private Vector2 screenSize;
	private Vector2 startBendPosition;
	private Vector3[] currentRopePoints;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		
		screenSize = CustomExtensions.screenSize;
		lineRenderer.positionCount = ropePrecision;
		
		InitializeRope();
	}
	
	private void Update()
	{
		if (!isEnabled) return;
		
		if (player.position.y + screenSize.y >= lineRenderer.GetPosition(lineRenderer.positionCount - 1).y)
		{
			currentRopePoints[lineRenderer.positionCount - 1] = new Vector2(0, lineRenderer.GetPosition(lineRenderer.positionCount - 1).y + ropeSpawnThreshold);
			
			for (int i = lineRenderer.positionCount - 2; i >= 0; i--)
			{
				currentRopePoints[i] = lineRenderer.GetPosition(i + 1);
			}
			
			lineRenderer.SetPositions(currentRopePoints);
			GenerateCollider();
		}
	}
	
	private void OnFingerDownHandler(Finger finger)
	{
		startBendPosition = CustomExtensions.ScreenVector2ToWorld(finger.screenPosition);
	}
	
	private void OnFingerMoveHandler(Finger finger)
	{
		UpdateRopePoints(CustomExtensions.ScreenVector2ToWorld(finger.screenPosition));
	}
	
	private void UpdateRopePoints(Vector2 fingerMovePosition)
	{
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
			
			point.x += direction * bendStrength * bendValue;
			//* Mathf.Abs(Mathf.Abs(point.x) - screenSize.x) / screenSize.x;
			
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
	
	public void InitializeRope()
	{
		Vector3[] positions = new Vector3[ropePrecision];
		
		float step = 2 * screenSize.y / (ropePrecision - 8);
		float yPos = - 2 * screenSize.y / ropePrecision;
		
		for (int i = 0; i < positions.Length; i++)
		{
			positions[i].x = 0;
			positions[i].y = yPos - screenSize.y;
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
