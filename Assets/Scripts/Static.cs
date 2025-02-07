using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static
{
	public static int ShieldLvl;
	
	public static int DrivesLvl;
	
	public static int Weapon1Lvl;
	
	public static int Weapon2Lvl;

	public static int Weapon1ID;
	public static int Weapon2ID;
    // Start is called before the first frame update
	static Static(){
		ShieldLvl = 0;
		DrivesLvl = 0;
		Weapon1Lvl = 0;
		Weapon2Lvl = 0;
		Weapon1ID = 0;
		Weapon2ID = 10;
    }

    // Update is called once per frame
   // void Update()
   // {
        
   // }
}
