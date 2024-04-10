using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public static class LevelDataGenerator
{
	public static int GetLevelReward()
	{
		var x = PlayerSaves.levelsPassed;
		return (int)(10 * Mathf.Log(x * x + 1) + 10);
	}

	public static int GetLevelPoints()
	{
		var x = PlayerSaves.levelsPassed;
		return (int)(10 * Mathf.Log(Mathf.Sqrt(x) + 1));
	}
}
