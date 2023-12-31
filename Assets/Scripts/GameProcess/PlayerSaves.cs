using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSaves
{
	public static int lifes { get; set; }
	public static int ballGravity { get; set; }
	public static int currentProgress { get; set; }
	public static int coins { get; set; }
	public static int isVolumeEnabled { get; set; }
	public static float volume { get; set; }
	public static float tutRequested { get; set; }
	
	
	public static void ClearSaves()
	{
		lifes = 1;
		ballGravity = 0;
		currentProgress = 1;
		volume = 1f;
		tutRequested = 1f;
		coins = 100;
		isVolumeEnabled = 1;
		SaveSaves();
	} 
	
	public static void SaveSaves()
	{
		PlayerPrefs.SetInt("lifes", lifes);
		PlayerPrefs.SetInt("coins", coins);
		PlayerPrefs.SetInt("ballGravity", ballGravity);
		PlayerPrefs.SetInt("currentProgress", currentProgress);
		PlayerPrefs.SetInt("isVolumeEnabled", isVolumeEnabled);
		PlayerPrefs.SetFloat("volume", volume);
		PlayerPrefs.SetFloat("tutRequested", tutRequested);
	}
	
	public static void LoadSaves()
	{
		lifes = PlayerPrefs.GetInt("lifes", 1);
		coins = PlayerPrefs.GetInt("coins", 100);
		ballGravity = PlayerPrefs.GetInt("ballGravity", 0);
		currentProgress = PlayerPrefs.GetInt("currentProgress", 1);
		isVolumeEnabled = PlayerPrefs.GetInt("isVolumeEnabled", 1);
		volume = PlayerPrefs.GetFloat("volume", 1f);
		tutRequested = PlayerPrefs.GetFloat("tutRequested", 1f);
	}
}
