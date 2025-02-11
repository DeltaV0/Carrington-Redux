using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Transform damagespawnpos;

	public static Player me;

	public static int health;

	public static int[] syshealth = new int[4];

	public static bool[] sysdead = new bool[4];

	public static int selectedSystem;

	public static int steam;
	public static int power;

	public static int Shield;

	public static int Speed;

	public static Transform pos;
	public Transform pos2;

	public static Transform[] hitpos;

	public Transform[] hp;

	public static Transform[] misspos;
	
	public Transform[] mp;


	public static LineRenderer[] lr;

	public static Animator anim;

	public LineRenderer[] lr2;
	
	public Animator anim2;

	public static ParticleSystem[] ps;

	public ParticleSystem[] ps2;

	public static AudioSource[] As;
	
	public AudioSource[] As2;

	public static GameObject Shieldobj;

	public GameObject shobj;

	public GameObject[] gobj2;

	public Transform myloc;

	public ParticleSystem[] DestroyParts;
	public AudioSource[] DestroyAud;
    // Start is called before the first frame update
    void Start()
    {
		me = this;
		selectedSystem = 0;
		syshealth[0] = 150;
		syshealth[1] = 150;
		syshealth[2] = 150;
		syshealth[3] = 150;
		Shieldobj = shobj;
		As = As2;
		ps = ps2;
		lr = lr2;
		anim = anim2;
		misspos = mp;
		hitpos = hp;
		pos = pos2;
		health = 800;
		steam = 6;
		power = 1;
		Shield = 0;
		Speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
		Buttons.text3.text = (steam * 5) + " Steam / 100";
		Buttons.text4.text = (power * 5) + " Power / 100";
    }
}
