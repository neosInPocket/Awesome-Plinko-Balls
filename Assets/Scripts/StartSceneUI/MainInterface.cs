using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainInterface : MonoBehaviour
{
	private void Awake()
	{
		//PlayerSaves.ClearSaves();
		PlayerSaves.LoadSaves();
	}
	
	public void PlayGame()
	{
		SceneManager.LoadScene("RopeGameScene");
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
}
