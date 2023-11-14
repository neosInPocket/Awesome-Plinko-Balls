using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
	[SerializeField] private Slider musicVolumeSlider;
	[SerializeField] private AudioSource musicAudio;
	[SerializeField] private Toggle musicToggle;
	
	private void Start()
	{
		PlayerSaves.LoadSaves();
		
		musicAudio.volume = PlayerSaves.volume;
		if (musicVolumeSlider != null)
		{
			musicVolumeSlider.value = PlayerSaves.volume;
		}
		
		if (PlayerSaves.isVolumeEnabled == 0)
		{
			musicAudio.enabled = false;
			if (musicToggle != null)
			{
				musicToggle.isOn = false;
			}
		}
	}
	
	public void SetMusicVolumeSlider(float musicVolume)
	{
		musicAudio.volume = musicVolume;
	}
	
	public void Save()
	{
		PlayerSaves.volume = musicAudio.volume;
		PlayerSaves.SaveSaves();
	}
	
	public void ToggleVolume(bool value)
	{
		musicAudio.enabled = value;
		if (value)
		{
			PlayerSaves.isVolumeEnabled = 1;
			PlayerSaves.SaveSaves();
		}
		else
		{
			PlayerSaves.isVolumeEnabled = 0;
			PlayerSaves.SaveSaves();
		}
	}
}
