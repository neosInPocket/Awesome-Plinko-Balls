using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineRendererSmoother : MonoBehaviour
{
	[SerializeField] private LineRenderer line;
	[SerializeField] private EdgeCollider2D edgeCollider;
	[SerializeField] private int precision = 100;
	[SerializeField] private Vector2[] InitialState = new Vector2[1];
	[SerializeField] private float SmoothingLength = 2f;
	[SerializeField] private int SmoothingSections = 10;
	private Vector2 screenSize;
	
	public void InitializeRope()
	{
		screenSize = CustomExtensions.GetScreenWorldSize();
		line.positionCount = precision;
		Vector3[] positions = new Vector3[100];
		float yPos = 0;
		float step = screenSize.y * 2 / 100;
		for (int i = 0; i < positions.Length; i++)
		{
			positions[i].x = 0;
			positions[i].y = yPos - screenSize.y;
			yPos += step;
		}
		
		line.SetPositions(positions);
		GenerateCollider();
	}
		
	public void GenerateCollider()
	{
		List<Vector2> edges = new List<Vector2>();
		
		for (int i = 0; i < line.positionCount; i++)
		{
			Vector3 point = line.GetPosition(i);
			edges.Add(new Vector2(point.x, point.y));
		}
		
		edgeCollider.SetPoints(edges);
	}

	

	public void Smooth()
	{
		BezierCurve[] curves = new BezierCurve[line.positionCount - 1];
		for (int i = 0; i < curves.Length; i++)
		{
			curves[i] = new BezierCurve();
		}

		for (int i = 0; i < curves.Length; i++)
		{
			Vector2 position = line.GetPosition(i);
			Vector2 lastPosition = i == 0 ? line.GetPosition(0) : line.GetPosition(i - 1);
			Vector2 nextPosition = line.GetPosition(i + 1);

			Vector2 lastDirection = (position - lastPosition).normalized;
			Vector2 nextDirection = (nextPosition - position).normalized;

			Vector2 startTangent = (lastDirection + nextDirection) * SmoothingLength;
			Vector2 endTangent = (nextDirection + lastDirection) * -1 * SmoothingLength;


			curves[i]._points[0] = position; // Start Position (P0)
			curves[i]._points[1] = position + startTangent; // Start Tangent (P1)
			curves[i]._points[2] = nextPosition + endTangent; // End Tangent (P2)
			curves[i]._points[3] = nextPosition; // End Position (P3)
		}

		// Apply look-ahead for first curve and retroactively apply the end tangent
		{
			Vector2 nextDirection = (curves[1].EndPosition - curves[1].StartPosition).normalized;
			Vector2 lastDirection = (curves[0].EndPosition - curves[0].StartPosition).normalized;

			curves[0]._points[2] = curves[0]._points[3] +
				(nextDirection + lastDirection) * -1 * SmoothingLength;
		}

		line.positionCount = curves.Length * SmoothingSections;
		int index = 0;
		for (int i = 0; i < curves.Length; i++)
		{
			Vector2[] segments = curves[i].GetSegments(SmoothingSections);
			for (int j = 0; j < segments.Length; j++)
			{
				line.SetPosition(index, segments[j]);
				index++;
			}
		}
	}
}