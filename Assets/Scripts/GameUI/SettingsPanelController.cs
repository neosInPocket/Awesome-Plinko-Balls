using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private Toggle musicToggleButton;
	private LoadMusic loadMusic;
	private AudioSource musicAudioVolume;
	private float prevValue;

	private void Start()
	{
		loadMusic = FindFirstObjectByType<LoadMusic>();
		musicAudioVolume = loadMusic.GetComponent<AudioSource>();

		musicAudioVolume.volume = PlayerSaves.simpleVolume;
		if (slider != null)
		{
			slider.value = PlayerSaves.simpleVolume;
		}

		if (PlayerSaves.volumeMusic == 0)
		{
			musicAudioVolume.enabled = false;
			if (musicToggleButton != null)
			{
				musicToggleButton.isOn = false;
			}
		}
	}

	public void SetMusicVolumeCurrent(float musicVolume)
	{
		musicAudioVolume.volume = musicVolume;
	}

	public void SaveVolumeCurrent()
	{
		PlayerSaves.simpleVolume = musicAudioVolume.volume == 1f ? 1 : 0;
		PlayerSaves.SaveCurrentParameters();
	}

	public void ToggleVolumeValues(bool value)
	{
		if (!value) prevValue = musicAudioVolume.volume;

		musicAudioVolume.volume = value ? prevValue : 0f;

		if (value)
		{
			PlayerSaves.volumeMusic = 1;
			PlayerSaves.SaveCurrentParameters();
		}
		else
		{
			PlayerSaves.volumeMusic = 0;
			PlayerSaves.SaveCurrentParameters();
		}
	}
}
