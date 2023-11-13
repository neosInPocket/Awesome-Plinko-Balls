using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TutWindow : MonoBehaviour
{
	[SerializeField] private TMP_Text characterDialogText;
	[SerializeField] private Routine routine;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		
		Touch.onFingerDown += OnFingerDown1;
	}
	
	private void OnFingerDown1(Finger finger)
	{
		Touch.onFingerDown -= OnFingerDown1;
		Touch.onFingerDown += OnFingerDown2;
		characterDialogText.text = "Don't let your ball touch the red spikes";
	}
	
	private void OnFingerDown2(Finger finger)
	{
		Touch.onFingerDown -= OnFingerDown2;
		Touch.onFingerDown += OnFingerDown3;
		characterDialogText.text = "To do this, control the taut rope, guiding the constantly moving ball";
	}
	
	private void OnFingerDown3(Finger finger)
	{
		Touch.onFingerDown -= OnFingerDown3;
		Touch.onFingerDown += OnFingerDown4;
		characterDialogText.text = "Also, don't overdo the rope bending as this may cause you to touch the edges of the screen and you will take damage";
	}
	
	private void OnFingerDown4(Finger finger)
	{
		Touch.onFingerDown -= OnFingerDown4;
		Touch.onFingerDown += OnFingerDown5;
		characterDialogText.text = "Collect coins, complete levels and receive rewards for which you can buy various upgrades in shop";
	}
	
	private void OnFingerDown5(Finger finger)
	{
		Touch.onFingerDown -= OnFingerDown5;
		Touch.onFingerDown += OnFingerDown6;
		characterDialogText.text = "Good luck!";
	}
	
	private void OnFingerDown6(Finger finger)
	{
		Touch.onFingerDown -= OnFingerDown6;
		routine.PlayCount();
		gameObject.SetActive(false);
	}
}
