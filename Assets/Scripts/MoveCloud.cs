using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour
{
	public Transform cloud;

	public bool pl;

	public float speed;

	public float[] resetdist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void FixedUpdate(){
		if(pl){
				cloud.position = new Vector3(-98, cloud.position.y - (speed * (Player.Speed + 1)), 0);
			if(cloud.position.y < resetdist[0]){
				cloud.position = new Vector3(-98, resetdist[1], 0);
			}
		} else {
			cloud.position = new Vector3(98, cloud.position.y - (speed * (Enemy.Speed + 1)), 0);
			if(cloud.position.y < resetdist[0]){
				cloud.position = new Vector3(98, resetdist[1], 0);
			}
		}
	}
}
