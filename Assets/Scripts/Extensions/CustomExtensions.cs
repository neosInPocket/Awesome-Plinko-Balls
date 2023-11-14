using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtensions
{
	public static Vector2 screenSize;
	
	static CustomExtensions()
	{
		screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
	}
		
	public static Vector2 ScreenVector2ToWorld(Vector2 screenVector)
	{
		return Camera.main.ScreenToWorldPoint(screenVector);
	}
}
