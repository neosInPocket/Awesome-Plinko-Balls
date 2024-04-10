
using UnityEngine;

public class Routine : MonoBehaviour
{
	[SerializeField] private ObstacleSpawner spawnerObstacles;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private LevelProgressStatus progressCurrentStatus;
	[SerializeField] private PrizeWindow gainPrizeScreen;
	[SerializeField] private CountWindow countDownCurrent;
	[SerializeField] private TutWindow tutorial;

	private int currentCoinsCollected;
	private int maxPointsNeeded;
	private int coinsGainPerLevel;
	private bool endGameStatus;

	private void Start()
	{
		LoadGameValues();
	}

	public void LoadGameValues()
	{
		endGameStatus = false;
		spawnerObstacles.Init();
		playerController.InitializePlayer();
		playerController.SetDisactiveState();
		progressCurrentStatus.StartProgressReaction();

		maxPointsNeeded = LevelDataGenerator.GetLevelPoints();
		currentCoinsCollected = 0;
		coinsGainPerLevel = LevelDataGenerator.GetLevelReward();

		if (PlayerSaves.tutNeed != 1)
		{
			countDownCurrent.gameObject.SetActive(true);
		}
		else
		{
			PlayerSaves.tutNeed = 0;
			PlayerSaves.SaveCurrentParameters();
			tutorial.gameObject.SetActive(true);
			tutorial.StartTutorialRoutine();
		}
	}

	public void StartLevelCurrent()
	{
		playerController.StartPlayer();
	}

	public void PlayCountDownCurrent()
	{
		countDownCurrent.gameObject.SetActive(true);
	}

	public void CoinGained()
	{
		if (endGameStatus) return;

		currentCoinsCollected += 2;
		if (currentCoinsCollected >= maxPointsNeeded)
		{
			endGameStatus = true;
			gainPrizeScreen.gameObject.SetActive(true);
			gainPrizeScreen.UpdateWindow(coinsGainPerLevel.ToString(), "LEVEL COMPLETED");
			currentCoinsCollected = maxPointsNeeded;
			playerController.SetDisactiveState();
			PlayerSaves.coinsCollected += coinsGainPerLevel;
			PlayerSaves.levelsPassed++;
			PlayerSaves.SaveCurrentParameters();
		}

		progressCurrentStatus.RestartProgressCurrent((float)currentCoinsCollected / (float)maxPointsNeeded);
	}

	public void DamagedPlayer(int health)
	{
		if (endGameStatus) return;

		if (health == 0)
		{
			endGameStatus = true;
			gainPrizeScreen.gameObject.SetActive(true);
			gainPrizeScreen.UpdateWindow("0", "LEVEL FAIL");
			playerController.SetDisactiveState();
		}

		progressCurrentStatus.RestartHealthStatus(health);
	}
}
