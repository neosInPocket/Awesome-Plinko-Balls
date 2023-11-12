using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class RopeController : MonoBehaviour
{
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
	private Vector2 screenSize;
	
	private void Start()
	{
		GetDependencies();
		
		screenSize = CustomExtensions.GetScreenWorldSize();
		lineRenderer.positionCount = 100;
		Vector2[] positions = new Vector2[100];
		float startX = 0;
		float yPos = 0;
		float step = screenSize.x * 2 / 100;
		foreach(var position in positions)
		{
			
		}
		
		lineRenderer.SetPositions(startArray);
		lineRendererSmoother.GenerateCollider();
	}
	
	private void OnFingerDownHandler(Finger finger)
	{
		
	}
	
	private void OnFingerMoveHandler(Finger finger)
	{
		
	}
	
	private void GetDependencies()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		
		lineRenderer = GetComponent<LineRenderer>();
		lineRendererSmoother = GetComponent<LineRendererSmoother>();
	}
}
