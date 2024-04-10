using UnityEngine;
using UnityEngine.SceneManagement;

public class MainInterface : MonoBehaviour
{
	public void LoadNextStartScene()
	{
		SceneManager.LoadScene("NextStartScene");
	}
}
