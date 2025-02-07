using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionSpawner : MonoBehaviour
{
	public List<Transform> spots = new List<Transform>();
	//public List<string> spotsNames = new List<string>();

	public GameObject[] missions;

	public TMP_Text[] txt2;

	public TMP_Text day;

	public static TMP_Text[] txt;
    // Start is called before the first frame update
    void Start()
    {
		txt = txt2;
		Random.seed = System.DateTime.Now.Millisecond;
		for(int i = 0; i < 3; i++){
			int a = Random.Range(0, spots.Count);
			missions[i].transform.position = spots[a].position;
			missions[i].GetComponent<Mission>().name = spots[a].gameObject.name;
			//spots[a].SetActive(true); 
			spots.Remove(spots[a]);
			
		}
    }

    // Update is called once per frame
    void Update()
    {
		day.text = "Day: " + PlayerPrefs.GetInt ("Day");
    }
}
