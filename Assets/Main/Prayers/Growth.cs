using UnityEngine;
using System.Collections;

public class Growth : MonoBehaviour {
	public GameObject Thing_To_Grow;
	private GameObject x;
	private float max_tree_size;
	private float tree_growing;
	public float growth_speed = 1;
	// Use this for initialization
	void Start () {
		Debug.Log ("STUFF IS GROWING!");

		Vector3 playerPos = GameObject.Find ("Player").transform.position;
		Vector3 playerDirection = GameObject.Find ("Player").transform.forward;
		Quaternion playerRotation = GameObject.Find ("Player").transform.rotation;
		float spawnDistance = 4;
		Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
		spawnPos.y = Terrain.activeTerrain.SampleHeight (spawnPos);
		max_tree_size = Thing_To_Grow.transform.localScale.y;
		x = (GameObject) Instantiate (Thing_To_Grow, spawnPos, playerRotation);
		x.transform.localScale *= 0.1F;


	}
	// Update is called once per frame
	void Update () {
		if (tree_growing < max_tree_size) {
			tree_growing += Time.deltaTime*growth_speed;
			tree_growing = Mathf.Min(tree_growing,max_tree_size);
			x.transform.localScale = new Vector3 (tree_growing,tree_growing,tree_growing);
		} else {
		}
	}
}
