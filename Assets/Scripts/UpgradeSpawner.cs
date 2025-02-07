using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeSpawner : MonoBehaviour
{
	public List<GameObject> cards = new List<GameObject>();

	public Transform[] spots;

	public static List<GameObject> cardsremaining = new List<GameObject>();

	public GameObject[] gb;

	public static GameObject[] gameobjects;

	public GameObject[] gb2;
	
	public static GameObject[] gameobjects2;

	public TMP_Text text1;
	public TMP_Text text2;

	public static TMP_Text textTitle;
	public static TMP_Text textDesc;

    // Start is called before the first frame update
    void Start()
    {
		textTitle = text1;
		textDesc = text2;
		gameobjects = gb;
		gameobjects2 = gb2;
		Random.seed = System.DateTime.Now.Millisecond;
		List<GameObject> cards2 = cards;
		for(int i = 0; i < 3; i++){
			int a = Random.Range(0, cards2.Count);
			cards2[a].transform.position = spots[i].position;
			cards2[a].SetActive(true); 
			cardsremaining.Add(cards2[a]);
			cards2.Remove(cards2[a]);

		}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void NextMission(){
		SceneManager.LoadSceneAsync("SampleScene");
	}
}
