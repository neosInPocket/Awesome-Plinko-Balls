using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopContainerController : MonoBehaviour
{
	[SerializeField] private TMP_Text coins;
	[SerializeField] private List<Image> gravityImages;
	[SerializeField] private List<Image> healthImages;
	[SerializeField] private Button healthButton;
	[SerializeField] private Button gravityButton;
	[SerializeField] private Button gravityChooseButton;
	[SerializeField] private Button lifesChooseButton;
	[SerializeField] private GameObject lifesContainer;
	[SerializeField] private GameObject gravityContainer;
	
	
	
	public void UpdateShopSettings()
	{
		PlayerSaves.LoadSaves();
		coins.text = PlayerSaves.coins.ToString();
		CheckButtons();
		CheckPoints();
	}
	
	public void CheckButtons()
	{
		if (PlayerSaves.coins - 50 < 0 || PlayerSaves.ballGravity == 3)
		{
			gravityButton.interactable = false;
		}
		
		if (PlayerSaves.coins - 100 < 0 || PlayerSaves.lifes == 3)
		{
			healthButton.interactable = false;
		}
	}
	
	public void CheckPoints()
	{
		CheckGravityPoints();
		CheckHealthPoints();
	}
	
	private void CheckHealthPoints()
	{
		for (int i = 0; i < PlayerSaves.lifes; i++)
		{
			if (healthImages[i].gameObject.activeSelf) continue;
			healthImages[i].gameObject.SetActive(true);
		}
	}
	
	private void CheckGravityPoints()
	{
		for (int i = 0; i < PlayerSaves.ballGravity; i++)
		{
			if (gravityImages[i].gameObject.activeSelf) continue;
			gravityImages[i].gameObject.SetActive(true);
		}
	}
	
	public void UpgradeLifes()
	{
		PlayerSaves.coins -= 100;
		PlayerSaves.lifes++;
		PlayerSaves.SaveSaves();
		CheckPoints();
		CheckHealthPoints();
		coins.text = PlayerSaves.coins.ToString();
	}
	
	public void UpgradeGravity()
	{
		PlayerSaves.coins -= 50;
		PlayerSaves.ballGravity++;
		PlayerSaves.SaveSaves();
		CheckGravityPoints();
		CheckButtons();
		coins.text = PlayerSaves.coins.ToString();
	}
	
	private void Start()
	{
		UpdateShopSettings();
	}
	
	public void ChooseGravity()
	{
		gravityChooseButton.interactable = false;
		lifesChooseButton.interactable = true;
		gravityContainer.gameObject.SetActive(true);
		lifesContainer.gameObject.SetActive(false);
	}
	
	public void ChooseLifes()
	{
		lifesChooseButton.interactable = false;
		gravityChooseButton.interactable = true;
		gravityContainer.gameObject.SetActive(false);
		lifesContainer.gameObject.SetActive(true);
	}
}
