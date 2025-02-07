using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Mission : MonoBehaviour
{
	public int difficulty;
	public string name;
	public string diff;

	private string[] AirshipType = {"eee", "eee"};

	public int ID;

	public SpriteRenderer spr;

	private bool hover;

	private int[] RuralMissions = {0, 1, 2, 3};
	private int[] UrbanMissions = {4, 5, 6, 7, 8};
	private int[] WaterMissions = {9, 10, 11};
	// Start is called before the first frame update
    void Start()
    {
		difficulty = Random.Range(0, 3);
		if(name.Equals ("Rural") || name.Equals ("Island")){
			ID = RuralMissions[Random.Range(0, 4)];
		} else if(name.Equals ("Water") || name.Equals ("Coast") || name.Equals ("Lake")){
			ID = WaterMissions[Random.Range(0, 3)];
		} else {
			if(name.Equals ("Birmingham")){
				ID = UrbanMissions[0];
			} else if(name.Equals ("Hereford")){
				ID = UrbanMissions[1];
			} else if(name.Equals ("Oxford")){
				ID = UrbanMissions[2];
			} else if(name.Equals ("Winchester")){
				ID = UrbanMissions[3];
			} else if(name.Equals ("Plymouth")){
				ID = UrbanMissions[4];
			}
		}
		if(difficulty == 0){
			spr.color = new Color(0.1f, 0.7f, 0.1f);
			diff = "Easy";
			AirshipType[0] = "small, but still dangerous";
			AirshipType[1] = "";
		} else if(difficulty == 1){
			spr.color = new Color(0.9f, 0.9f, 0.1f);
			diff = "Medium";
			AirshipType[0] = "medium sized, but still very powerful";
		} else {
			spr.color = new Color(0.7f, 0, 0);
			diff = "Hard";
			AirshipType[0] = "large, heavily equipped";
		}

    }

    // Update is called once per frame
    void Update()
    {
		if(hover){
			CardStats();
		}
    }
	private void OnMouseDown(){
		Static.ShieldLvl = Random.Range(0, PlayerPrefs.GetInt("ShieldLvl") + 1 + difficulty);
		Static.DrivesLvl = Random.Range(0, PlayerPrefs.GetInt("DrivesLvl") + 1 + difficulty);
		Static.Weapon1Lvl = Random.Range(0, PlayerPrefs.GetInt("RepeaterLvl") + 1 + difficulty);
		Static.Weapon2Lvl = Random.Range(0, PlayerPrefs.GetInt("RefractorLvl") + 1 + difficulty);
		PlayerPrefs.SetInt("MissionID", ID);
		SceneManager.LoadSceneAsync("SampleScene");
	}
	private void OnMouseOver(){
		hover = true;
	}
	private void OnMouseExit(){
		hover = false;
		MissionSpawner.txt[0].text = "";
		MissionSpawner.txt[1].text = "";
		MissionSpawner.txt[2].text = "";
	}

	void CardStats(){
		switch(ID)
		{
			case 0:
				MissionSpawner.txt[0].text = "Munitions";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "In times of war, ammunition is very important. There is a munitions depot at this location that is currently being threatened by a " + AirshipType[0] + ", airship. Destroy the enemy as soon as possible and protect our supply of ammunition.";
				break;
			case 1:
				MissionSpawner.txt[0].text = "Distraction";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "A large enemy force, made up of several " + AirshipType[0] + ", airships, is very quickly approaching this location. We need you to slow down the enemy forces before they are able to push any further until we are able to deploy our own forces to push back.";
				break;
			case 2:
				MissionSpawner.txt[0].text = "Turncoat";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "One of our commanders appears to have issues with loyalty, and has switched sides to join the French. He has vital strategic information that would cause a lot of trouble if it were to fall into enemy hands. He is equipped with a " + AirshipType[0] + ", airship, and he must be destroyed immediately!";
				break;
			case 3:
				MissionSpawner.txt[0].text = "Industrial Bombing";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "A " + AirshipType[0] + ", airship equipped with a large payload of bombs is approaching an industrial zone that is producing vital resources. Defend the area and destroy the airship before he is able to drop his payload.";
				break;
			case 4:
				MissionSpawner.txt[0].text = "Cloudfire";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "A " + AirshipType[0] + ", airship is concerningly close to our capital city of Birmingham. We are unsure of the Airship's mission, but we can be certain the it is a danger to the people who live in the city. Destroy the Airship as soon as possible!";
				break;
			case 5:
				MissionSpawner.txt[0].text = "Attack on Hereford";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "After the flooding on london, Hereford gained a large amount of population. We're concerned about a " + AirshipType[0] + ", airship flying near the city's airspace. We need you to destroy the Airship quickly before it can finish doing whatever it was doing.";
				break;
			case 6:
				MissionSpawner.txt[0].text = "Oxford Ablaze";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "The parts of Oxford that are not yet flooded remain heavily urbanized. The " + AirshipType[0] + ", airship could pose a threat to the people living there. Keep the Airship out of our airspace.";
				break;
			case 7:
				MissionSpawner.txt[0].text = "Defense of Winchester";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "The city of Winchester is currently one of our industrial powerhouses, and we must keep the factories running if we are to continue fighting this war. A " + AirshipType[0] + ", airship that is currently closing in on the city is likely going to damage or destroy many factories, and we will be less equipped to deal with the french onslaught. Destroy the Airship in question before it manages to cause any damage.";
				break;
			case 8:
				MissionSpawner.txt[0].text = "Aerial Gambit";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "A large portion of our navy is stationed at Plymouth, and we will need it in order to disrupt french supplies and landings in southern england. The french are currently scrampling to land their armies due to the coming hurricane, and thus our navy must remain powerful to keep them out. We have spotted a " + AirshipType[0] + ", airship rapidly approaching our dockyards, and we need you to destroy it at once!";
				break;
			case 9:
				MissionSpawner.txt[0].text = "Waterways";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "A " + AirshipType[0] + ", airship has been spotted near this location. Destroy it before it makes landfall and causes damage.";
				break;
			case 10:
				MissionSpawner.txt[0].text = "Coastal Assault";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "Protect our coasts from the " + AirshipType[0] + ", airship that is approching.";
				break;
			case 11:
				MissionSpawner.txt[0].text = "Rogue Waters";
				MissionSpawner.txt[1].text = diff;
				MissionSpawner.txt[2].text = "A " + AirshipType[0] + ", airship is escorting a french supply ship off of the coast. Distract the Airship and destroy it to allow us to keep the ship's supplies out of the enemy's hands.";
				break;
		}
	}
}
