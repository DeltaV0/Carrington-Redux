using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEquipment : MonoBehaviour
{

	public int ID;

	public GameObject otherWeapon;

	public bool equipped;

	public int slot;

	public Transform originalpos;

	public bool selected;
    // Start is called before the first frame update
    void Start()
    {
		//PlayerPrefs.SetInt("EquippedWeapon2", 3);
		//PlayerPrefs.SetInt("MLUnlocked?", 1);
    }

    // Update is called once per frame
    void Update()
    {
		//otherWeapon = WeaponManager.choice;
		if(WeaponManager.selectedSlot == slot){
			transform.position = originalpos.position + new Vector3(0, 10, 0);
		} else {
			transform.position = originalpos.position;
		}
    }

	private void OnMouseDown(){
		//selected = true
		if(!equipped){
			if(WeaponManager.selectedSlot == 1){
		if(ID == 0){
			PlayerPrefs.SetInt("EquippedWeapon1", 0);
		} else if(ID == 1){
			PlayerPrefs.SetInt("EquippedWeapon1", 1);
		} else if(ID == 2){
			PlayerPrefs.SetInt("EquippedWeapon1", 2);
		}else if(ID == 3){
			PlayerPrefs.SetInt("EquippedWeapon2", 3);
		}
			} else if(WeaponManager.selectedSlot == 2){
		if(ID == 0){
			PlayerPrefs.SetInt("EquippedWeapon2", 0);
		} else if(ID == 1){
			PlayerPrefs.SetInt("EquippedWeapon2", 1);
		} else if(ID == 2){
			PlayerPrefs.SetInt("EquippedWeapon2", 2);
		} else if(ID == 3){
			PlayerPrefs.SetInt("EquippedWeapon2", 3);
		}
			}
		WeaponManager.selectedSlot = 0;
			//Vector3 swaptrans = otherWeapon.transform.position;
		//swaptrans.position = otherWeapon.transform.position;
		//otherWeapon.transform.position = transform.position;
		//transform.position = swaptrans;
			//equipped = true;
			//otherWeapon.GetComponent<CardEquipment>().equipped = false;
		} else {
			if(WeaponManager.selectedSlot != slot){
				WeaponManager.selectedSlot = slot;
			} else {
				WeaponManager.selectedSlot = 0;
			}
		}
	}
}
