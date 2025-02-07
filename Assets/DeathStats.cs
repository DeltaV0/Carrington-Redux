using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathStats : MonoBehaviour
{
	public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
		text.text = "You fought for " + PlayerPrefs.GetInt("Day") + " days\nYou killed " + (PlayerPrefs.GetInt("Day") * 3 + Random.Range(3, 5) + " people");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
