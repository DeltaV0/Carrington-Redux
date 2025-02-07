using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public GameObject[] cards;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(PlayerPrefs.GetInt("LCUnlocked?") == 1){
			cards[0].SetActive(true);
		} else {
			cards[0].SetActive(false);
		}
    }
}
