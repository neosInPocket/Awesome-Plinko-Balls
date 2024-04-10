using System.Linq;
using UnityEngine;

public class LoadMusic : MonoBehaviour
{
	[SerializeField] private AudioSource musicCurrent;
	public float Volume
	{
		get => musicCurrent.volume;
		set => musicCurrent.volume = value;
	}

	private void Awake()
	{
		LoadMusic[] musicsFound = FindObjectsByType<LoadMusic>(sortMode: FindObjectsSortMode.None);
		var length = musicsFound.Length == 1;

		if (!length)
		{
			var foundOneMusic = musicsFound.FirstOrDefault(x => x.gameObject.scene.name != "DontDestroyOnLoad");
			Destroy(foundOneMusic.gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		musicCurrent.volume = PlayerSaves.simpleVolume;
		if (PlayerSaves.volumeMusic != 0)
		{
		}
		else
		{
			musicCurrent.enabled = true;
		}
	}
}
