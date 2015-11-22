using UnityEngine;
using System.Collections;

public class HealthDecreaser : MonoBehaviour {
	public float seconds_health_decrease = 1;
	public Texture hurt_texture;
	public int critical_health = 20;
	
	private float time_last_decrease = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		time_last_decrease += Time.deltaTime;
		if (time_last_decrease > seconds_health_decrease) {
			GameObject.Find ("HealthBar").GetComponent<Healthbar> ().ModifyHealth (1);
			time_last_decrease = 0;
		}
	}

	void OnGUI() {
		if (GameObject.Find ("HealthBar").GetComponent<Healthbar> ().health < critical_health*10) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), hurt_texture);
		}
	}
}
