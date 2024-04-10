using TMPro;
using UnityEngine;

public class CoinsCapturer : MonoBehaviour
{
	[SerializeField] private TMP_Text coinsCapturer;

	private void Start()
	{
		RestartCoins();
	}

	public void RestartCoins()
	{
		coinsCapturer.text = PlayerSaves.coinsCollected.ToString();
	}
}
