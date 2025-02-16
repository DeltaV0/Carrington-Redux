using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public GameObject[] cards;

	public GameObject[] options;

	public static GameObject choice;

	public Transform[] optionsT;
	
	public static Transform choiceT;


    // Start is called before the first frame update
    void Start()
    {
        
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
		if(PlayerPrefs.GetInt("EquippedWeapon") == 0){
			options[0].transform.position = optionsT[2].position;
			options[0].GetComponent<CardEquipment>().equipped = true;
			options[1].transform.position = optionsT[1].position;
			options[1].GetComponent<CardEquipment>().equipped = false;
			options[2].transform.position = optionsT[0].position;
			options[2].GetComponent<CardEquipment>().equipped = false;
		} else if(PlayerPrefs.GetInt("EquippedWeapon") == 1){
			options[1].transform.position = optionsT[2].position;
			options[1].GetComponent<CardEquipment>().equipped = true;
			options[0].transform.position = optionsT[1].position;
			options[0].GetComponent<CardEquipment>().equipped = false;
			options[2].transform.position = optionsT[0].position;
			options[2].GetComponent<CardEquipment>().equipped = false;
		} else if(PlayerPrefs.GetInt("EquippedWeapon") == 2){
			options[2].transform.position = optionsT[2].position;
			options[2].GetComponent<CardEquipment>().equipped = true;
			options[0].transform.position = optionsT[1].position;
			options[0].GetComponent<CardEquipment>().equipped = false;
			options[1].transform.position = optionsT[0].position;
			options[1].GetComponent<CardEquipment>().equipped = false;
		}

	}
}
