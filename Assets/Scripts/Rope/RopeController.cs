using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class RopeController : MonoBehaviour
{
	[SerializeField] private float bendRadius;
	
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
	private LineRenderer lineRenderer;
	private LineRendererSmoother lineRendererSmoother;
	private Vector2 startBendPosition;
	private Vector3[] currentRopePoints;
	
	private void Start()
	{
		GetDependencies();
		lineRendererSmoother.InitializeRope();
		currentRopePoints = new Vector3[lineRenderer.positionCount];
		
		Enabled = true;
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
		Vector2 closestPoint = GetClosestPoint(fingerMovePosition);
		
		float bendStrength = fingerMovePosition.x - startBendPosition.x;
		Debug.Log(bendStrength);
	
		for (int i = 0; i < lineRenderer.positionCount; i++)
		{
			var point = lineRenderer.GetPosition(i);
			
			point.x += bendStrength * Mathf.Exp(-Mathf.Pow(point.y - fingerMovePosition.y / 4, 2) / (2 * bendRadius * bendRadius));
			currentRopePoints[i] = point;
		}
		
		lineRenderer.SetPositions(currentRopePoints);
		lineRendererSmoother.GenerateCollider();
	}
	
	private Vector2 GetClosestPoint(Vector2 touchPosition)
	{
		Vector2[] points = new Vector2[lineRenderer.positionCount];
		float maxDistance = 0;
		Vector2 maxPoint = touchPosition;
		
		for (int i = 0; i < lineRenderer.positionCount; i++)
		{
			points[i] = lineRenderer.GetPosition(i);
			float distance = Vector2.Distance(touchPosition, points[i]);
			if (distance > maxDistance)
			{
				maxPoint = points[i];
				maxDistance = distance;
			}
		}
		
		return maxPoint;
	}
	
	private void GetDependencies()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		
		lineRenderer = GetComponent<LineRenderer>();
		lineRendererSmoother = GetComponent<LineRendererSmoother>();
	}
	
	private void OnDestroy()
	{
		Enabled = false;
	}
}
