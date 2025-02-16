using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
	public int type;

//	public bool player;

//	public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
		if(type == 0){
			StartCoroutine(Bolt());
		} else if(type == 1){
			StartCoroutine(Rocket());
		}
    }

	IEnumerator Bolt(){
		for(int a = 0; a < 1; a++){
			yield return new WaitForSeconds(0.35f);
		}
		Destroy(gameObject);
	}

	IEnumerator Rocket(){
		for(int a = 0; a < 1; a++){
			yield return new WaitForSeconds(3f);
		}
		Destroy(gameObject);
	}
}
