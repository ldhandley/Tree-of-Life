using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prayer_Manager : MonoBehaviour {

	[Serializable]
	public class Prayer{
		public string key;
		public Texture texture;
		public Texture cooldown_texture;
		public bool prayer_display;
		public float cooldown_time;
		public float seconds_cooldown;
		public AudioClip sound;
		public GameObject script;
	}

	public List<Prayer> Prayers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		foreach (Prayer p in Prayers) {
			AudioSource audio = GetComponent<AudioSource> ();
			if ((p.prayer_display == true) && Input.GetKeyDown (p.key)) {
				p.prayer_display = false;
				p.seconds_cooldown = 0;
				Debug.Log ("You cast a spell!");
				Instantiate(p.script);
				audio.PlayOneShot (p.sound);
			}

			p.seconds_cooldown += Time.deltaTime;
			if (p.seconds_cooldown > p.cooldown_time) {
				p.prayer_display = true;
				p.seconds_cooldown = 0;
			}
		}
	}

	void OnGUI() {
		int counter = 0;
		foreach (Prayer p in Prayers) {
			Texture x;
			if (p.prayer_display == true) {
				x = p.texture;
			} else {
				x = p.cooldown_texture;  
			}
			GUI.DrawTexture (new Rect (5 + counter*50, Screen.height - 45, 40, 40), x);
			GUI.Label (new Rect (20 + counter*50, Screen.height - 65, 200, 200), p.key);
			counter += 1;
		}
	}
}
