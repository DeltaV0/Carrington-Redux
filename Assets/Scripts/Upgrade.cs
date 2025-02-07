using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
	public Transform upgrade;

	public bool hovering;

	public int ID;
    // Start is called before the first frame update
    void Start()
    {
		if ((PlayerPrefs.GetInt ("BattleWin") == 0)) {
			for (int i = 0; i < UpgradeSpawner.cardsremaining.Count; i++) {
				if (UpgradeSpawner.cardsremaining [i] != null) {
					UpgradeSpawner.cardsremaining [i].SetActive (false);
				}
			}
			for (int i = 0; i < 4; i++) {
				UpgradeSpawner.gameobjects [i].SetActive (false);
			}
			UpgradeSpawner.gameobjects2 [0].SetActive (true);
			UpgradeSpawner.gameobjects2 [1].SetActive (true);
			UpgradeSpawner.gameobjects2 [2].SetActive (true);
			PlayerPrefs.Save ();
		}
    }

    // Update is called once per frame
    void Update()
    {
		if(hovering){
			upgrade.localScale = new Vector3(50f, 50f, 1f);
			if(ID == 0){
				UpgradeSpawner.textTitle.text = "Repeater Mk " + (PlayerPrefs.GetInt("RepeaterLvl") + 1);
				UpgradeSpawner.textDesc.text = "Increases damage output of the Refractor \n " + (int)(100 * Mathf.Pow(1.1f , PlayerPrefs.GetInt("RepeaterLvl"))) + " Average Damage -> " + (int)(100 * Mathf.Pow(1.1f , (PlayerPrefs.GetInt("RepeaterLvl") + 1))) + " Average Damage";
			} else if(ID == 1){
				UpgradeSpawner.textTitle.text = "Refractor Mk " + (PlayerPrefs.GetInt("RefractorLvl") + 1);
				UpgradeSpawner.textDesc.text = "Increases damage output of the Refractor  \n " + (int)(90 * Mathf.Pow(1.1f , PlayerPrefs.GetInt("RefractorLvl"))) + " Average Damage -> " + (int)(90 * Mathf.Pow(1.1f , (PlayerPrefs.GetInt("RefractorLvl") + 1))) + " Average Damage";
			} else if(ID == 2){
				UpgradeSpawner.textTitle.text = "Shield Generator Mk " + (PlayerPrefs.GetInt("ShieldLvl") + 1);
				UpgradeSpawner.textDesc.text = "Increases Shield's blocking capabilities \n " + (10 *  (PlayerPrefs.GetInt("ShieldLvl") + 1)) + " Blocked Damage -> " + (10 * (PlayerPrefs.GetInt("ShieldLvl") + 2)) + " Blocked Damage";
			} else if(ID == 3){
				UpgradeSpawner.textTitle.text = "Drives Mk " + (PlayerPrefs.GetInt("DrivesLvl") + 1);
				UpgradeSpawner.textDesc.text = "Increases Drives' avoidance capabilities \n " + (10 *  (PlayerPrefs.GetInt("DrivesLvl") + 1)) + " Dodge Chance -> " + (10 * (PlayerPrefs.GetInt("DrivesLvl") + 2)) + " Dodge Chance";
			} else if(ID == 4){
				UpgradeSpawner.textTitle.text = "Light Cannon Mk " + (PlayerPrefs.GetInt("RepeaterLvl") + 1);
				UpgradeSpawner.textDesc.text = "Increases damage output of the Refractor \n " + (int)(100 * Mathf.Pow(1.1f , PlayerPrefs.GetInt("RepeaterLvl"))) + " Average Damage -> " + (int)(100 * Mathf.Pow(1.1f , (PlayerPrefs.GetInt("RepeaterLvl") + 1))) + " Average Damage";
			}
		} else {
			upgrade.localScale = new Vector3(40f, 40f, 1f);
			//UpgradeSpawner.textTitle.text = "";
			//UpgradeSpawner.textDesc.text = "";
		}
    }

	private void OnMouseDown(){
		PlayerPrefs.SetInt("BattleWin", 0);
		if(ID == 0){
			PlayerPrefs.SetInt("RepeaterLvl", PlayerPrefs.GetInt("RepeaterLvl") + 1);
		} else if(ID == 1){
			PlayerPrefs.SetInt("RefractorLvl", PlayerPrefs.GetInt("RefractorLvl") + 1);
		} else if(ID == 2){
			PlayerPrefs.SetInt("ShieldLvl", PlayerPrefs.GetInt("ShieldLvl") + 1);
		} else if(ID == 3){
			PlayerPrefs.SetInt("DrivesLvl", PlayerPrefs.GetInt("DrivesLvl") + 1);
		} else if(ID == 4){

			if(PlayerPrefs.GetInt("LCUnlocked?") == 0){
				PlayerPrefs.SetInt("LCUnlocked?", 1);
			} else {
				PlayerPrefs.SetInt("RepeaterLvl", PlayerPrefs.GetInt("RepeaterLvl") + 1);
			}
		}
		for(int i = 0; i < UpgradeSpawner.cardsremaining.Count; i++){
			if(UpgradeSpawner.cardsremaining[i] != null){
			UpgradeSpawner.cardsremaining[i].SetActive(false);
			}
		}
		for(int i = 0; i < 4; i++){
			UpgradeSpawner.gameobjects[i].SetActive(false);
		}
		UpgradeSpawner.gameobjects2[0].SetActive(true);
		UpgradeSpawner.gameobjects2[1].SetActive(true);
		UpgradeSpawner.gameobjects2[2].SetActive(true);
		PlayerPrefs.Save();
		//gameObject.SetActive (false);
	}

	private void OnMouseOver () {
		hovering = true;
	}
	private void OnMouseExit () {
		hovering = false;
	}
}
