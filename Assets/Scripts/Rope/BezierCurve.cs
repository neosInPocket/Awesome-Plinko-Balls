using UnityEngine;

[System.Serializable]
public class BezierCurve
{
	public Vector2[] _points;

	public BezierCurve()
	{
		_points = new Vector2[4];
	}

	public BezierCurve(Vector2[] points)
	{
		this._points = points;
	}

	public Vector3 StartPosition => _points[0];

	public Vector3 EndPosition => _points[3];
	
	public Vector3 GetSegment(float Time)
	{
		Time = Mathf.Clamp01(Time);
		float time = 1 - Time;
		return (time * time * time * _points[0])
			+ (3 * time * time * Time * _points[1])
			+ (3 * time * Time * Time * _points[2])
			+ (Time * Time * Time * _points[3]);
	}

	public Vector2[] GetSegments(int subDivs)
	{
		Vector2[] segments = new Vector2[subDivs];

		float time;
		for (int i = 0; i < subDivs; i++)
		{
			time = (float)i / subDivs;
			segments[i] = GetSegment(time);
		}

		return segments;
	}
}