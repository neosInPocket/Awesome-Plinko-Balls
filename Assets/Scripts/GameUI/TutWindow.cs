using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TutWindow : MonoBehaviour
{
	[SerializeField] private TMP_Text dialog;
	[SerializeField] private Routine route;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void StartTutorialRoutine()
	{
		Touch.onFingerDown += RedSpikes;
	}

	private void RedSpikes(Finger finger)
	{
		Touch.onFingerDown -= RedSpikes;
		Touch.onFingerDown += RopeControl;
		dialog.text = "WELCOME TO NEW GAME! Don't let your ball touch the red spikes";
	}

	private void RopeControl(Finger finger)
	{
		Touch.onFingerDown -= RopeControl;
		Touch.onFingerDown += ScreenDamage;
		dialog.text = "ALL YOU NEED TO DO IS SIMPLY CONTROL the taut rope BY HOLDING THE SCREEN";
	}

	private void ScreenDamage(Finger finger)
	{
		Touch.onFingerDown -= ScreenDamage;
		Touch.onFingerDown += RecieveRewards;
		dialog.text = "BE CAREFUL AND DON'T OVERDO THE ROPE! IF YOUR BALL FLYES OFF THE SCREEN YOU WILL LOSE";
	}

	private void RecieveRewards(Finger finger)
	{
		Touch.onFingerDown -= RecieveRewards;
		Touch.onFingerDown += GoodLuck;
		dialog.text = "COMPLETE LEVELS COLLECTING A CERTAIN NUMBER OF COINS. EACH LEVEL HAS A REWARD FOR WHICH YOU CAN BUY VARIOUS IMPROVEMENTS IN THE STORE";
	}

	private void GoodLuck(Finger finger)
	{
		Touch.onFingerDown -= GoodLuck;
		Touch.onFingerDown += RoutineEnd;
		dialog.text = "Good luck!";
	}

	private void RoutineEnd(Finger finger)
	{
		Touch.onFingerDown -= RoutineEnd;
		route.PlayCountDownCurrent();
		gameObject.SetActive(false);
	}
}
