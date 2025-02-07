using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public bool player;

	public int targetx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(player){
			if(transform.position.x <= 0){
				if(transform.position.x <= targetx){

				}
			}
		}
    }
}
