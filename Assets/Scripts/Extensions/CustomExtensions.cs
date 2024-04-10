using UnityEngine;

public static class CustomExtensions
{
	public static Vector2 sizeOfTheScreen;

	static CustomExtensions()
	{
		sizeOfTheScreen = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
	}

	public static Vector2 ScreenPointFromWorldPoint(Vector2 screen)
	{
		screen.x /= Screen.width * sizeOfTheScreen.x;
		screen.y /= Screen.height * sizeOfTheScreen.y;
		screen += (Vector2)Camera.main.transform.position;
		return screen;
	}
}
