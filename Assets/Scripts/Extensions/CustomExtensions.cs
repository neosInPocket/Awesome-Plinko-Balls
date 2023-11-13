using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtensions
{
	public static Vector2 GetScreenWorldSize()
	{
		return ScreenVector2ToWorld(new Vector2(Screen.width, Screen.height));
	}
	
	public static Vector2 ScreenVector2ToWorld(Vector2 screenVector)
	{
		return Camera.main.ScreenToWorldPoint(screenVector);
	}
}
