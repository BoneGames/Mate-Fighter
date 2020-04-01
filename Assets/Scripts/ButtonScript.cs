using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

	public void ToFightScene()
	{
		SceneManager.LoadScene(1);
	}
	public void Quit()
	{
		Application.Quit();
		Debug.Log("You Quit");
	}
	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
