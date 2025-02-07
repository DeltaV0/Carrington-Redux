using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageShower : MonoBehaviour
{
	public TMP_Text msg;
    // Start is called before the first frame update
    void Start()
    {
		Invoke("Kill", 5f);
    }

	public void Set(string s){
		//set the text
		msg.SetText(s);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void Kill(){
		Destroy(gameObject);
	}
}
