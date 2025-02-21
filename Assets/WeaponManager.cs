using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public GameObject[] cards;

	public GameObject[] options;

	public static GameObject choice;

	public Transform[] optionsselectedT;

	public Transform[] optionsqueueT;
	
	public static Transform choiceT;

	public static int selectedSlot;

    // Start is called before the first frame update
    void Start()
    {
		set ();
    }

    // Update is called once per frame
    void Update()
    {
		set ();
		//choice = options[PlayerPrefs.GetInt("EquippedWeapon")];
		//choiceT = optionsT[PlayerPrefs.GetInt("EquippedWeapon")];
		if(PlayerPrefs.GetInt("LCUnlocked?") == 1){
			cards[0].SetActive(true);
		} else {
			cards[0].SetActive(false);
		}
		if(PlayerPrefs.GetInt("MLUnlocked?") == 1){
			cards[1].SetActive(true);
		} else {
			cards[1].SetActive(false);
		}
    }

	void set(){
		for(int i = 0; i < 4; i++){
			if(i != PlayerPrefs.GetInt("EquippedWeapon1") && i != PlayerPrefs.GetInt("EquippedWeapon2")){
				options[i].transform.position = optionsqueueT[i].position;
				options[i].GetComponent<CardEquipment>().originalpos = options[i].transform;
				options[i].GetComponent<CardEquipment>().equipped = false;
				options[i].GetComponent<CardEquipment>().slot = -1;
			} else if(i == PlayerPrefs.GetInt("EquippedWeapon1")){
				options[i].transform.position = optionsselectedT[0].position;
				options[i].GetComponent<CardEquipment>().originalpos = options[i].transform;
				options[i].GetComponent<CardEquipment>().equipped = true;
				options[i].GetComponent<CardEquipment>().slot = 1;
			} else if(i == PlayerPrefs.GetInt("EquippedWeapon2")){
				options[i].transform.position = optionsselectedT[1].position;
				options[i].GetComponent<CardEquipment>().originalpos = options[i].transform;
				options[i].GetComponent<CardEquipment>().equipped = true;
				options[i].GetComponent<CardEquipment>().slot = 2;
			}
		}
		/*if(PlayerPrefs.GetInt("EquippedWeapon1") == 0){
			options[0].transform.position = optionsT[2].position;
			options[0].GetComponent<CardEquipment>().equipped = true;
			options[0].GetComponent<CardEquipment>().slot = 1;
			options[1].transform.position = optionsT[1].position;
			options[1].GetComponent<CardEquipment>().equipped = false;
			options[2].transform.position = optionsT[0].position;
			options[2].GetComponent<CardEquipment>().equipped = false;
		} else if(PlayerPrefs.GetInt("EquippedWeapon1") == 1){
			options[1].transform.position = optionsT[2].position;
			options[1].GetComponent<CardEquipment>().equipped = true;
			options[1].GetComponent<CardEquipment>().slot = 1;
			options[0].transform.position = optionsT[1].position;
			options[0].GetComponent<CardEquipment>().equipped = false;
			options[2].transform.position = optionsT[0].position;
			options[2].GetComponent<CardEquipment>().equipped = false;
		} else if(PlayerPrefs.GetInt("EquippedWeapon1") == 2){
			options[2].transform.position = optionsT[2].position;
			options[2].GetComponent<CardEquipment>().equipped = true;
			options[2].GetComponent<CardEquipment>().slot = 1;
			options[0].transform.position = optionsT[1].position;
			options[0].GetComponent<CardEquipment>().equipped = false;
			options[1].transform.position = optionsT[0].position;
			options[1].GetComponent<CardEquipment>().equipped = false;
		}

		if(PlayerPrefs.GetInt("EquippedWeapon2") == 0){
			options[0].transform.position = optionsT[3].position;
			options[0].GetComponent<CardEquipment>().equipped = true;
			options[0].GetComponent<CardEquipment>().slot = 2;
			options[1].transform.position = optionsT[1].position;
			options[1].GetComponent<CardEquipment>().equipped = false;
			options[2].transform.position = optionsT[0].position;
			options[2].GetComponent<CardEquipment>().equipped = false;
		} else if(PlayerPrefs.GetInt("EquippedWeapon2") == 1){
			options[1].transform.position = optionsT[3].position;
			options[1].GetComponent<CardEquipment>().equipped = true;
			options[1].GetComponent<CardEquipment>().slot = 2;
			options[0].transform.position = optionsT[1].position;
			options[0].GetComponent<CardEquipment>().equipped = false;
			options[2].transform.position = optionsT[0].position;
			options[2].GetComponent<CardEquipment>().equipped = false;
		} else if(PlayerPrefs.GetInt("EquippedWeapon2") == 2){
			options[2].transform.position = optionsT[3].position;
			options[2].GetComponent<CardEquipment>().equipped = true;
			options[2].GetComponent<CardEquipment>().slot = 2;
			options[0].transform.position = optionsT[1].position;
			options[0].GetComponent<CardEquipment>().equipped = false;
			options[1].transform.position = optionsT[0].position;
			options[1].GetComponent<CardEquipment>().equipped = false;
		}*/

	}
}
