using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopContainerController : MonoBehaviour
{
	[SerializeField] private TMP_Text coinsCurrentCollectedText;
	[SerializeField] private List<Image> pointsForGravityUpgrade;
	[SerializeField] private List<Image> pointsForHealthUpgrade;
	[SerializeField] private Button healthUpgradeToggle;
	[SerializeField] private Button gravityUpgradeToggle;
	[SerializeField] private Button gravitySetButton;
	[SerializeField] private Button lifesSetButton;
	[SerializeField] private GameObject containerForLifes;
	[SerializeField] private GameObject containerForGravity;
	[SerializeField] private CoinsCapturer coinsCapturer;

	public void SetDefaultStoreSettings()
	{
		coinsCurrentCollectedText.text = PlayerSaves.coinsCollected.ToString();

		coinsCapturer.RestartCoins();
		SetButtonsValues();
		SetPointsValues();
	}

	public void SetButtonsValues()
	{
		if (PlayerSaves.coinsCollected - 50 < 0 || PlayerSaves.gravityUpdate == 3)
		{
			gravityUpgradeToggle.interactable = false;
		}

		if (PlayerSaves.coinsCollected - 100 < 0 || PlayerSaves.lifesCounUpgrade == 3)
		{
			healthUpgradeToggle.interactable = false;
		}
	}

	public void SetPointsValues()
	{
		coinsCapturer.RestartCoins();
		SetPointsForGravity();
		SetPointsFotHealth();
	}

	public virtual void SetPointsFotHealth()
	{
		for (int i = 0; i < PlayerSaves.lifesCounUpgrade; i++)
		{
			if (pointsForHealthUpgrade[i].gameObject.activeSelf) continue;
			pointsForHealthUpgrade[i].gameObject.SetActive(true);
		}
	}

	public virtual void SetPointsForGravity()
	{
		for (int i = 0; i < PlayerSaves.gravityUpdate; i++)
		{
			if (pointsForGravityUpgrade[i].gameObject.activeSelf) continue;
			pointsForGravityUpgrade[i].gameObject.SetActive(true);
		}
	}

	public void PurchaseLifesOne()
	{
		PlayerSaves.lifesCounUpgrade++;
		PlayerSaves.coinsCollected -= 100;

		PlayerSaves.SaveCurrentParameters();
		SetButtonsValues();
		SetPointsFotHealth();
		coinsCapturer.RestartCoins();
		coinsCurrentCollectedText.text = PlayerSaves.coinsCollected.ToString();
	}

	public void PurchaseGravityOne()
	{
		PlayerSaves.gravityUpdate++;
		PlayerSaves.coinsCollected -= 50;

		PlayerSaves.SaveCurrentParameters();
		SetPointsForGravity();
		SetButtonsValues();
		coinsCapturer.RestartCoins();
		coinsCurrentCollectedText.text = PlayerSaves.coinsCollected.ToString();
	}

	private void Start()
	{
		SetDefaultStoreSettings();
	}

	public void SetGravityUpgradeActive()
	{
		gravitySetButton.interactable = false;
		lifesSetButton.interactable = true;
		containerForGravity.gameObject.SetActive(true);
		containerForLifes.gameObject.SetActive(false);
	}

	public void SetLifesUpgradeActive()
	{
		lifesSetButton.interactable = false;
		gravitySetButton.interactable = true;
		containerForGravity.gameObject.SetActive(false);
		containerForLifes.gameObject.SetActive(true);
	}
}
