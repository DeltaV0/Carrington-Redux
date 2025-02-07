using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	public GameObject[] GameObjects;

	public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void NextTutorial(){
		GameObjects[0].SetActive(false);

		GameObjects[1].SetActive(false);
	}
}
