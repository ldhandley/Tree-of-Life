using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FruitTree : MonoBehaviour {
	public GameObject Fruit_To_Grow;

	public int number_of_Fruit;
	public float Growth_Cycle;
	public float tree_radius;

	private GameObject x;

	private List<GameObject> Fruits = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		StartCoroutine(WaitAndGrow(Growth_Cycle));
	}
	
	// Update is called once per frame
	void Update () {


	}

	void Grow () {
		for (int i = 0; i < number_of_Fruit; i++) {
			Vector3 random_offset = Random.insideUnitSphere * tree_radius;
			Vector3 treePos = gameObject.transform.position;
			treePos = treePos + random_offset;
			Fruits.Add((GameObject)Instantiate (Fruit_To_Grow, treePos, Fruit_To_Grow.transform.rotation));
		}
	}

	void Fall (){
		while (Fruits.Count > 0) {
			Rigidbody y = Fruits [0].GetComponent<Rigidbody> ();
			y.useGravity = true;
			y.isKinematic = false;
			Fruits [0].GetComponent<Fruit_Behavior> ().my_prefab = Fruit_To_Grow;
			Fruits.Remove(Fruits[0]);
		}
	}
	
	IEnumerator WaitAndGrow(float waitTime) {
		while (true) {
			yield return new WaitForSeconds (waitTime);
			Fall ();
			Grow ();
		}
	}
	
}
