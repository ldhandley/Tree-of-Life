using UnityEngine;
using System.Collections;
using Artngame.INfiniDy;

public class Grass_Grows_on_Key_Press : MonoBehaviour {

	public GameObject GrassPrefabs;
	private Vector3 playerPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F)){
			InfiniGRASSManager gm = GameObject.Find("Grass Manager").GetComponent<InfiniGRASSManager>();


			Vector3 playerPos = gameObject.transform.position;
			GameObject TEMP = Instantiate (GrassPrefabs);
			playerPos.y = Terrain.activeTerrain.SampleHeight (playerPos);
			TEMP.transform.position = playerPos;
			INfiniDyGrassField TREE = TEMP.GetComponent<INfiniDyGrassField> ();
			TREE.Intial_Up_Vector = Vector3.up;
			TREE.Grow_tree = true;

			//v1.1 - terrain adapt
			if (gm.AdaptOnTerrain) {
				int Xpos = (int)(((playerPos.x - gm.Tpos.x)*gm.Tdata.alphamapWidth/gm.Tdata.size.x));
				int Zpos = (int)(((playerPos.z - gm.Tpos.z)*gm.Tdata.alphamapHeight/gm.Tdata.size.z));
				float[,,] splats = gm.Tdata.GetAlphamaps(Xpos,Zpos,1,1);
				float[] Tarray = new float[splats.GetUpperBound(2)+1];
				for(int j =0;j<Tarray.Length;j++){
					Tarray[j] = splats[0,0,j];
					//Debug.Log(Tarray[j]); // ScalePerTexture
				}
				float Scaling = 0;
				for(int j =0;j<Tarray.Length;j++){
					if(j > gm.ScalePerTexture.Count-1){
						Scaling = Scaling + (1*Tarray[j]);
					}else{
						Scaling = Scaling + (gm.ScalePerTexture[j]*Tarray[j]);
					}
				}
				TREE.End_scale = Scaling*UnityEngine.Random.Range (gm.min_scale, gm.max_scale);
				//Debug.Log(Tarray);
			}else{
				TREE.End_scale = UnityEngine.Random.Range (gm.min_scale, gm.max_scale);
			}
			
			TREE.Max_interact_holder_items = gm.Max_interactive_group_members;//Define max number of trees grouped in interactive batcher that opens up. 
			//Increase to lower draw calls, decrease to lower spikes when group is opened for interaction
			TREE.Max_trees_per_group = gm.Max_static_group_members;
			
			TREE.Interactive_tree = gm.Interactive;
			TREE.transform.localScale *= TREE.End_scale * gm.Collider_scale;
			
			if(gm.Override_spread){
				TREE.PosSpread = new Vector2(UnityEngine.Random.Range(gm.Min_spread,gm.Max_spread),UnityEngine.Random.Range(gm.Min_spread,gm.Max_spread));
			}
			if(gm.Override_density){
				TREE.Min_Max_Branching = new Vector2(gm.Min_density,gm.Max_density);
			}
			TREE.PaintedOnOBJ = GameObject.Find ("Terrain").transform;
			TREE.GridOnNormal = gm.GridOnNormal;
			TREE.max_ray_dist = gm.rayCastDist;
			TREE.MinAvoidDist = gm.MinAvoidDist;
			TREE.MinScaleAvoidDist = gm.MinScaleAvoidDist;
			TREE.InteractionSpeed = gm.InteractionSpeed;
			TREE.InteractSpeedThres = gm.InteractSpeedThres;
			
			//v1.4
			TREE.Interaction_thres = gm.Interaction_thres;
			TREE.Interaction_offset = gm.Interaction_offset;
			
			TREE.LOD_distance = gm.LOD_distance;
			TREE.LOD_distance1 = gm.LOD_distance1;
			TREE.LOD_distance2 = gm.LOD_distance2;
			TREE.Cutoff_distance = gm.Cutoff_distance;
			
			TREE.Tag_based = false;
			TREE.GrassManager = gm;
			TREE.Type = gm.Grass_selector+1;
			TREE.Start_tree_scale = TREE.End_scale/4;
			
			TREE.RandomRot = gm.RandomRot;
			TREE.RandRotMin = gm.RandRotMin;
			TREE.RandRotMax = gm.RandRotMax;
			
			TREE.GroupByObject = gm.GroupByObject;
			TREE.ParentToObject = gm.ParentToObject;
			TREE.MoveWithObject = gm.MoveWithObject;
			TREE.AvoidOwnColl = gm.AvoidOwnColl;
			
			TEMP.transform.parent = gm.GrassHolder.transform;
			
			//Add to holder, in order to mass change properties
			gm.Grasses.Add (TREE);
			gm.GrassesType.Add (gm.Grass_selector);
			
			TEMP.name = "GrassPatch" + gm.Grasses.Count.ToString (); 
			
			TREE.Grass_Holder_Index = gm.Grasses.Count - 1;//register id in grasses list								
		}
	}
}
