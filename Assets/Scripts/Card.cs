using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
	public bool played;

	public int handInd;

	public int ID;
	public int PwrReq;
	public int SteamReq;

	public Vector3 origpos;

	public bool hovering;

	public Animator anim;

	public SpriteRenderer sprite;

	//public static int Shield;
	//private GameManager gm;

	//THIS NEEDS TO BE FINISHED!!!

    // Start is called before the first frame update
	void OnEnable()
    {
		sprite = gameObject.GetComponent<SpriteRenderer>();
		if(ID == 0){
			if(PlayerPrefs.GetInt("EquippedWeapon") == 0){
			sprite.sprite = GameManager.This.sprites[0];
		} else {
			sprite.sprite = GameManager.This.sprites[1];
		}
		}
		origpos = transform.position;
		hovering = false;
		anim = transform.parent.gameObject.GetComponent<Animator> ();
		anim.SetBool("Selected", false);
		anim.ResetTrigger("Discard");
		anim.ResetTrigger("Play");
		//Shield = 0;
	//	gm = FindObjectOfType<GameManager>();
    }

	void Draw(){
		GameManager.DrawCard ();
	}

    // Update is called once per frame
    void Update()
    {
		if(GameManager.turn % 2 == 1 && !played){
			played = true;
			GameManager.avalCardSlots2 [handInd] = true;
			GameManager.discard.Add (this);
			anim.SetTrigger("Discard");
			Invoke("Deactivate", 1f);
		}
		if(hovering){
			anim.SetBool("Selected", true);
			if(PwrReq <= 0){
				GameManager.This.Indicators[1].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(PwrReq) * 25.5f);
				GameManager.This.Indicators[1].pivot = new Vector2(0, 0.5f);
			} else {
				GameManager.This.Indicators[1].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(PwrReq) * 25.5f);
				GameManager.This.Indicators[1].pivot = new Vector2(1, 0.5f);
			}
			if(SteamReq <= 0){
				GameManager.This.Indicators[0].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(SteamReq) * 25.5f);
				GameManager.This.Indicators[0].pivot = new Vector2(0, 0.5f);
			} else {
				GameManager.This.Indicators[0].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(SteamReq) * 25.5f);
				GameManager.This.Indicators[0].pivot = new Vector2(1, 0.5f);
			}
			GameManager.This.Indicators[0].anchoredPosition = new Vector2(0, 0);
			GameManager.This.Indicators[1].anchoredPosition = new Vector2(0, 0);

			switch(ID)
			{
			case 0:
				if(PlayerPrefs.GetInt("EquippedWeapon") == 0){
				Buttons.text1.text = "Repeater \n Mk " + (PlayerPrefs.GetInt("RepeaterLvl"));
				Buttons.text2.text = "Deals damage over several shots. \n \n Average Damage: " + (int)(112.5 * Mathf.Pow(1.1f , (PlayerPrefs.GetInt("RepeaterLvl")))) + "\nShots: 10\nCosts 5 power";
				break;
				} else {
					Buttons.text1.text = "Light Cannon \n Mk " + (PlayerPrefs.GetInt("RepeaterLvl"));
					Buttons.text2.text = "Deals damage over several shots.\n Has 10% chance to misfire \n \n Average Damage: " + (int)(100 * Mathf.Pow(1.1f , (PlayerPrefs.GetInt("RepeaterLvl")))) + "\nShots: 5\nCosts 5 power";
					break;
				}
			case 1:
				Buttons.text1.text = "Shield System \n Mk " + (PlayerPrefs.GetInt("ShieldLvl"));
				Buttons.text2.text = "Blocks " + (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)) + " damage per shot per level\nCosts 5 power";
				break;
			case 2:
				Buttons.text1.text = "Generator";
				Buttons.text2.text = "Generates 10 Power from 10 Steam";
				break;
			case 3:
				Buttons.text1.text = "Boiler Overdrive";
				Buttons.text2.text = "Generates 10 Steam";
				break;
			case 4:
				Buttons.text1.text = "Draw 2";
				Buttons.text2.text = "Draw 2 random cards from your deck \n \n Costs 5 Steam";
				break;
			case 5:
				Buttons.text1.text = "Refractor \n Mk " + (PlayerPrefs.GetInt("RefractorLvl"));
				Buttons.text2.text = "Deals damage over a single shot. \n \n Average Damage: " + (int)(90 * Mathf.Pow(1.1f , (PlayerPrefs.GetInt("RefractorLvl")))) + "\nShots: 1\nCosts 5 power";
				break;
			case 6:
				Buttons.text1.text = "Drives System \n Mk " + (PlayerPrefs.GetInt("DrivesLvl"));
				Buttons.text2.text = "" + (10 * (PlayerPrefs.GetInt("DrivesLvl") + 1)) + "% chance to dodge per shot per level\nCosts 5 power";
				break;
			}
		} else {
			transform.position = origpos;
			anim.SetBool("Selected", false);
		}
    }

	private void OnMouseDown(){
		if(GameManager.turn % 2 == 0 && !played){
		CardEff ();
		}
	}

	private void OnMouseOver () {

		hovering = true;
		if(Input.GetMouseButton(1) && GameManager.turn % 2 == 0 && !played){
			played = true;
			GameManager.avalCardSlots2 [handInd] = true;
			GameManager.discard.Add (this);
			Player.steam += 1;
			anim.SetTrigger("Discard");
			Invoke("Deactivate", 1f);
		}
	}
	private void OnMouseExit () {

		hovering = false;
	}

	void CardEff(){
		if(Player.steam - SteamReq >= 0 && Player.steam - SteamReq <= 20 && Player.power - PwrReq >= 0 && Player.power - PwrReq <= 20){
		Player.steam -= SteamReq;
		Player.power -= PwrReq;

		if(ID == 0){
				if(Player.syshealth[0] > 0){
					if(PlayerPrefs.GetInt("EquippedWeapon") == 1){
					StartCoroutine(Light_Cannon());
					} else {
						StartCoroutine(Repeater());
					}
				} else {
					Player.steam += SteamReq;
					Player.power += PwrReq;
				}
			} else if(ID == 4){
				Invoke("Draw", 0f);
				Invoke("Draw", 0.5f);
				//Invoke("Draw", 1f);
			} else if(ID == 1){
				if(Player.syshealth[2] > 0){
				Player.Shield++;
				if(Player.Shield == 1){
					Player.Shieldobj.SetActive(true);
				}
				played = true;
				GameManager.avalCardSlots2 [handInd] = true;
				GameManager.discard.Add (this);
					anim.SetTrigger("Play");
					Invoke("Deactivate", 1f);
				} else {
					Player.steam += SteamReq;
					Player.power += PwrReq;
				}
			} else if(ID == 5){
				if(Player.syshealth[1] > 0){
				StartCoroutine(Refractor());
				} else {
					Player.steam += SteamReq;
					Player.power += PwrReq;
				}
			} else if (ID == 6){
				if(Player.syshealth[3] > 0){
				Player.Speed++;
				Player.As[2].Play();
				played = true;
				GameManager.avalCardSlots2 [handInd] = true;
				GameManager.discard.Add (this);
				anim.SetTrigger("Play");
				Invoke("Deactivate", 1f);
				} else {
					Player.steam += SteamReq;
					Player.power += PwrReq;
				}
			}
			if(ID != 0 && ID != 5 && ID != 1 && ID != 6){
			anim.SetTrigger("Play");
			played = true;
			GameManager.avalCardSlots2 [handInd] = true;
			GameManager.discard.Add (this);
			Invoke("Deactivate", 1f);
			}
		}
	}

	void Deactivate(){
		transform.parent.gameObject.SetActive (false);
	}

	IEnumerator Light_Cannon(){
		anim.SetTrigger("Play");
		played = true;
		GameManager.avalCardSlots2 [handInd] = true;
		GameManager.discard.Add (this);
		for(int i = 0; i < 5; i++){
			
			if(Random.Range(0, 100) > (Enemy.Speed * (10 * (Static.DrivesLvl + 1)))){
				if(Random.Range(0, 101) >= 10){
					Player.ps[2].Play();
					Player.As[3].pitch = Random.Range(0.9f, 1.1f);
					Player.As[3].Play();
					int damage = (int)((float)Random.Range(10, 35) * Mathf.Pow(1.1f , (PlayerPrefs.GetInt("RepeaterLvl"))));
					Player.health -= (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) > 0 ? (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) : 0;
					GameManager.This.SpawnDmg("-" + ((damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) > 0 ? (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) : 0), Player.me.damagespawnpos);
					
					Enemy.syshealth[Player.selectedSystem] -= (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) > 0 ? (damage - (Enemy.Shield * (10 * (Static.ShieldLvl) + 1))) : 0;
					if(Enemy.syshealth[Player.selectedSystem] < 0){
						Enemy.syshealth[Player.selectedSystem] = 0;
					}
					Enemy.lr[2].SetPosition (0, Player.pos.position);
					Enemy.lr[2].SetPosition (1, Player.hitpos[Random.Range(0, 4)].position);
				} else {
					//misfire
				}
			} else {
				Player.ps[2].Play();
				Player.As[3].pitch = Random.Range(0.95f, 1.05f);
				Player.As[3].Play();
				GameManager.This.SpawnDmg("Miss", Player.me.damagespawnpos);
				
				Enemy.lr[2].SetPosition (0, Player.pos.position);
				Enemy.lr[2].SetPosition (1, Player.misspos[Random.Range(0, 3)].position);
			}
			Enemy.anim.SetTrigger("LightCannon");
			checksystems();
			for(int a = 0; a < 1; a++){
				yield return new WaitForSeconds(0.167f);
			}
		}
		for(int a = 0; a < 1; a++){
			yield return new WaitForSeconds(0.167f);
		}
		Enemy.lr[2].SetPosition (0, Player.pos.position);
		Enemy.lr[2].SetPosition (1, Player.pos.position);

		transform.parent.gameObject.SetActive (false);
	}

	IEnumerator Repeater(){
		anim.SetTrigger("Play");
		played = true;
		GameManager.avalCardSlots2 [handInd] = true;
		GameManager.discard.Add (this);
		for(int i = 0; i < 10; i++){
			Player.As[0].pitch = Random.Range(0.95f, 1.05f);
			Player.ps[0].Play();
			Player.As[0].Play();
			if(Random.Range(0, 100) > (Enemy.Speed * (10 * (Static.DrivesLvl + 1)))){
				int damage = (int)((float)Random.Range(5, 15) * Mathf.Pow(1.1f , (PlayerPrefs.GetInt("RepeaterLvl"))));
				Enemy.health -= (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) > 0 ? (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) : 0;
				GameManager.This.SpawnDmg("-" + ((damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) > 0 ? (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) : 0), Player.me.damagespawnpos);
				Enemy.syshealth[Player.selectedSystem] -= (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) > 0 ? (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) : 0;
				if(Enemy.syshealth[Player.selectedSystem] < 0){
					Enemy.syshealth[Player.selectedSystem] = 0;
				}
				checksystems();
				Player.lr[0].SetPosition (0, Player.pos.position);
				Player.lr[0].SetPosition (1, Player.hitpos[Random.Range(0, 6)].position);
			} else {
				GameManager.This.SpawnDmg("Miss", Player.me.damagespawnpos);

				Player.lr[0].SetPosition (0, Player.pos.position);
				Player.lr[0].SetPosition (1, Player.misspos[Random.Range(0, 3)].position);
			}
			Player.anim.SetTrigger("Repeater");
			for(int a = 0; a < 1; a++){
				yield return new WaitForSeconds(0.167f);
			}
		}
		for(int a = 0; a < 1; a++){
			yield return new WaitForSeconds(0.167f);
		}
		Player.lr[0].SetPosition (0, Player.pos.position);
		Player.lr[0].SetPosition (1, Player.pos.position);


		transform.parent.gameObject.SetActive (false);
	}

	IEnumerator Refractor(){
		anim.SetTrigger("Play");
		played = true;
		GameManager.avalCardSlots2 [handInd] = true;
		GameManager.discard.Add (this);
		bool ooo = false;
		int c = Random.Range(0, 6);
		int c2 = Random.Range(0, 4);
		//for(int i = 0; i < 1; i++){
			Player.As[1].pitch = Random.Range(0.95f, 1.05f);
			Player.ps[1].Play();
			Player.As[1].Play();
		if(Random.Range(0, 100) > (Enemy.Speed * (10 * (Static.DrivesLvl + 1)))){
			int damage = (int)((float)Random.Range(60, 120) * Mathf.Pow(1.1f , (PlayerPrefs.GetInt("RefractorLvl"))));
			Enemy.health -= (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) > 0 ? (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) : 0;
			GameManager.This.SpawnDmg("-" + ((damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) > 0 ? (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) : 0), Player.me.damagespawnpos);
			Enemy.syshealth[Player.selectedSystem] -= (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) > 0 ? (damage - (Enemy.Shield * (10 * (Static.ShieldLvl + 1)))) : 0;
			if(Enemy.syshealth[Player.selectedSystem] < 0){
				Enemy.syshealth[Player.selectedSystem] = 0;
			}
			checksystems();
			Player.anim.SetTrigger("Refractor");
		for(int b = 0; b < 40; b++){

				Player.lr[1].SetPosition (0, Player.pos.position);
				Player.lr[1].SetPosition (1, Player.hitpos[c].position);
			yield return new WaitForSeconds(0.1f);	

			}
		} else {
			//if(!ooo){
				GameManager.This.SpawnDmg("Miss", Player.me.damagespawnpos);
			//}
			//ooo = true;
			Player.anim.SetTrigger("Refractor");
			for(int b = 0; b < 40; b++){
				Player.lr[1].SetPosition (0, Player.pos.position);
				Player.lr[1].SetPosition (1, Player.misspos[c2].position);
				yield return new WaitForSeconds(0.1f);	
				
			}
		}
		Player.lr[1].SetPosition (0, Player.pos.position);
		Player.lr[1].SetPosition (1, Player.pos.position);


		transform.parent.gameObject.SetActive (false);
	}

	void checksystems(){
		//deactivate enemy systems if destroyed
		if(Enemy.syshealth[2] <= 0){
			Enemy.Shieldobj.SetActive(false);
			Enemy.Shield = 0;
		} else if(Enemy.syshealth[3] <= 0){
			Enemy.Speed = 0;
		}
	}
}
