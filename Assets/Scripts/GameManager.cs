using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class GameManager : MonoBehaviour
{
	public List<Card> deck = new List<Card>();
	//public List<Card> disc2 = new List<Card>();
	public GameObject deckFull;
	public Transform[] cardSlots;
	public bool[] avalCardSlots;

	public static List<Card> deck2 = new List<Card>();
	public static List<Card> discard = new List<Card>();
	public static Transform[] cardSlots2;
	public static bool[] avalCardSlots2;
	public static int avalSlotCount;

	public static int turn;

	public TMP_Text text;

	public RectTransform PlayerHp;
	public RectTransform EnemyHp;

	public RectTransform[] PlayersysHp;
	public RectTransform[] EnemysysHp;

	public RectTransform[] Indicators;

	public TMP_Text[] PlayersysHptxt;
	public TMP_Text[] EnemysysHptxt;

	public TMP_Text[] Hptxt;

	public TMP_Text decksize;

	public RectTransform Steam;
	public RectTransform Power;

	public Animator anim2;
	public static Animator anim;

	public ParticleSystem[] partdmgs;

	public TMP_Text[] EnemyEquip;
	public TMP_Text[] PlayerEquip;

	public static GameManager This;

	public GameObject dmgpf;

	private string[] enemyweapons = new string[4];

	public static bool ChatMode;

	public GameObject cards;
	public GameObject chat;

	public AudioSource Music;

	private int cardtodraw;
	private int[] cardstodraw = {0, 3, 1, 0, 2, 1};

	public Sprite[] sprites;
	//public Text deckSizeText;
	void Awake(){
		cards.SetActive (false);
		chat.SetActive (true);
		Static.Weapon1ID = Random.Range (0, 2);
		This = this;
		anim = anim2;
		turn = 0;
		//MAKE SURE DECK IS CLEAR (i forgor this was here for a while and had a bug with it)
		deck2.Clear ();
		discard.Clear ();
		foreach(Transform child in deckFull.transform){
			deck2.Add (child.GetChild(0).gameObject.GetComponent<Card> ());
			//deck.Add (child.gameObject.GetComponent<Card> ());
		}
		deck = deck2;
		//disc2 = discard;
		cardSlots2 = cardSlots;
		avalCardSlots2 = avalCardSlots;
		avalSlotCount = avalCardSlots.Length;

		if (Static.Weapon1ID == 0) {
			enemyweapons [0] = "Repeater";
		} else {
			enemyweapons[0] = "Light Cannon";
		}
	}

	public void startup(){
		Music.Play ();
		Shuffle();
		if(PlayerPrefs.GetInt("Day") != 0){
		Invoke("Draw", 0f);
		Invoke("Draw", 0.5f);
		Invoke("Draw", 1f);
		} else {
			Invoke("DrawSpec", 0f);
			Invoke("DrawSpec", 0.5f);
			Invoke("DrawSpec", 1f);
		}
	}

	void Draw(){
		DrawCard ();
	}

	public void DrawSpec(){
		DrawSpecificCard (cardstodraw[cardtodraw]);
		cardtodraw++;
	}

	public static void DrawCard(){
		//draw a random card from deck
		if (deck2.Count >= 1) {
			Random.seed = System.DateTime.Now.Millisecond;
			Card randCard = deck2[Random.Range(0, deck2.Count)];

			for(int i = 0; i < cardSlots2.Length; i++){
				if(avalCardSlots2[i]){
					deck2.Remove (randCard);
					Debug.Log(randCard.ID + " #" + i);
					avalSlotCount--;
					randCard.handInd = i;
					randCard.transform.parent.transform.position = cardSlots2[i].position;
					randCard.transform.parent.gameObject.SetActive(true);
					randCard.played = false;
					avalCardSlots2[i] = false;
					return;
				}
			}
		}
	}

	public static void DrawSpecificCard(int Number){
		//draw a random card from deck
		if (deck2.Count >= 1) {
			Random.seed = System.DateTime.Now.Millisecond;
			Card randCard = deck2[Number];
			
			for(int i = 0; i < cardSlots2.Length; i++){
				if(avalCardSlots2[i]){
					deck2.Remove (randCard);
					Debug.Log(randCard.ID + " #" + i);
					avalSlotCount--;
					randCard.handInd = i;
					randCard.transform.parent.transform.position = cardSlots2[i].position;
					randCard.transform.parent.gameObject.SetActive(true);
					randCard.played = false;
					avalCardSlots2[i] = false;
					return;
				}
			}
		}
	}

	public static void Shuffle(){
		//shuffle the deck
		if (discard.Count >= 1) {
			foreach(Card card in discard){
				deck2.Add (card);
			}
			discard.Clear ();
		}
	}

	public void SpawnDmg(string s, Transform pos){
		GameObject clne = Instantiate(dmgpf, pos.position, pos.rotation);
		clne.GetComponent<DamageShower>().Set(s);
		clne.GetComponent<Rigidbody2D> ().AddForce (new Vector2 ( Random.Range(-2, 3), 0), ForceMode2D.Impulse);
		
	}

	private void Update(){
		decksize.text = deck2.Count + "";
		if(GameManager.turn % 2 == 0){
		text.text = "Your Turn \n" + turn + "";
		} else if(GameManager.turn % 2 == 1){
			text.text = "Enemy's Turn \n" + turn + "";
		}

		EnemyEquip[0].text = "MK " + Static.Weapon1Lvl + " " + enemyweapons[0];
		EnemyEquip[1].text = "MK " + Static.Weapon2Lvl + " Refractor";
		EnemyEquip[2].text = "MK " + Static.ShieldLvl + " Shields";
		EnemyEquip[3].text = "MK " + Static.DrivesLvl + " Drives";

		if(PlayerPrefs.GetInt("EquippedWeapon") == 0){
		PlayerEquip[0].text = "MK " + PlayerPrefs.GetInt("RepeaterLvl") + " Repeater";
		} else {
		PlayerEquip[0].text = "MK " + PlayerPrefs.GetInt("RepeaterLvl") + " Light Cannon";
		}
		PlayerEquip[1].text = "MK " + PlayerPrefs.GetInt("RefractorLvl") + " Refractor";
		PlayerEquip[2].text = "MK " + PlayerPrefs.GetInt("ShieldLvl") + " Shields";
		PlayerEquip[3].text = "MK " + PlayerPrefs.GetInt("DrivesLvl") + " Drives";

		PlayerHp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (Player.health * 1.25f) * 0.51f);
		EnemyHp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (int)(Enemy.health  * (510f / Enemy.maxHealth)));
		//Debug.Log((int)(1000f  * (510f / 1000f)) + "");
		Steam.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Player.steam * 25.5f);
		Power.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Player.power * 25.5f);

		Hptxt[0].text = Player.health + "/800 Hull Health";
		Hptxt[1].text = Enemy.health + "/" + Enemy.maxHealth + " Hull Health";

		if(Player.health < 400){
			if(Player.health < 200 && !partdmgs[2].isPlaying){
				partdmgs[2].Play();
			}
			if(!partdmgs[0].isPlaying){
				partdmgs[0].Play();
			}
		} else {
			partdmgs[2].Stop();
			partdmgs[0].Stop();
		}

		if(Enemy.health < Enemy.maxHealth / 2){
			if(Enemy.health < Enemy.maxHealth / 4 && !partdmgs[3].isPlaying){
				//Debug.Log("d0d0");
				partdmgs[3].Play();
			}
			if(!partdmgs[1].isPlaying){
				partdmgs[1].Play();
			}
		} else {
			partdmgs[3].Stop();
			partdmgs[1].Stop();
		}

		for(int i = 0; i < 4; i++){
			EnemysysHp[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Enemy.syshealth[i] * (11f/15f));
			PlayersysHp[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Player.syshealth[i] * (11f/15f));
			PlayersysHptxt[i].text = Player.syshealth[i] + "/150";
			EnemysysHptxt[i].text = Enemy.syshealth[i] + "/150";
		}
		//Global.deckSize.text = deck2.Count.ToString ();
		//Global.discardSize.text = discard.Count.ToString ();
	}
}
