
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressStatus : MonoBehaviour
{
	[SerializeField] private Image fillProgress;
	[SerializeField] private List<Image> lifes;
	[SerializeField] private TMP_Text levelNumber;
	
	public void ReStart()
	{
		fillProgress.fillAmount = 0;
		RefreshLifes(PlayerSaves.lifes);
		levelNumber.text = "LEVEL " + PlayerSaves.currentProgress;
	}	
	
	public void RefreshLifes(int lifesCount)
	{
		int pointer = 0; 
		
		foreach (var lifeImage in lifes)
		{
			lifeImage.gameObject.SetActive(false);
		}
		
		foreach (var lifeImage in lifes)
		{
			if (pointer == lifesCount) return;
			lifeImage.gameObject.SetActive(true);
			pointer++;
		}
	}
	
	public void RestartProgressValue(float progressValue)
	{
		fillProgress.fillAmount = progressValue;
	}
}
