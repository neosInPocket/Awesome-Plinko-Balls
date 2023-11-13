using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Routine : MonoBehaviour
{
	[SerializeField] private ObstacleSpawner obstacleSpawner;
	[SerializeField] private PlayerController player;
	[SerializeField] private LevelProgressStatus levelProgressStatus;
	[SerializeField] private PrizeWindow prizeWindow;
	[SerializeField] private CountWindow countWindow;
	[SerializeField] private TutWindow tutWindow;
	
	private int levelPoints;
	private int maxPoints;
	private int reward;
	private bool isEndGame;
	
	
	private void Start()
	{
		PlayerSaves.ClearSaves();
		PlayGame();
	}
	
	public void PlayGame()
	{
		PlayerSaves.LoadSaves();
		isEndGame = false;
		obstacleSpawner.Init();
		player.Initialize();
		player.Disable();
		levelProgressStatus.ReStart();
		
		maxPoints = LevelDataGenerator.GetLevelPoints();
		levelPoints = 0;
		reward = LevelDataGenerator.GetLevelReward();
		
		if (PlayerSaves.tutRequested < 0.5f)
		{
			countWindow.gameObject.SetActive(true);
		}
		else
		{
			PlayerSaves.tutRequested = 0f;
			PlayerSaves.SaveSaves();
			tutWindow.gameObject.SetActive(true);
		}
	}
	
	public void PlayRoutine()
	{
		player.GoPlay();
	}
	
	public void PlayCount()
	{
		countWindow.gameObject.SetActive(true);
	}
	
	public void PlayerCoin()
	{
		if (isEndGame) return;
		
		levelPoints += 2;
		if (levelPoints >= maxPoints)
		{
			isEndGame = true;
			prizeWindow.gameObject.SetActive(true);
			prizeWindow.UpdateWindow(reward.ToString(), "YOU WIN");
			levelPoints = maxPoints;
			player.Disable();
			PlayerSaves.coins += reward;
			PlayerSaves.currentProgress++;
			PlayerSaves.SaveSaves();
		}
		
		levelProgressStatus.RestartProgressValue((float)levelPoints / (float)maxPoints);
	}
	
	public void PlayerDamage(int lifesLeft)
	{
		if (isEndGame) return;
		
		if (lifesLeft == 0)
		{
			isEndGame = true;
			prizeWindow.gameObject.SetActive(true);
			prizeWindow.UpdateWindow("0", "YOU LOSE");
			player.Disable();
		}
		
		levelProgressStatus.RefreshLifes(lifesLeft);
	}
}
