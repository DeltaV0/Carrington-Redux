using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEquipment : MonoBehaviour
{

	public int ID;

	public GameObject otherWeapon;

	public bool equipped;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

	private void OnMouseDown(){
		if(!equipped){
		if(ID == 0){
			PlayerPrefs.SetInt("EquippedWeapon", 0);
		} else {
			PlayerPrefs.SetInt("EquippedWeapon", 1);
		}
			Vector3 swaptrans = otherWeapon.transform.position;
		//swaptrans.position = otherWeapon.transform.position;
		otherWeapon.transform.position = transform.position;
		transform.position = swaptrans;
			equipped = true;
			otherWeapon.GetComponent<CardEquipment>().equipped = false;
		}
	}
}
