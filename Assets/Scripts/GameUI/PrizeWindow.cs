using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrizeWindow : MonoBehaviour
{
	[SerializeField] private TMP_Text reward;
	[SerializeField] private TMP_Text prizeResultText;
	[SerializeField] private Routine routine;
	
	public void UpdateWindow(string coins, string prizeResult)
	{
		reward.text = $"+{coins}";
		prizeResultText.text = prizeResult;
	}
	
	public void HideWindowAndPlay()
	{
		routine.PlayGame();
		gameObject.SetActive(false);
	}
	
	public void Menu()
	{
		SceneManager.LoadScene("StartScene");
	}
}
