using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountWindow : MonoBehaviour
{
	[SerializeField] private Routine routine;

	public void PlayRoutine()
	{
		gameObject.SetActive(false);
		routine.StartLevelCurrent();
	}
}
