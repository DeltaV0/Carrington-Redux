using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
	public Transform pos3;
	public GameObject[] spritemodel;

	public static int maxHealth;
	public static int health;

	public static int steam;
	public int power;

	public static int[] syshealth = new int[4];

	public static bool[] sysdead = new bool[4];

	public int selectedSystem;

	public static int Shield;

	public static int Speed;

	private bool done;

	private bool playing;

	public static Transform pos;
	public Transform pos2;
	
	public static Transform[] hitpos;
	
	public Transform[] hp;
	
	public Transform[] mp;

	public static LineRenderer[] lr;
	
	public static Animator anim;
	
	public LineRenderer[] lr2;
	
	public Animator anim2;
	
	public static ParticleSystem[] ps;
	
	public ParticleSystem[] ps2;
	
	public static AudioSource[] As;
	
	public AudioSource[] As2;

	public static GameObject Shieldobj;
	
	public GameObject shobj;

	private bool death;
	bool one;

	public static Enemy This;

	public Transform Trans;

	public Transform myloc;

	public GameObject[] gobj;
    // Start is called before the first frame update
    void Start()
    {

		This = this;
		Trans = transform;
		spritemodel[Random.Range(0, 1)].SetActive(true);
		one = false;
		death = false;
		syshealth[0] = 150;
		syshealth[1] = 150;
		syshealth[2] = 150;
		syshealth[3] = 150;
		Shieldobj = shobj;
		Shield = 0;
		Speed = 0;
		As = As2;
		ps = ps2;
		lr = lr2;
		anim = anim2;
		hitpos = hp;
		pos = pos2;
		if(PlayerPrefs.GetInt("Day") == 0){
		maxHealth = 400; 
		} else {
			maxHealth = 800 + ((10 * Random.Range(1, 6)) * (PlayerPrefs.GetInt("Day") - 1)); 
		}

		health = maxHealth; 
		//Debug.Log(health + "aaa");
		done = false;
		steam = 10;
		power = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if(health <= 0 && !death){
			if(PlayerPrefs.GetInt("Day") != 0){
			PlayerPrefs.SetInt("BattleWin", 1);
			}
			PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
			//PlayerPrefs.SetInt("Kills", PlayerPrefs.GetInt("Kills") + 1);
			death = true;
			anim.SetTrigger("death");
			Invoke("deathFr", 5f);
		}
		if(Player.health <= 0){
			SceneManager.LoadSceneAsync("Death");
			//PlayerPrefs.DeleteAll();
			//PlayerPrefs.Save();
		}
		if(Player.steam > 20) {
			Player.steam = 20;
		}
		if(GameManager.turn % 2 == 1 && !done && !playing){
			playing = true;
			StartCoroutine(AI ());
		}
		if(GameManager.turn % 2 == 1 && done){
			GameManager.anim.SetTrigger("PlayerTurn");
			if(PlayerPrefs.GetInt("Day") == 0 && GameManager.turn == 1){
				Invoke("DrawSpec", 0f);
				Invoke("DrawSpec", 0.5f);
				Invoke("DrawSpec", 1f);
				Debug.Log("spec Out");
			} else {
				Invoke("Draw", 0f);
				Invoke("Draw", 0.5f);
				Invoke("Draw", 1f);
			}
			if(Player.Shield > 0){
				Player.Shield--;
			}
			if(Player.Speed > 0){
				Player.Speed--;
			}
			if(Player.Shield == 0){
				Player.Shieldobj.SetActive(false);
			}
			playing = false;
			done = false;
			GameManager.turn++;
			//if(Player.steam < 20){
			//Player.steam += 2;
			//} 
			if(Player.steam > 20) {
				Player.steam = 20;
			}
		}

    }

	void deathFr(){
		SceneManager.LoadSceneAsync("MapMenu");
	}

	void Draw(){
		GameManager.DrawCard ();
	}

	void DrawSpec(){
		GameManager.This.DrawSpec();
	}

	IEnumerator AI(){
		for(int i = 0; i < Random.Range(1, 4); i++){
			Debug.Log("gen");
			for(int a = 0; a < 1; a++){
				yield return new WaitForSeconds(Random.Range(0.5f, 2f));
			}
			if(steam - 2 >= 0){
				power += 1;
				steam -= 2;
			} else {
				break;
			}
		}

		if(Random.Range(0, 2) != 0){
			for(int i = 0; i < Random.Range(0, 2); i++){
				if(syshealth[2] > 0){
				Debug.Log("Shield");
				for(int a = 0; a < 1; a++){
					yield return new WaitForSeconds(Random.Range(0.5f, 2f));
				}
				if(power - 1 >= 0){
					power -= 1;
					Shield++;
					if(Shield == 1){
						Shieldobj.SetActive(true);
					}
				} else {
					break;
				}
				} else {
					sysdead[2] = false;
					syshealth[2] += Random.Range(50, 75);
					break;
				}
			}
		} else {
			for(int i = 0; i < Random.Range(0, 2); i++){
				if(syshealth[3] > 0){
				Debug.Log("Speed");
				for(int a = 0; a < 1; a++){
					yield return new WaitForSeconds(Random.Range(0.5f, 2f));
				}
				if(power - 1 >= 0){
					As[2].Play();
					power -= 1;
					Speed++;
				} else {
					break;
				}
				} else {
					sysdead[3] = false;
					syshealth[3] += Random.Range(50, 75);
					break;
				}
			}
		}
		//}
		for(int a = 0; a < 1; a++){
			yield return new WaitForSeconds(Random.Range(1f, 2f));
		}
		bool repDmg = false;
		for(int i = 0; i < 2; i++){
			selectedSystem = Random.Range(0, 4);

			Debug.Log("attack");
			//attack with main weapon
			if(Player.Shield == 0 && !repDmg){
				//repreatre
				if(Static.Weapon1ID == 0){
					if(power - 1 >= 0){
						if(syshealth[0] > 0){
							StartCoroutine(Repeater());
							power -= 1;
						} else {
							power -= 1;
							sysdead[0] = false;
							syshealth[0] += Random.Range(50, 75);
							repDmg = true;
							i--;
						}
					} else {
						break;
					}
				} else {
					//light cannon
					if(power - 1 >= 0){
						if(syshealth[0] > 0){
							StartCoroutine(Light_Cannon());
							power -= 1;
						} else {
							power -= 1;
							sysdead[0] = false;
							syshealth[0] += Random.Range(50, 75);
							repDmg = true;
							i--;
						}
					} else {
						break;
					}
				}
				for(int a = 0; a < 1; a++){
					yield return new WaitForSeconds(Random.Range(4f, 6f));
				}
			} else {
				//secondary weapon
				if(power - 1 >= 0){
					if(syshealth[1] > 0){
						one = false;
					StartCoroutine(Refractor());
					power -= 1;
					} else {
						power -= 1;
						sysdead[1] = false;
						syshealth[1] += Random.Range(50, 75);
						break;
					}
				} else {
					break;
				}
				for(int a = 0; a < 1; a++){
					yield return new WaitForSeconds(Random.Range(4f, 5f));
				}
			}
		}
		for(int a = 0; a < 1; a++){
			yield return new WaitForSeconds(Random.Range(0f, 2f));
		}
		Debug.Log("done");
		done = true;

	}

	IEnumerator Repeater(){
		for(int i = 0; i < 10; i++){
			ps[0].Play();
			As[0].pitch = Random.Range(0.95f, 1.05f);
			As[0].Play();
			if(Random.Range(0, 100) > (Player.Speed * (10 * (PlayerPrefs.GetInt("DrivesLvl") + 1)))){
				int damage = (int)((float)Random.Range(5, 15) * Mathf.Pow(1.1f , (Static.Weapon1Lvl)));
				Player.health -= (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0;
				GameManager.This.SpawnDmg("-" + ((damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0), pos3);

				Player.syshealth[selectedSystem] -= (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0;
				if(Player.syshealth[selectedSystem] < 0){
					Player.syshealth[selectedSystem] = 0;
				}
				lr[0].SetPosition (0, pos.position);
				lr[0].SetPosition (1, hitpos[Random.Range(0, 4)].position);
			} else {
				GameManager.This.SpawnDmg("Miss", pos3);

				lr[0].SetPosition (0, pos.position);
				lr[0].SetPosition (1, mp[Random.Range(0, 3)].position);
			}
			anim.SetTrigger("ShootRep");
			checksystems();
			for(int a = 0; a < 1; a++){
				yield return new WaitForSeconds(0.167f);
			}
		}
		for(int a = 0; a < 1; a++){
			yield return new WaitForSeconds(0.167f);
		}
		lr[0].SetPosition (0, pos.position);
		lr[0].SetPosition (1, pos.position);
	}

	IEnumerator Refractor(){
		int c = Random.Range(0, 4);
		int c2 = Random.Range(0, 3);

		//for(int i = 0; i < 1; i++){
		Enemy.As[1].pitch = Random.Range(0.95f, 1.05f);
		Enemy.ps[1].Play();
		Enemy.As[1].Play();
		if(Random.Range(0, 100) > (Player.Speed * (10 * (PlayerPrefs.GetInt("DrivesLvl") + 1)))){
			int damage = (int)((float)Random.Range(60, 120) * Mathf.Pow(1.1f , (Static.Weapon2Lvl)));
		Player.health -= (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0;
			GameManager.This.SpawnDmg("-" + ((damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0), pos3);
			Player.syshealth[selectedSystem] -= (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0;
			if(Player.syshealth[selectedSystem] < 0){
				Player.syshealth[selectedSystem] = 0;
			}
			checksystems();

		Enemy.anim.SetTrigger("Refractor");
		for(int b = 0; b < 40; b++){
			Enemy.lr[1].SetPosition (0, pos.position);
			Enemy.lr[1].SetPosition (1, hitpos[c].position);
			yield return new WaitForSeconds(0.1f);	
			
		}
		} else {
			if(!one){
				GameManager.This.SpawnDmg("Miss", pos3);
		}
		one = true;
			Enemy.anim.SetTrigger("Refractor");
			for(int b = 0; b < 40; b++){
				Enemy.lr[1].SetPosition (0, pos.position);
				Enemy.lr[1].SetPosition (1, mp[c2].position);
				yield return new WaitForSeconds(0.1f);	
				
			}
		}
		lr[1].SetPosition (0, pos.position);
		lr[1].SetPosition (1, pos.position);
	}

	IEnumerator Light_Cannon(){
		for(int i = 0; i < 5; i++){
			if(Random.Range(0, 101) >= 10){
				Instantiate(gobj[0], myloc);
			if(Random.Range(0, 100) > (Player.Speed * (10 * (PlayerPrefs.GetInt("DrivesLvl") + 1)))){

					ps[2].Play();
					As[3].pitch = Random.Range(0.9f, 1.1f);
					As[3].Play();
				
				//lr[2].SetPosition (0, pos.position);
				//lr[2].SetPosition (1, hitpos[Random.Range(0, 4)].position);
				
				
				for(int a = 0; a < 1; a++){
					yield return new WaitForSeconds(0.2f);
				}
				ps[3].Play();
					int damage = (int)((float)Random.Range(10, 35) * Mathf.Pow(1.1f , (Static.Weapon1Lvl)));
					Player.health -= (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0;
					GameManager.This.SpawnDmg("-" + ((damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0), pos3);
					
					Player.syshealth[selectedSystem] -= (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0;
					if(Player.syshealth[selectedSystem] < 0){
						Player.syshealth[selectedSystem] = 0;
					}
					As[5].pitch = Random.Range(0.95f, 1.05f);
					As[5].Play();
					checksystems();
				for(int a = 0; a < 1; a++){
					yield return new WaitForSeconds(0.1f);
				}
				
			} else {
				ps[2].Play();
				As[3].pitch = Random.Range(0.95f, 1.05f);
				As[3].Play();
				
				
				//lr[2].SetPosition (0, pos.position);
				//lr[2].SetPosition (1, mp[Random.Range(0, 3)].position);
				//anim.SetTrigger("LightCannon");
				checksystems();
					for(int a = 0; a < 1; a++){
						yield return new WaitForSeconds(0.2f);
					}
					GameManager.This.SpawnDmg("Miss", pos3);
					As[6].pitch = Random.Range(0.95f, 1.05f);
					As[6].Play();
					for(int a = 0; a < 1; a++){
						yield return new WaitForSeconds(0.1f);
					}
			}
			} else {
				//misfire
				//anim.SetTrigger("LightCannon");
				As[4].pitch = Random.Range(0.95f, 1.05f);
				As[4].Play();
				checksystems();
				for(int a = 0; a < 1; a++){
					yield return new WaitForSeconds(0.1f);
				}
			}

		}
		for(int a = 0; a < 1; a++){
			yield return new WaitForSeconds(0.12f);
		}
		//lr[2].SetPosition (0, pos.position);
		//lr[2].SetPosition (1, pos.position);
	}
	//unfinished
	IEnumerator Heavy_Cannon(){
		for(int i = 0; i < 2; i++){
			
			if(Random.Range(0, 100) > (Player.Speed * (10 * (PlayerPrefs.GetInt("DrivesLvl") + 1)))){
				if(Random.Range(0, 101) >= 10){
					ps[2].Play();
					As[3].pitch = Random.Range(0.9f, 1.1f);
					As[3].Play();
					int damage = (int)((float)Random.Range(35, 65) * Mathf.Pow(1.1f , (Static.Weapon1Lvl)));
					Player.health -= (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0;
					GameManager.This.SpawnDmg("-" + ((damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0), pos3);
					
					Player.syshealth[selectedSystem] -= (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) > 0 ? (damage - (Player.Shield * (10 * (PlayerPrefs.GetInt("ShieldLvl") + 1)))) : 0;
					if(Player.syshealth[selectedSystem] < 0){
						Player.syshealth[selectedSystem] = 0;
					}
					lr[2].SetPosition (0, pos.position);
					lr[2].SetPosition (1, hitpos[Random.Range(0, 4)].position);
				} else {
					
				}
			} else {
				ps[2].Play();
				As[3].pitch = Random.Range(0.95f, 1.05f);
				As[3].Play();
				GameManager.This.SpawnDmg("Miss", pos3);
				
				lr[2].SetPosition (0, pos.position);
				lr[2].SetPosition (1, mp[Random.Range(0, 3)].position);
			}
			anim.SetTrigger("LightCannon");
			checksystems();
			for(int a = 0; a < 1; a++){
				yield return new WaitForSeconds(0.12f);
			}
		}
		for(int a = 0; a < 1; a++){
			yield return new WaitForSeconds(0.12f);
		}
		lr[2].SetPosition (0, pos.position);
		lr[2].SetPosition (1, pos.position);
	}

	void checksystems(){
		if(Player.syshealth[2] <= 0 && !Player.sysdead[2]){
			Player.sysdead[2] = true;
			Player.Shieldobj.SetActive(false);
			Player.Shield = 0;
			Player.me.DestroyParts[6].Play();
			Player.me.DestroyAud[2].Play();
		} else if(Player.syshealth[3] <= 0 && !Player.sysdead[3]){
			Player.sysdead[3] = true;
			Player.Speed = 0;
			Player.me.DestroyParts[7].Play();
			Player.me.DestroyAud[3].Play();
		} else if(Player.syshealth[1] <= 0 && !Player.sysdead[1]){
			Player.sysdead[1] = true;
			Player.me.DestroyParts[5].Play();
			Player.me.DestroyAud[1].Play();
		} else if(Player.syshealth[0] <= 0 && !Player.sysdead[0]){
			Player.sysdead[0] = true;
			Player.me.DestroyParts[4].Play();
			Player.me.DestroyAud[0].Play();
		}
	}
}
