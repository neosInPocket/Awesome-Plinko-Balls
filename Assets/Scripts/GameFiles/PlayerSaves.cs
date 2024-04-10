using UnityEngine;

public class PlayerSaves : MonoBehaviour
{
	[SerializeField] private bool serializeNewSaves;
	[SerializeField] private PlayerSavesParameters defaultParameters;

	public static PlayerSavesParameters defaultStaticParameters;
	public static int lifesCounUpgrade;
	public static int gravityUpdate;
	public static int levelsPassed;
	public static int coinsCollected;
	public static int volumeMusic;
	public static int simpleVolume;
	public static int tutNeed;

	private void Awake()
	{
		defaultStaticParameters = defaultParameters;

		if (serializeNewSaves)
		{
			ClearSaves();
		}
		else
		{
			LoadCurrentParameters();
		}
	}

	public static void ClearSaves()
	{
		lifesCounUpgrade = defaultStaticParameters.lifesCounUpgrade;
		gravityUpdate = defaultStaticParameters.gravityUpdate;
		levelsPassed = defaultStaticParameters.levelsPassed;
		simpleVolume = defaultStaticParameters.simpleVolume;
		tutNeed = defaultStaticParameters.tutNeed;
		coinsCollected = defaultStaticParameters.coinsCollected;
		volumeMusic = defaultStaticParameters.volumeMusic;
		SaveCurrentParameters();
	}

	public static void SaveCurrentParameters()
	{
		PlayerPrefs.SetInt("lifesCountUpgrade", lifesCounUpgrade);
		PlayerPrefs.SetInt("coinsCollected", coinsCollected);
		PlayerPrefs.SetInt("gravityUpdate", gravityUpdate);
		PlayerPrefs.SetInt("levelsPassed", levelsPassed);
		PlayerPrefs.SetInt("volumeMusic", volumeMusic);
		PlayerPrefs.SetInt("simpleVolume", simpleVolume);
		PlayerPrefs.SetInt("tutNeed", tutNeed);
	}

	public static void LoadCurrentParameters()
	{
		lifesCounUpgrade = PlayerPrefs.GetInt("lifesCountUpgrade", 1);
		coinsCollected = PlayerPrefs.GetInt("coinsCollected", 100);
		gravityUpdate = PlayerPrefs.GetInt("gravityUpdate", 0);
		levelsPassed = PlayerPrefs.GetInt("levelsPassed", 1);
		volumeMusic = PlayerPrefs.GetInt("volumeMusic", 1);
		simpleVolume = PlayerPrefs.GetInt("simpleVolume", 1);
		tutNeed = PlayerPrefs.GetInt("tutNeed", 1);
	}
}
