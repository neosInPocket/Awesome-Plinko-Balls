using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSaves
{
	public static int lifes { get; set; }
	public static int ballSpeed { get; set; }
	public static int currentProgress { get; set; }
	public static int coins { get; set; }
	public static float volume { get; set; }
	public static float tutRequested { get; set; }
	
	public static void ClearSaves()
	{
		lifes = 1;
		ballSpeed = 0;
		currentProgress = 1;
		volume = 1f;
		tutRequested = 1f;
		coins = 100;
		SaveSaves();
	} 
	
	public static void SaveSaves()
	{
		PlayerPrefs.SetInt("lifes", lifes);
		PlayerPrefs.SetInt("coins", coins);
		PlayerPrefs.SetInt("ballSpeed", ballSpeed);
		PlayerPrefs.SetInt("currentProgress", currentProgress);
		PlayerPrefs.SetFloat("volume", volume);
		PlayerPrefs.SetFloat("tutRequested", tutRequested);
	}
	
	public static void LoadSaves()
	{
		lifes = PlayerPrefs.GetInt("lifes", 1);
		coins = PlayerPrefs.GetInt("coins", 100);
		ballSpeed = PlayerPrefs.GetInt("ballSpeed", 0);
		currentProgress = PlayerPrefs.GetInt("currentProgress", 1);
		volume = PlayerPrefs.GetFloat("volume", 1f);
		tutRequested = PlayerPrefs.GetFloat("tutRequested", 1f);
	}
}
