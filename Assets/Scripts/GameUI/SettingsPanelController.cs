using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
	[SerializeField] private Slider musicVolumeSlider;
	[SerializeField] private AudioSource musicAudio;
	
	private void Start()
	{
		PlayerSaves.LoadSaves();
		
		musicAudio.volume = PlayerSaves.volume;
		if (musicVolumeSlider != null)
		{
			musicVolumeSlider.value = PlayerSaves.volume;
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
}
