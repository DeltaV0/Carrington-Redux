using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
		if(PlayerPrefs.GetInt("Day") == 0) {
			buttons[0].SetActive(true);
			buttons[1].SetActive(false);
		} else {
			buttons[1].SetActive(true);
			buttons[0].SetActive(false);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Wipe(){
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}
	
	public void Continue(){
		SceneManager.LoadSceneAsync("SampleScene");
	}

	public void Continue2(){
		SceneManager.LoadSceneAsync("MapMenu");
	}
	
	public void Quit(){
		Application.Quit ();
	}

}
