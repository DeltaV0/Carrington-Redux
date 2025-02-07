using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
	public static TMP_Text text1;
	public static TMP_Text text2;

	public TMP_Text text11;
	public TMP_Text text21;

	public static TMP_Text text3;
	public static TMP_Text text4;
	
	public TMP_Text text31;
	public TMP_Text text41;

	public AudioSource aud1;
	public AudioSource aud2;

    // Start is called before the first frame update
    void Start()
    {
		text1 = text11;
		text2 = text21;

		text3 = text31;
		text4 = text41;
    }

    // Update is called once per frame
    void Update()
    {
		if(GameManager.deck2.Count < 1){
			GameManager.Shuffle ();
		}
    }

	public void Wipe(){
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}

	public void Continue(){
		SceneManager.LoadSceneAsync("SampleScene");
	}

	public void Quit(){
		Application.Quit ();
	}

	public void MenuFromDeath(){
		//PlayerPrefs.DeleteAll();
		//PlayerPrefs.Save();
		SceneManager.LoadSceneAsync("Title");
	}

	public void SelectSubsystem(int system){
		aud1.Play();
		Player.selectedSystem = system;
	}


	public void RepairSubsystem(int system){

	if(Player.syshealth[system] < 150 && Player.power >= 1 && GameManager.turn % 2 == 0){
		aud2.Play();
		Player.power--;
		Player.syshealth[system] += Random.Range(50, 75);
		if(Player.syshealth[system] > 150){
			Player.syshealth[system] = 150;
		}
	}
	}

	public void NextTurn(){
		if(GameManager.turn % 2 == 0){

			GameManager.anim.SetTrigger("EnemyTurn");
			text1.text = "";
			text2.text = "";
		GameManager.turn++;
			if(Enemy.Shield > 0){
				Enemy.Shield--;
			}
			if(Enemy.Speed > 0){
				Enemy.Speed--;
			}
			if(Enemy.Shield == 0){
				Enemy.Shieldobj.SetActive(false);
			}
		//GameManager.Shuffle();
		

		if(Enemy.steam > 20) {
			Enemy.steam = 20;
		}


		if(Enemy.steam < 20){
			Enemy.steam += 2;
			} 
		if(Enemy.steam > 20) {
			Enemy.steam = 20;
			}

	}
	}
}
