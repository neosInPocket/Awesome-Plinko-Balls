using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMusic : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	
	private void Start()
	{
		PlayerSaves.LoadSaves();
		audioSource.volume = PlayerSaves.volume;
		if (PlayerSaves.isVolumeEnabled == 0)
		{
			audioSource.enabled = false;
		}
	}
}
