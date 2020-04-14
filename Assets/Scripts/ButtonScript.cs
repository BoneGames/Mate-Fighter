using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public void LoadScene(int scene)
    {
        if(scene == -1)
        {
            Quit();
            return;
        }
        SceneManager.LoadScene(scene);
    }


	void Quit()
	{
		Debug.Log("You Quit");
		Application.Quit();
	}
	
}
