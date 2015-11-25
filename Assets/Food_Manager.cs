using UnityEngine;
using System.Collections;

public class Food_Manager : MonoBehaviour {
	public GameObject Last_Fruit_Eaten;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EatenLast (GameObject x){
		Last_Fruit_Eaten = x; 
	}

}
