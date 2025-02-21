using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
	public TMP_Text output;
	public int section;
	public List<string[]> text2 = new List<string[]>();
	public bool currentlyTyping;
	public int speaker;
	public int secondarySpeaker;
	public Button[] icons;

	public AudioSource aud;

	public AudioClip[] RandMorse;

	public AudioClip[] Done;

	public GameObject[] tutorialText;

	public GameObject tutorialButton;
    // Start is called before the first frame update
    void Start()
    {
		GameManager.This.waitPlay = true;
		speaker = 0;
		currentlyTyping = false;
		section = 0;
		SetText (PlayerPrefs.GetInt("Day"), PlayerPrefs.GetInt("ID"));
		setSpeaker(int.Parse(text2 [section][1]), int.Parse(text2 [section][2]));
		StartCoroutine(Type(text2[section][0]));
		//tutorialText[0].SetActive(true);
    }

	public void startAgain(int t){
		GameManager.This.waitPlay = true;
		speaker = 0;
		currentlyTyping = false;
		section = 0;
		SetText (PlayerPrefs.GetInt("Day"), PlayerPrefs.GetInt("ID"), t);
		setSpeaker(int.Parse(text2 [section][1]), int.Parse(text2 [section][2]));
		StartCoroutine(Type(text2[section][0]));
		GameManager.This.cards.SetActive (false);
		GameManager.This.chat.SetActive (true);
	}

	void setSpeaker(int a, int b){
		for (int i = 0; i < 3; i++) {
			icons[i].interactable = false;
		}
		icons[a].interactable = true;
		for (int i = 0; i < 3; i++) {
			icons[i].gameObject.SetActive(false);
		}
		icons[a].gameObject.SetActive(true);
		icons[b].gameObject.SetActive(true);
	}

	IEnumerator Type(string text){
		if(PlayerPrefs.GetInt("Day") == 0){
			tutorialText[0].SetActive(false);
		}
		currentlyTyping = true;
		string outputText = "";
		for (int i = 0; i < text.Length; i++) {
			if(currentlyTyping){

			outputText = outputText + text.Substring(i, 1);
			output.text = outputText;

			yield return new WaitForSeconds(Random.Range(0.086f, 0.1f));
			}
			}
		if (currentlyTyping) {
			section++;
			currentlyTyping = false;
			aud.Stop();
			aud.clip = Done[0];
			aud.Play();
			if(PlayerPrefs.GetInt("Day") == 0){
				tutorialText[0].SetActive(true);
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
		if(currentlyTyping){
			if(!aud.isPlaying){
				aud.clip = RandMorse[Random.Range(0, RandMorse.Length)];
				aud.Play();
			}
		}
    }
	public void next(){
		if (!currentlyTyping) {
			if (section < text2.Count) {
				aud.Stop();
				StartCoroutine (Type (text2 [section][0]));
				setSpeaker(int.Parse(text2 [section][1]), int.Parse(text2 [section][2]));
			} else {
				if(PlayerPrefs.GetInt("Day") == 0){
					tutorialText[0].SetActive(false);
				}
				done ();
			}
		} else {
			currentlyTyping = false;
			aud.Stop();
			aud.clip = Done[0];
			aud.Play();
			StopCoroutine(Type (text2 [section][0]));
			output.text = text2 [section][0];
			section++;
			if(PlayerPrefs.GetInt("Day") == 0){
				tutorialText[0].SetActive(true);
			}

		}
	}

	public void done(){
		GameManager.This.waitPlay = false;
		GameManager.anim.SetTrigger("PlayerTurn");
		GameManager.This.cards.SetActive (true);
		GameManager.This.chat.SetActive (false);
		GameManager.This.startup ();
		if(PlayerPrefs.GetInt("Day") == 0){
			tutorialButton.SetActive(true);
			tutorialText[1].SetActive(true);
		}

	}

	public void SetText(int day, int ID, int a){
		if (day == 0) {
			text2.Add (new string[]{"Good afternoon commander!", "1", "0"});
			text2.Add (new string[]{"With all of your training, we are sure that you know exactly what you're doing.", "1", "0"});
			text2.Add (new string[]{"Just to make sure that you are combat ready, we will be giving you a quick rundown on how to operate an Airship.", "1", "0"});
			text2.Add (new string[]{"Sounds good!", "0", "1"});
			//text2.Add (new string[]{"", "1", "0"});
		}else if (day == 1) {
			text2.Add (new string[]{"Testing testing. Do you copy?", "0", "1"});
			text2.Add (new string[]{"Indeed we do commander.", "1", "0"});
			text2.Add (new string[]{"Your mission is very simple, destroy the enemy Airship.", "1", "0"});
			text2.Add (new string[]{"Affirmitive.", "0", "1"});
			text2.Add (new string[]{"It is I! The spooky scary enemy Airship! I'll shoot you!", "2", "0"});
		} else if (day == 2) {
			text2.Add (new string[]{"I've arrived at the objective area.", "0", "1"});
			text2.Add (new string[]{"Affirmitive.", "1", "0"});
			text2.Add (new string[]{"Once again, we need you to destroy the enemy Airship.", "1", "0"});
			text2.Add (new string[]{"Understood.", "0", "1"});
			text2.Add (new string[]{"There isnt much for me to say besides that.", "1", "0"});
			text2.Add (new string[]{"I'm back again! You really thought you could simply shoot me to death??", "2", "0"});
			text2.Add (new string[]{"Well i'm actually a different person this time...", "2", "0"});
			text2.Add (new string[]{"Doesnt matter.", "2", "0"});
			text2.Add (new string[]{"I'll just continue shooting at you if you so desire.", "0", "1"});
		} else if (day == 3) {
			text2.Add (new string[]{"Hello!", "0", "1"});
			text2.Add (new string[]{"...", "2", "0"});
			text2.Add (new string[]{"Your mission remains the same, destroy the enemy", "1", "0"});
			text2.Add (new string[]{"Ok.", "0", "1"});
		} 
	}
}
