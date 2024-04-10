
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressStatus : MonoBehaviour
{
	[SerializeField] private Image fillProgress;
	[SerializeField] private List<Image> lifes;
	[SerializeField] private TMP_Text levelNumber;

	public void StartProgressReaction()
	{
		fillProgress.fillAmount = 0;
		RestartHealthStatus(PlayerSaves.lifesCounUpgrade);
		levelNumber.text = "LEVEL " + PlayerSaves.levelsPassed;
	}

	public void RestartHealthStatus(int health)
	{
		int pointer = 0;

		foreach (var lifeImage in lifes)
		{
			lifeImage.gameObject.SetActive(false);
		}

		foreach (var lifeImage in lifes)
		{
			if (pointer == health) return;
			lifeImage.gameObject.SetActive(true);
			pointer++;
		}
	}

	public void RestartProgressCurrent(float progress)
	{
		fillProgress.fillAmount = progress;
	}
}
