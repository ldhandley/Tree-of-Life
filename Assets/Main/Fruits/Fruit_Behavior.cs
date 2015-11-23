using UnityEngine;
using System.Collections;

public class Fruit_Behavior : MonoBehaviour {
	private float max_fruit_size;
	private float fruit_growing;
	public float growth_speed;
	public AudioClip eating_sound;
	// Use this for initialization
	void Start () {
		max_fruit_size = gameObject.transform.localScale.y;
		fruit_growing = 0;
		gameObject.transform.localScale = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (fruit_growing < max_fruit_size) {
			fruit_growing += Time.deltaTime*growth_speed;
			fruit_growing = Mathf.Min(fruit_growing,max_fruit_size);
			gameObject.transform.localScale = new Vector3 (fruit_growing,fruit_growing,fruit_growing);
		} else {
		}


	}

	void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.name == "Player") {
			AudioSource audio = collision.gameObject.GetComponent<AudioSource> ();
			audio.PlayOneShot (eating_sound);
			Destroy(gameObject);
			GameObject.Find ("HealthBar").GetComponent<Healthbar> ().ModifyHealth (-100);
		}
	}

}
