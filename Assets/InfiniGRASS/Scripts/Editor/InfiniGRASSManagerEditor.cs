﻿using UnityEditor;
using UnityEditor.Macros;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Artngame.INfiniDy {
	
	[CustomEditor(typeof(InfiniGRASSManager))] 	
	public class InfiniGRASSManagerEditor : Editor {

		//v1.4
		SerializedProperty Player_tag;
		SerializedProperty BrushSettings;
		Object play_mode_grass_holder;
		SerializedProperty Scale_grabbed;
		SerializedProperty ScaleMin_grabbed;

		//v1.3
		public Transform Wind_Zone;
		int selectedIndex = -1;

		//v1.3
		SerializedProperty EnableDist;
		SerializedProperty BrushTypePerSplat;
		SerializedProperty SubTypePerSplat;
		SerializedProperty MassPlantDensity;

		//v1.2a
		SerializedProperty WorldScale;

		SerializedProperty ScalePerTexture;

		Vector3 Camera_last_pos;
		Quaternion Camera_last_rot;//keep just before mouse drag starts
		
		SerializedProperty WindTurbulence;
		SerializedProperty SphereCastRadius;

		SerializedProperty rayCastDist;//define max raycast distance
		
		SerializedProperty MinAvoidDist;//distance to raycast around branch to see if touches terrain or should be scaled down
		SerializedProperty MinScaleAvoidDist;//distance to raycast around branch to see if touches terrain or should be avoided creation
		SerializedProperty InteractionSpeed;//define speed of grass interaction
		SerializedProperty InteractSpeedThres;	

		//v1.4
		SerializedProperty Interaction_thres;
		SerializedProperty Interaction_offset;
		
		public bool ActivateHelp = true;//help on/off
		
		SerializedProperty MinBranches;
		
		SerializedProperty Editor_view_dist;	
		
		SerializedProperty Max_interactive_group_members;
		SerializedProperty Max_static_group_members;
		SerializedProperty LOD_distance;
		SerializedProperty LOD_distance1;
		SerializedProperty LOD_distance2;
		SerializedProperty Cutoff_distance;

		SerializedProperty Collider_scale;
		SerializedProperty Gizmo_scale;

		SerializedProperty AmplifyWind;
		

		SerializedProperty TintPower;
		SerializedProperty TintFrequency;//v1.1

		SerializedProperty SpecularPower;
		
		public Texture2D GrassICON1;
		public Texture2D GrassICON2;
		public Texture2D GrassICON3;
		public Texture2D GrassICON4;
		public Texture2D GrassICON5;
		public Texture2D GrassICON6;
		
		public Texture2D GrassICON7;
		public Texture2D GrassICON8;
		public Texture2D GrassICON9;
		public Texture2D GrassICON10;
		
		public Texture2D FenceICON1;
		public Texture2D FenceICON2;
		public Texture2D RockICON1;
		public Texture2D RockICON2;
		public Texture2D RockICON3;
		
		[Range(0.0f,20.0f)]
		SerializedProperty min_grass_patch_dist;

		SerializedProperty Grass_Fade_distance;
		SerializedProperty Stop_Motion_distance;		

		SerializedProperty	ExtraGrassMaterials;//v1.4
		SerializedProperty	GrassMaterials;
		SerializedProperty	GrassPrefabs;
		SerializedProperty	GrassPrefabsIcons;//v1.3
		
		SerializedProperty	RockPrefabs;
		SerializedProperty	FencePrefabs;
		SerializedProperty	FenceMidPrefabs;
		
		public void OnEnable(){

			//v1.4
			Player_tag = serializedObject.FindProperty ("Player_tag");
			BrushSettings = serializedObject.FindProperty ("BrushSettings");
			Scale_grabbed = serializedObject.FindProperty ("Scale_grabbed");
			ScaleMin_grabbed = serializedObject.FindProperty ("ScaleMin_grabbed");

			//v1.3
			EnableDist = serializedObject.FindProperty ("EnableDist");
			BrushTypePerSplat = serializedObject.FindProperty ("BrushTypePerSplat");
			SubTypePerSplat = serializedObject.FindProperty ("SubTypePerSplat");
			MassPlantDensity = serializedObject.FindProperty ("MassPlantDensity");

			//v1.2a	
			WorldScale = serializedObject.FindProperty ("WorldScale");

			//v1.1
			ScalePerTexture = serializedObject.FindProperty ("ScalePerTexture");
			
			WindTurbulence = serializedObject.FindProperty ("WindTurbulence");
			SphereCastRadius = serializedObject.FindProperty ("SphereCastRadius");			

			rayCastDist = serializedObject.FindProperty ("rayCastDist");
			
			MinAvoidDist = serializedObject.FindProperty ("MinAvoidDist");
			MinScaleAvoidDist = serializedObject.FindProperty ("MinScaleAvoidDist");
			InteractionSpeed = serializedObject.FindProperty ("InteractionSpeed");
			InteractSpeedThres = serializedObject.FindProperty ("InteractSpeedThres");	

			//v1.4
			Interaction_thres = serializedObject.FindProperty ("Interaction_thres");
			Interaction_offset = serializedObject.FindProperty ("Interaction_offset");
	
			MinBranches = serializedObject.FindProperty ("MinBranches");			
		
			Editor_view_dist = serializedObject.FindProperty ("Editor_view_dist");			

			TintPower = serializedObject.FindProperty ("TintPower");
			TintFrequency =  serializedObject.FindProperty ("TintFrequency");

			SpecularPower = serializedObject.FindProperty ("SpecularPower");			

			AmplifyWind = serializedObject.FindProperty ("AmplifyWind");
			
			RockPrefabs = serializedObject.FindProperty ("RockPrefabs");
			FencePrefabs = serializedObject.FindProperty ("FencePrefabs");
			FenceMidPrefabs = serializedObject.FindProperty ("FenceMidPrefabs");			

			ExtraGrassMaterials = serializedObject.FindProperty ("ExtraGrassMaterials");//v1.4
			GrassMaterials= serializedObject.FindProperty ("GrassMaterials");
			GrassPrefabs= serializedObject.FindProperty ("GrassPrefabs");
			GrassPrefabsIcons = serializedObject.FindProperty ("GrassPrefabsIcons");//v1.3
			min_grass_patch_dist= serializedObject.FindProperty ("min_grass_patch_dist");
			
			Max_interactive_group_members= serializedObject.FindProperty ("Max_interactive_group_members");
			Max_static_group_members = serializedObject.FindProperty ("Max_static_group_members");
			LOD_distance= serializedObject.FindProperty ("LOD_distance");
			LOD_distance1= serializedObject.FindProperty ("LOD_distance1");
			LOD_distance2= serializedObject.FindProperty ("LOD_distance2");
			Cutoff_distance= serializedObject.FindProperty ("Cutoff_distance");
			
			Grass_selector = serializedObject.FindProperty ("Grass_selector");

			Collider_scale = serializedObject.FindProperty ("Collider_scale");
			Gizmo_scale = serializedObject.FindProperty ("Gizmo_scale");
			
			Grass_Fade_distance = serializedObject.FindProperty ("Grass_Fade_distance");
			Stop_Motion_distance = serializedObject.FindProperty ("Stop_Motion_distance");
			
			fence_scale = serializedObject.FindProperty ("fence_scale");
		}
		
		//FENCE
		SerializedProperty fence_scale;
		
		
		private InfiniGRASSManager script;
		void Awake()
		{
			script = (InfiniGRASSManager)target;
			
			script.GrassPrefab = grass_prefab as GameObject;
			
			if (script.FencesHolder == null) {
				GameObject FencesHolder = new GameObject();
				FencesHolder.transform.parent = script.transform;
				FencesHolder.name = "Fences Holder";
				script.FencesHolder = FencesHolder;
				FencesHolder.AddComponent<ControlCombineChildrenINfiniDyGrass>().MakeActive = true;
				FencesHolder.GetComponent<ControlCombineChildrenINfiniDyGrass>().Auto_Disable = true;
				FencesHolder.GetComponent<ControlCombineChildrenINfiniDyGrass>().realtime = true;
			}
			if (script.RocksHolder == null) {
				GameObject RocksHolder = new GameObject();
				RocksHolder.transform.parent = script.transform;
				RocksHolder.name = "Rocks Holder";
				script.RocksHolder = RocksHolder;
				RocksHolder.AddComponent<ControlCombineChildrenINfiniDyGrass>().MakeActive = true;
				RocksHolder.GetComponent<ControlCombineChildrenINfiniDyGrass>().Auto_Disable = true;
				RocksHolder.GetComponent<ControlCombineChildrenINfiniDyGrass>().realtime = true;
			}
			if (script.GrassHolder == null) {
				GameObject GrassHolder = new GameObject();
				GrassHolder.transform.parent = script.transform;
				GrassHolder.name = "Grass Holder";
				script.GrassHolder = GrassHolder;
			}
			if (script.GrassBatchHolder == null) {
				GameObject GrassBatchHolder = new GameObject();
				//GrassBatchHolder.transform.parent = script.transform;
				GrassBatchHolder.name = "GrassBatchHolder";
				script.GrassBatchHolder = GrassBatchHolder;
			}
			
		}
		
		public GameObject FindInChildren (GameObject gameObject, string name){		
			foreach(Transform transf in gameObject.GetComponentsInChildren<Transform>()){
				if(transf.name == name){
					Debug.Log(transf.name);
					return transf.gameObject;
				}
			}
			return null;		
		}
		
		//INITIAL DEFAULTS
		
		SerializedProperty Grass_selector;//select grass preset
		
		public Material grass_material;
		public Material grass_flower_material;
		public Material grass_wheet_material;
		public Material grass_vertex_material;
		public Material grass_vertex2_material;
		public Material grass_vertexHEAVY_material;
		
		public Material grass_whites_material;
		public Material grass_curved_vertex_material;
		public Material grass_low_grass_material;
		public Material grass_vines_material;
		
		public Object grass_prefab; // grass mesh to use
		public Object grass_flower_prefab; // grass mesh to use
		public Object grass_wheet_prefab; // grass mesh to use
		public Object grass_vertex_prefab;
		public Object grass_vertex2_prefab;
		public Object grass_vertexHEAVY_prefab;
		
		public Object grass_whites_prefab;
		public Object grass_curved_vertex_prefab;
		public Object grass_low_grass_prefab;
		public Object grass_vines_prefab;
		
		public Object rock_prefab;
		public Object fence_prefab;
		public Object fence_mid_prefab;
		
		public Object rock_prefab1;
		public Object rock_prefab2;
		public Object fence_prefab1;
		public Object fence_mid_prefab1;
		
		public bool grass_folder1;
		
		public bool Fence_bulk = false;//draw fences, or one by one
		
		Vector3 Keep_last_mouse_pos;
		bool Fence_start_added = false;
		
		void OnSceneGUI () {

			//v1.3 - draw mass place handles
			if (script.MassPlantAreaCornerA != null) {
				Handles.SphereCap(1,script.MassPlantAreaCornerA.position,Quaternion.identity,script.Gizmo_scale);
				Handles.ArrowCap(1,script.MassPlantAreaCornerA.position,Quaternion.LookRotation(-Vector3.up),script.Gizmo_scale*10);

				if(Handles.Button(script.MassPlantAreaCornerA.position,script.MassPlantAreaCornerA.rotation,0.2f,0.2f,Handles.DotCap)){
					selectedIndex = 1;
					Repaint();
				}
				if(selectedIndex == 1){
					EditorGUI.BeginChangeCheck();
					script.MassPlantAreaCornerA.position = Handles.DoPositionHandle(script.MassPlantAreaCornerA.position,Quaternion.identity);
					if(EditorGUI.EndChangeCheck()){
						Undo.RecordObject(script,"Move force");
						EditorUtility.SetDirty(script);
						//PhysicsLinker.forcePower = Vector3.Distance(PhysicsLinker.transform.position, PhysicsLinker.ForceVector);
					}	
				}
			}
			if (script.MassPlantAreaCornerB != null) {
				Handles.SphereCap(2,script.MassPlantAreaCornerB.position,Quaternion.identity,script.Gizmo_scale);
				Handles.ArrowCap(1,script.MassPlantAreaCornerB.position,Quaternion.LookRotation(-Vector3.up),script.Gizmo_scale*10);

				if(Handles.Button(script.MassPlantAreaCornerB.position,script.MassPlantAreaCornerB.rotation,0.2f,0.2f,Handles.DotCap)){
					selectedIndex = 1;
					Repaint();
				}
				if(selectedIndex == 1){
					EditorGUI.BeginChangeCheck();
					script.MassPlantAreaCornerB.position = Handles.DoPositionHandle(script.MassPlantAreaCornerB.position,Quaternion.identity);
					if(EditorGUI.EndChangeCheck()){
						Undo.RecordObject(script,"Move force");
						EditorUtility.SetDirty(script);
						//PhysicsLinker.forcePower = Vector3.Distance(PhysicsLinker.transform.position, PhysicsLinker.ForceVector);
					}	
				}
			}


			if (!script.UnGrown) {
			
				Event cur = Event.current;
			
				//DRAW MOUSE
				if (script.Rock_painting | script.Grass_painting | script.Fence_painting) {
					Ray ray1 = HandleUtility.GUIPointToWorldRay (cur.mousePosition); 
					RaycastHit hit2 = new RaycastHit ();
					if (Physics.Raycast (ray1, out hit2, Mathf.Infinity)) {

						if (script.Erasing) {
							if (script.MassErase) {

								Handles.color = Color.red;
								Handles.DrawWireDisc (hit2.point, hit2.normal, script.SphereCastRadius * 1.2f);
							} else {
								Handles.color = Color.red;

								Handles.DrawWireDisc (hit2.point, hit2.normal, 15);
							}
						} else {
							if (script.Looking) {
							
							} else {
								Handles.color = Color.white;
								Handles.DrawWireDisc (hit2.point, hit2.normal, 15 * (script.Max_spread/12));
							}
						}
					}
					SceneView.RepaintAll ();
				}
			
			
				//MAKE SURE TYPES LIST IS FILLED
				if (script.GrassesType.Count < script.Grasses.Count) {			
					script.GrassesType.Clear ();
					for (int i=0; i<script.Grasses.Count; i++) {
						script.GrassesType.Add (0);
					}
				}
			
			
				if (script.GrassBatchHolder == null) {
					GameObject GrassBatchHolder = new GameObject ();
				
					GrassBatchHolder.name = "GrassBatchHolder";
					script.GrassBatchHolder = GrassBatchHolder;
				}


				//v1.1 - check if null combiners or grass grown and items erased
				if(!Application.isPlaying & 1==1){
					for(int i=script.DynamicCombiners.Count-1;i>=0;i--){
						if(script.DynamicCombiners[i] == null){
							script.DynamicCombiners.RemoveAt(i);
						}
					}
					for(int i=script.StaticCombiners.Count-1;i>=0;i--){
						if(script.StaticCombiners[i] == null){
							script.StaticCombiners.RemoveAt(i);
						}
					}
					for(int j = script.Grasses.Count -1 ; j>=0;j--){
						if(script.Grasses[j].Grow_tree_ended && script.Grasses[j].Forest_holder == null && script.Grasses[j].root_tree == null && script.Grasses[j].Combiner == null){
							DestroyImmediate(script.Grasses[j].gameObject);
							script.Grasses.RemoveAt(j);
							script.GrassesType.RemoveAt(j);
						}
					}
				}

				//UNDO HELPERS - handle redo on erase undo - if field script is gone, remove items
				for(int i=0;i<script.DynamicCombiners.Count;i++){
					for(int j=script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items_handles.Count-1;j>=0;j--){
						if(script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items_handles[j] == null){
							script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items_handles.RemoveAt(j);
							DestroyImmediate(script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items[j].gameObject);
							script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items.RemoveAt(j);
							script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_item_count -=1;
						}
					}
				}
				for(int i=0;i<script.StaticCombiners.Count;i++){
					for(int j=script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items_handles.Count-1;j>=0;j--){
						if(script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items_handles[j] == null){
							script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items_handles.RemoveAt(j);
							DestroyImmediate(script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items[j].gameObject);
							script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items.RemoveAt(j);
							script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_item_count -=1;
						}
					}
				}


				//MAKE SURE GRASSES EXIST
				for(int j = script.Grasses.Count -1 ; j>=0;j--){
					if(script.Grasses[j] == null){
						script.Grasses.RemoveAt(j);
						script.GrassesType.RemoveAt(j);
					}
				}				
				for (int j=0; j<script.Grasses.Count;j++) {
					script.Grasses[j].Grass_Holder_Index = j;
				}



				//EDITOR PREVIEW - LOWER IN DISTANCE for performance

				//v1.2
				if(!Application.isPlaying &(Camera.current != null && (Camera.current.transform.position - script.prev_cam_pos).magnitude > 1)){
				for(int j=script.DynamicCombiners.Count-1;j>=0;j--){
					//v1.2
					bool Hero_far = true;
					if (script.DynamicCombiners[j].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items != null) {
						for (int i=0; i<script.DynamicCombiners[j].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items.Count; i++) {
							if (script.DynamicCombiners[j].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items [i] != null) {
								if (Vector3.Distance (script.DynamicCombiners[j].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items [i].position, Camera.current.transform.position) < script.Editor_view_dist) {
									Hero_far = false;
								}
							}
						}
					}
					if(Hero_far){
						script.DynamicCombiners[j].SetActive(false);
					}else{
						script.DynamicCombiners[j].SetActive(true);
					}
				}
				for(int j=script.StaticCombiners.Count-1;j>=0;j--){
					//v1.2
					bool Hero_far = true;
					if (script.StaticCombiners[j].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items != null) {
						for (int i=0; i<script.StaticCombiners[j].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items.Count; i++) {
							if (script.StaticCombiners[j].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items [i] != null) {
								if (Vector3.Distance (script.StaticCombiners[j].GetComponent<ControlCombineChildrenINfiniDyGrass>().Added_items [i].position, Camera.current.transform.position) < script.Editor_view_dist) {
									Hero_far = false;
								}
							}
						}
					}
					if(Hero_far){
						script.StaticCombiners[j].SetActive(false);
					}else{
						script.StaticCombiners[j].SetActive(true);
					}
				}
				}


				for (int i=0; i<script.Grasses.Count; i++) {

					if (script.Grasses [i].Combiner != null) {
						if (script.Grasses [i].Combiner.transform.parent != script.GrassBatchHolder.transform) {
							script.Grasses [i].Combiner.transform.parent = script.GrassBatchHolder.transform;
						}
					}
				}
			
			
				Handles.color = Color.red;
			
			
			
			
				//CLEAN UP PREVIOUS IF LOW ON BRANCHES
				if (script.CleanUp && script != null && script.Grasses.Count > 0 & !Application.isPlaying & 1==1) {
					//grab last
					INfiniDyGrassField forest = script.Grasses [script.Grasses.Count - 1];
					ControlCombineChildrenINfiniDyGrass forest_holder = forest.Combiner;
				
					if (forest.Registered_Brances.Count < script.MinBranches && forest.Grow_tree_ended && forest_holder.Added_items_handles.Count > forest.Tree_Holder_Index) {
					
						DestroyImmediate (forest_holder.Added_items_handles [forest.Tree_Holder_Index].gameObject);
						DestroyImmediate (forest_holder.Added_items [forest.Tree_Holder_Index].gameObject);
					
						forest_holder.Added_items_handles.RemoveAt (forest.Tree_Holder_Index);
						forest_holder.Added_items.RemoveAt (forest.Tree_Holder_Index);
					
						//remove from script
						script.Grasses.RemoveAt (forest.Grass_Holder_Index);
						script.GrassesType.RemoveAt (forest.Grass_Holder_Index);
					
						//adjust ids for items left
						for (int i=0; i<forest_holder.Added_items.Count; i++) {
							forest_holder.Added_items_handles [i].Tree_Holder_Index = i;
						
						}
						for (int i=0; i<script.Grasses.Count; i++) {
							script.Grasses [i].Grass_Holder_Index = i;
						}									
					
						forest_holder.Added_item_count -= 1;

						//v1.2

						if(forest_holder.Added_item_count == 0){
							DestroyImmediate(forest_holder.gameObject);
						}

						//check if combiners erased
						for(int i=script.DynamicCombiners.Count-1;i>=0;i--){
							if(script.DynamicCombiners[i] == null){
								script.DynamicCombiners.RemoveAt(i);
							}else{
								if(!Application.isPlaying){
									if(!script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().batching_initialized){
										script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().MakeActive = true;
									}
								}
							}
						}
						for(int i=script.StaticCombiners.Count-1;i>=0;i--){
							if(script.StaticCombiners[i] == null){
								script.StaticCombiners.RemoveAt(i);
							}else{
								if(!Application.isPlaying){
									if(!script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().batching_initialized){
										script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().MakeActive = true;
									}
								}
							}
						}
					
					}

				}//END CLEAN UP		




					//v1.2
					//check if combiners erased
					for(int i=script.DynamicCombiners.Count-1;i>=0;i--){
						if(script.DynamicCombiners[i] == null){
							script.DynamicCombiners.RemoveAt(i);
						}
						//Undo helpers - if not Started after undo, start batcher in editor or run Update
						if(!script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().started){
							script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().started = true;
						}
					}
					for(int i=script.StaticCombiners.Count-1;i>=0;i--){
						if(script.StaticCombiners[i] == null){
							script.StaticCombiners.RemoveAt(i);
						}
						//Undo helpers - if not Started after undo, start batcher in editor or run Update
						if(!script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().started){
							script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().started = true;
						}
					}


			
			
				if (script.Grass_painting & !Application.isPlaying) {
				
					if (script.GrassHolder == null) {
						GameObject GrassHolder = new GameObject ();
						GrassHolder.transform.parent = script.transform;
						GrassHolder.name = "Grass Holder";
						script.GrassHolder = GrassHolder;
					}
				
									

					//ERASE GRASS
					if (Input.GetKeyDown (KeyCode.LeftShift)) {
					

					} else if (cur.type == EventType.keyDown & cur.keyCode == (KeyCode.LeftControl)) {
						//rotate camera
						//Debug.Log("Lock camera");
					} else {
					
						bool erasing = false;
						script.Erasing = false;
						if (cur.modifiers > 0) {
							if ((cur.modifiers) == EventModifiers.Shift) {
							
								erasing = true;
								script.Erasing = true;
							}
						}
					
						bool looking = false;
						script.Looking = false;
						if (cur.modifiers > 0) {
							if ((cur.modifiers) == EventModifiers.Control) {
								//Debug.Log("look");

								looking = true;
								script.Looking = true;
							}
						}
					
						if (!looking & cur.keyCode != (KeyCode.LeftControl) & cur.type != EventType.keyDown &
							((cur.type == EventType.MouseDrag && cur.button == 1 & Vector3.Distance (Keep_last_mouse_pos, cur.mousePosition) > script.min_grass_patch_dist)  
							| (cur.type == EventType.MouseDown && cur.button == 1)) 
					   ) {
							Keep_last_mouse_pos = cur.mousePosition;
							Ray ray = HandleUtility.GUIPointToWorldRay (cur.mousePosition);
							RaycastHit hit = new RaycastHit ();
						
							//fix camera
							if (Camera.current != null) {
								Camera.current.transform.position = Camera_last_pos; 
								Camera.current.transform.rotation = Camera_last_rot;
							}						
						
							if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {														

								//v1.2 - dont paint if out of editor view
								//Debug.Log(Vector3.Distance (hit.point, Camera.current.transform.position));
								if (Vector3.Distance (hit.point, Camera.current.transform.position) < script.Editor_view_dist ) {


								bool is_Terrain = false;
								if ( (Terrain.activeTerrain != null && hit.collider.gameObject != null && hit.collider.gameObject == Terrain.activeTerrain.gameObject)){
									is_Terrain = true;
								}
									
								if ( is_Terrain | (script.PaintonTag && hit.collider.gameObject != null && hit.collider.gameObject.tag == "PPaint") ) {//v1.1	
								
								
									if (erasing) {									
									
									} else {
									
										Object To_instantiate = grass_prefab;								
									
										To_instantiate = script.GrassPrefabs [script.Grass_selector];
									
										GameObject TEMP = PrefabUtility.InstantiatePrefab (To_instantiate) as GameObject;
										TEMP.transform.position = hit.point;
									
										TEMP.GetComponent<INfiniDyGrassField> ().Intial_Up_Vector = hit.normal;
										TEMP.GetComponent<INfiniDyGrassField> ().Grow_in_Editor = true;

										//v1.1 - terrain adapt
										if (script.AdaptOnTerrain & is_Terrain) {
											int Xpos = (int)(((hit.point.x - script.Tpos.x)*script.Tdata.alphamapWidth/script.Tdata.size.x));
											int Zpos = (int)(((hit.point.z - script.Tpos.z)*script.Tdata.alphamapHeight/script.Tdata.size.z));
											float[,,] splats = script.Tdata.GetAlphamaps(Xpos,Zpos,1,1);
											float[] Tarray = new float[splats.GetUpperBound(2)+1];
											for(int j =0;j<Tarray.Length;j++){
												Tarray[j] = splats[0,0,j];
											}
											float Scaling = 0;
											for(int j =0;j<Tarray.Length;j++){
												if(j > script.ScalePerTexture.Count-1){
													Scaling = Scaling + (1*Tarray[j]);
												}else{
													Scaling = Scaling + (script.ScalePerTexture[j]*Tarray[j]);
												}
											}
											TEMP.GetComponent<INfiniDyGrassField> ().End_scale = Scaling*Random.Range (script.min_scale, script.max_scale);

										}else{
											TEMP.GetComponent<INfiniDyGrassField> ().End_scale = Random.Range (script.min_scale, script.max_scale);
										}


										TEMP.GetComponent<INfiniDyGrassField> ().Max_interact_holder_items = script.Max_interactive_group_members;//Define max number of trees grouped in interactive batcher that opens up. 
										//Increase to lower draw calls, decrease to lower spikes when group is opened for interaction
										TEMP.GetComponent<INfiniDyGrassField> ().Max_trees_per_group = script.Max_static_group_members;
									
										TEMP.GetComponent<INfiniDyGrassField> ().Interactive_tree = script.Interactive;
								//		TEMP.GetComponent<INfiniDyGrassField> ().transform.localScale *= TEMP.GetComponent<INfiniDyGrassField> ().End_scale * script.Collider_scale; //v1.2a
										TEMP.GetComponent<INfiniDyGrassField> ().colliderScale = Vector3.one*script.Collider_scale;
									
										if (script.Override_spread) {
											TEMP.GetComponent<INfiniDyGrassField> ().PosSpread = new Vector2 (Random.Range (script.Min_spread, script.Max_spread), Random.Range (script.Min_spread, script.Max_spread));
										}
										if (script.Override_density) {
											TEMP.GetComponent<INfiniDyGrassField> ().Min_Max_Branching = new Vector2 (script.Min_density, script.Max_density);
										}
										TEMP.GetComponent<INfiniDyGrassField> ().PaintedOnOBJ = hit.transform.gameObject.transform;
										TEMP.GetComponent<INfiniDyGrassField> ().GridOnNormal = script.GridOnNormal;
										TEMP.GetComponent<INfiniDyGrassField> ().max_ray_dist = script.rayCastDist;
										TEMP.GetComponent<INfiniDyGrassField> ().MinAvoidDist = script.MinAvoidDist;
										TEMP.GetComponent<INfiniDyGrassField> ().MinScaleAvoidDist = script.MinScaleAvoidDist;
										TEMP.GetComponent<INfiniDyGrassField> ().InteractionSpeed = script.InteractionSpeed;
										TEMP.GetComponent<INfiniDyGrassField> ().InteractSpeedThres = script.InteractSpeedThres;

										//v1.4
										TEMP.GetComponent<INfiniDyGrassField> ().Interaction_thres = script.Interaction_thres;
										TEMP.GetComponent<INfiniDyGrassField> ().Interaction_offset = script.Interaction_offset;

										TEMP.GetComponent<INfiniDyGrassField> ().LOD_distance = script.LOD_distance;
										TEMP.GetComponent<INfiniDyGrassField>().LOD_distance1 = script.LOD_distance1;
										TEMP.GetComponent<INfiniDyGrassField>().LOD_distance2 = script.LOD_distance2;
										TEMP.GetComponent<INfiniDyGrassField> ().Cutoff_distance = script.Cutoff_distance;

										TEMP.GetComponent<INfiniDyGrassField> ().Tag_based = false;
										TEMP.GetComponent<INfiniDyGrassField> ().GrassManager = script;
										TEMP.GetComponent<INfiniDyGrassField> ().Type = script.Grass_selector+1;
										TEMP.GetComponent<INfiniDyGrassField> ().Start_tree_scale = TEMP.GetComponent<INfiniDyGrassField> ().End_scale/4;
									
										TEMP.GetComponent<INfiniDyGrassField> ().RandomRot = script.RandomRot;
										TEMP.GetComponent<INfiniDyGrassField> ().RandRotMin = script.RandRotMin;
										TEMP.GetComponent<INfiniDyGrassField> ().RandRotMax = script.RandRotMax;

										TEMP.GetComponent<INfiniDyGrassField> ().GroupByObject = script.GroupByObject;
										TEMP.GetComponent<INfiniDyGrassField> ().ParentToObject = script.ParentToObject;
										TEMP.GetComponent<INfiniDyGrassField> ().MoveWithObject = script.MoveWithObject;
										TEMP.GetComponent<INfiniDyGrassField> ().AvoidOwnColl = script.AvoidOwnColl;

										TEMP.transform.parent = script.GrassHolder.transform;
									
										//Add to holder, in order to mass change properties
										script.Grasses.Add (TEMP.GetComponent<INfiniDyGrassField> ());
										script.GrassesType.Add (script.Grass_selector);
									
										TEMP.name = "GrassPatch" + script.Grasses.Count.ToString (); 
									
										TEMP.GetComponent<INfiniDyGrassField> ().Grass_Holder_Index = script.Grasses.Count - 1;//register id in grasses list
									
										Undo.RegisterCreatedObjectUndo(TEMP,"undo grass");
									}
								}
							
								if (erasing & !script.MassErase) {
								
									if (hit.collider != null && hit.collider.gameObject.GetComponent<GrassChopCollider> () != null) {
										ControlCombineChildrenINfiniDyGrass forest_holder = hit.collider.gameObject.GetComponent<GrassChopCollider> ().TreeHandler.Forest_holder.GetComponent<ControlCombineChildrenINfiniDyGrass> ();
										INfiniDyGrassField forest = hit.collider.gameObject.GetComponent<GrassChopCollider> ().TreeHandler;
									
										//UNDO

										forest.Combiner = null;
										forest.Grow_in_Editor = false;
										forest.growth_over = false;
										forest.Registered_Brances.Clear ();//
										//forest.root_tree = null;
										forest.Branch_grew.Clear ();
										forest.Registered_Leaves.Clear ();//
										forest.Registered_Leaves_Rot.Clear ();//
										forest.batching_ended = false;
										forest.Branch_levels.Clear ();
										forest.BranchID_per_level.Clear ();
										//forest.Grass_Holder_Index = 0;
										forest.Grow_level = 0;
										forest.Grow_tree_ended = false;
										forest.Health = forest.Max_Health;
										forest.is_moving = false;
										forest.Leaf_belongs_to_branch.Clear ();
										forest.scaleVectors.Clear ();
										forest.Leaves.Clear ();
										//forest.Tree_Holder_Index = 0;										
										forest.Forest_holder = null;	

										//v1.2
										forest_holder.MakeActive = true;
										//int index = forest.Tree_Holder_Index; 
										
										Undo.DestroyObjectImmediate (forest_holder.Added_items_handles [forest.Tree_Holder_Index].gameObject);

										//DestroyImmediate (forest_holder.Added_items_handles [forest.Tree_Holder_Index].gameObject);
										DestroyImmediate (forest_holder.Added_items [forest.Tree_Holder_Index].gameObject);
									
										forest_holder.Added_items_handles.RemoveAt (forest.Tree_Holder_Index);
										forest_holder.Added_items.RemoveAt (forest.Tree_Holder_Index);
									
										//remove from script
										script.Grasses.RemoveAt (forest.Grass_Holder_Index);
										script.GrassesType.RemoveAt (forest.Grass_Holder_Index);
									
										//adjust ids for items left
										for (int i=0; i<forest_holder.Added_items.Count; i++) {
											forest_holder.Added_items_handles [i].Tree_Holder_Index = i;
										
										}
										for (int i=0; i<script.Grasses.Count; i++) {
											script.Grasses [i].Grass_Holder_Index = i;
										}
									
										forest_holder.Added_item_count -= 1;

										if(forest_holder.Added_item_count == 0){
											DestroyImmediate(forest_holder.gameObject);
										}

										//check if combiners erased
										for(int i=script.DynamicCombiners.Count-1;i>=0;i--){
											if(script.DynamicCombiners[i] == null){
												script.DynamicCombiners.RemoveAt(i);
											}
										}
										for(int i=script.StaticCombiners.Count-1;i>=0;i--){
											if(script.StaticCombiners[i] == null){
												script.StaticCombiners.RemoveAt(i);
											}
										}

									}
								
								}
								}
							}
						
							//MASS ERASE
							if (erasing & script.MassErase) {
								RaycastHit[] hits = Physics.SphereCastAll (ray, script.SphereCastRadius, Mathf.Infinity);
								if (hits != null & hits.Length > 0) {

									bool one_is_outside_view=false;
									for (int j=0; j<hits.Length; j++) {
										if (Vector3.Distance (hits[j].point, Camera.current.transform.position) > script.Editor_view_dist ) {
											one_is_outside_view = true;
										}
									}

									if(!one_is_outside_view){
									for (int j=0; j<hits.Length; j++) {
										RaycastHit hit1 = hits [j];

										if (hit1.collider != null && hit1.collider.gameObject.GetComponent<GrassChopCollider> () != null) {
											ControlCombineChildrenINfiniDyGrass forest_holder = hit1.collider.gameObject.GetComponent<GrassChopCollider> ().TreeHandler.Forest_holder.GetComponent<ControlCombineChildrenINfiniDyGrass> ();
											INfiniDyGrassField forest = hit1.collider.gameObject.GetComponent<GrassChopCollider> ().TreeHandler;
										
											Undo.DestroyObjectImmediate(forest_holder.Added_items_handles [forest.Tree_Holder_Index].gameObject);

											//DestroyImmediate (forest_holder.Added_items_handles [forest.Tree_Holder_Index].gameObject);
											DestroyImmediate (forest_holder.Added_items [forest.Tree_Holder_Index].gameObject);
										
											forest_holder.Added_items_handles.RemoveAt (forest.Tree_Holder_Index);
											forest_holder.Added_items.RemoveAt (forest.Tree_Holder_Index);
										
											//remove from script
											script.Grasses.RemoveAt (forest.Grass_Holder_Index);
											script.GrassesType.RemoveAt (forest.Grass_Holder_Index);
										
											//v1.2
											forest_holder.MakeActive = true;

											//adjust ids for items left
											for (int i=0; i<forest_holder.Added_items.Count; i++) {
												forest_holder.Added_items_handles [i].Tree_Holder_Index = i;
											
											}
											for (int i=0; i<script.Grasses.Count; i++) {
												script.Grasses [i].Grass_Holder_Index = i;
											}
										
											forest_holder.Added_item_count -= 1;

											if(forest_holder.Added_item_count == 0){
												DestroyImmediate(forest_holder.gameObject);
											}

											//check if combiners erased
											for(int i=script.DynamicCombiners.Count-1;i>=0;i--){
												if(script.DynamicCombiners[i] == null){
													script.DynamicCombiners.RemoveAt(i);
												}
											}
											for(int i=script.StaticCombiners.Count-1;i>=0;i--){
												if(script.StaticCombiners[i] == null){
													script.StaticCombiners.RemoveAt(i);
												}
											}
										}
									}
									}
								}
							}
						
							Selection.activeGameObject = script.gameObject;
							Selection.activeObject = script.gameObject;
							Selection.activeTransform = script.transform;					
						
						} else {
							if (Camera.current != null) {
								Camera_last_pos = Camera.current.transform.position; 
								Camera_last_rot = Camera.current.transform.rotation;
							}
						}
					}
				
				}// END GRASS PAINTING
			
				///////////////////////////////// ROCK PAINTING /////////////////////////////////
				if (script.Rock_painting & !Application.isPlaying) {
				
					if (script.RocksHolder == null) {
						GameObject RocksHolder = new GameObject ();
						RocksHolder.transform.parent = script.transform;
						RocksHolder.name = "Rocks Holder";
						script.RocksHolder = RocksHolder;
					}
				
					//ERASE GRASS
					if (Input.GetKeyDown (KeyCode.LeftShift)) {
						//Debug.Log("erasing");
					} else if (cur.type == EventType.KeyDown & cur.keyCode == (KeyCode.LeftControl)) {
						//rotate camera
					} else {
					
						bool looking = false;
						script.Looking = false;
						if (cur.modifiers > 0) {
							if ((cur.modifiers) == EventModifiers.Control) {
							
								looking = true;
								script.Looking = true;
							}
						}
					
					
						if (!looking &
							((cur.type == EventType.MouseDrag && cur.button == 1 & Vector3.Distance (Keep_last_mouse_pos, cur.mousePosition) > script.min_grass_patch_dist) | (cur.type == EventType.MouseDown && cur.button == 1))) {
							Keep_last_mouse_pos = cur.mousePosition;
							Ray ray = HandleUtility.GUIPointToWorldRay (cur.mousePosition);
							RaycastHit hit = new RaycastHit ();
							if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {							
								bool erasing = false;
								if (cur.modifiers > 0) {
									if ((cur.modifiers) == EventModifiers.Shift) {
										//Debug.Log("shift press");
										erasing = true;
									}
								}
							
								if ( (Terrain.activeTerrain != null && hit.collider.gameObject != null && hit.collider.gameObject == Terrain.activeTerrain.gameObject) 
								    | (script.PaintonTag && hit.collider.gameObject != null && hit.collider.gameObject.tag == "PPaint") ) { //v1.1
								
									if (erasing) {
									
									} else {	
									
										Undo.RecordObject (script, "undo paint");
									
									
										GameObject TEMP = PrefabUtility.InstantiatePrefab (script.RockPrefabs [script.Grass_selector]) as GameObject;
										TEMP.transform.position = hit.point;	
									
										TEMP.transform.Rotate (Vector3.up, Random.Range (-180, 180));	
									
										TEMP.transform.localScale = TEMP.transform.localScale * Random.Range (script.min_scale, script.max_scale);
									
										TEMP.transform.parent = script.RocksHolder.transform;
									
										Undo.RegisterCreatedObjectUndo (TEMP, "destroy spheres");
									}
								}
							
								if (erasing) {	
									if (hit.collider.transform.parent != null) {
										if (hit.collider.transform.parent.gameObject.name == "Rocks Holder") {
											DestroyImmediate (hit.collider.gameObject);						
										}
									}
								}							
							}
							Selection.activeGameObject = script.gameObject;
							Selection.activeObject = script.gameObject;
							Selection.activeTransform = script.transform;
						}
					}				
				}// END ROCK PAINTING
			
			
			
			
				///////////////////////////////// FENCE PAINTING /////////////////////////////////
				if (script.Fence_painting & !Application.isPlaying) {
				
					if (script.FencesHolder == null) {
						GameObject FencesHolder = new GameObject ();
						FencesHolder.transform.parent = script.transform;
						FencesHolder.name = "Fences Holder";
						script.FencesHolder = FencesHolder;
					}
				
					//check for erased
					for (int i=script.Fences.Count-1; i>=0; i--) {
						if (script.Fences [i] == null) {
							script.Fences.RemoveAt (i);
						}
					}
				
					//ERASE FENCE
					if (Input.GetKeyDown (KeyCode.LeftShift)) {
						Debug.Log ("erasing");
					} else if (cur.type == EventType.KeyDown & cur.keyCode == (KeyCode.LeftControl)) {
						//rotate camera
					} else {
					
					
						if (script.Fence_poles.Count > 0) {
							if (script.Fence_poles [script.Fence_poles.Count - 1] != null) {
								//						Debug.DrawLine (script.Fence_poles [script.Fence_poles.Count - 1].position, script.Fence_poles [script.Fence_poles.Count - 1].position + Vector3.up * 40, Color.red, 5);
							}
						}
					
						if (cur.type == EventType.MouseDown && cur.button == 1 && script.Fence_poles_tmp.Count > 0) {
							//
							for (int i = 0; i<script.Fence_poles_tmp.Count; i++) {
							
								script.Fence_poles.Add (script.Fence_poles_tmp [i]);
								script.Fence_poles_midA.Add (script.Fence_poles_midA_tmp [i]);//
							
							}
							script.Fence_poles_tmp.Clear ();
							script.Fence_poles_midA_tmp.Clear ();//
						
						} else {
						
							Ray ray = HandleUtility.GUIPointToWorldRay (cur.mousePosition);
						
							if (!Fence_start_added) {
								if (cur.type == EventType.MouseDown && cur.button == 1) {
									Fence_start_added = true;
								}
							} else {
							
								RaycastHit hit = new RaycastHit ();
								if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {							
									bool erasing = false;
									if (cur.modifiers > 0) {
										if ((cur.modifiers) == EventModifiers.Shift) {
										
											erasing = true;
										}
									}
								
									if ( (Terrain.activeTerrain != null && hit.collider.gameObject != null && hit.collider.gameObject == Terrain.activeTerrain.gameObject) 
									    | (script.PaintonTag && hit.collider.gameObject != null && hit.collider.gameObject.tag == "PPaint") ) { //v1.1
									
										if (erasing) {
										
										} else {		
										
											//check for erased
											for (int i=script.Fences.Count-1; i>=0; i--) {
												if (script.Fences [i] == null) {
													script.Fences.RemoveAt (i);
												}
											}
										
											Undo.RecordObject (script, "undo paint");
										
											if (script.Fence_poles.Count == 0) {
											
												GameObject TEMP = PrefabUtility.InstantiatePrefab (script.FencePrefabs [script.Grass_selector]) as GameObject;
												TEMP.transform.position = hit.point;											
											
												TEMP.transform.localScale = TEMP.transform.localScale * script.fence_scale;
											
												GameObject Fence = new GameObject ();
												Fence.name = "Fence" + script.Fences.Count.ToString ();
												Fence.transform.parent = script.FencesHolder.transform;
												script.Fences.Add (Fence.transform);
												TEMP.transform.parent = script.Fences [script.Fences.Count - 1];										
											
												TEMP.transform.up = hit.normal;
											
												Undo.RegisterCreatedObjectUndo (TEMP, "destroy spheres");
											
												script.Fence_poles.Add (TEMP.transform);										
											
											} else {
											
												//create and destroy on the fly, register with mouse up										
												if (Fence_bulk) {
													if (script.Fence_poles [script.Fence_poles.Count - 1] != null) {
														float dist = Vector3.Distance (script.Fence_poles [script.Fence_poles.Count - 1].position, hit.point); 
														int fences = (int)(dist / script.min_grass_patch_dist);
														if (dist > script.min_grass_patch_dist) {
															// Keep_last_mouse_pos = cur.mousePosition;
														
															//decide how many times the distance includes fence size													
														
															if (script.Fence_poles_tmp.Count == fences) {
																//move only
																for (int i = script.Fence_poles_tmp.Count; i<fences; i++) {
																
																	script.Fence_poles_tmp [i].position = (hit.point - script.Fence_poles [script.Fence_poles.Count - 1].position).normalized * script.min_grass_patch_dist * (i + 1);				
																
																	script.Fence_poles_midA_tmp [i].position = (hit.point - script.Fence_poles_midA [script.Fence_poles_midA.Count - 1].position).normalized * script.min_grass_patch_dist * (i + 1);
																
																}
															
															} else if (script.Fence_poles_tmp.Count > fences) { //distance got shorter, must remove
																for (int i = script.Fence_poles_tmp.Count-1; i>(script.Fence_poles_tmp.Count - fences); i--) {
																	if (i < script.Fence_poles_tmp.Count & script.Fence_poles_tmp.Count > 0) {
																		DestroyImmediate (script.Fence_poles_tmp [i].gameObject);														
																		script.Fence_poles_tmp.RemoveAt (i);
																	
																		DestroyImmediate (script.Fence_poles_midA_tmp [i].gameObject);														
																		script.Fence_poles_midA_tmp.RemoveAt (i);
																	}
																}
															
															} else {
																for (int i = script.Fence_poles_tmp.Count; i<(fences); i++) {
																
																	GameObject TEMP = PrefabUtility.InstantiatePrefab (script.FencePrefabs [script.Grass_selector]) as GameObject;
																	TEMP.transform.position = hit.point;											
																	TEMP.transform.parent = script.Fences [script.Fences.Count - 1];//script.FencesHolder.transform;	
																
																	TEMP.transform.up = hit.normal;
																	if (i - 1 < script.Fence_poles_tmp.Count & i - 1 >= 0) {
																		TEMP.transform.forward = hit.point - script.Fence_poles_tmp [i - 1].position;
																	}
																	Undo.RegisterCreatedObjectUndo (TEMP, "destroy spheres");											
																	script.Fence_poles_tmp.Add (TEMP.transform);															
																
																	TEMP = PrefabUtility.InstantiatePrefab (script.FenceMidPrefabs [script.Grass_selector]) as GameObject;
																	TEMP.transform.position = hit.point + new Vector3 (0, 2, 0);											
																	TEMP.transform.parent = script.Fences [script.Fences.Count - 1];//script.FencesHolder.transform;
																	//float dist1 = 1.6f;
																	if (i - 1 < script.Fence_poles_tmp.Count & i - 1 >= 0) {
																		TEMP.transform.up = hit.point - script.Fence_poles_tmp [i - 1].position;
																		//dist1 = (hit.point - script.Fence_poles_tmp [i - 1].position).magnitude / 5;
																	}															 
																
																	script.Fence_poles_midA_tmp.Add (TEMP.transform);
																}
															
															}
														} else {
														
															for (int i = script.Fence_poles_tmp.Count-1; i>=0; i--) {
																if (i < script.Fence_poles_tmp.Count) {
																	DestroyImmediate (script.Fence_poles_tmp [i].gameObject);														
																	script.Fence_poles_tmp.RemoveAt (i);
																
																	DestroyImmediate (script.Fence_poles_midA_tmp [i].gameObject);														
																	script.Fence_poles_midA_tmp.RemoveAt (i);
																}
															}
														}
													}
												} else {///////////////////////// IF ONE BY ONE
													if (script.Fence_poles.Count > 0 && script.Fence_poles [script.Fence_poles.Count - 1] != null) {
														float dist = Vector3.Distance (script.Fence_poles [script.Fence_poles.Count - 1].position, hit.point); 
														//int fences = (int)(dist / script.min_grass_patch_dist);
													
														if (dist > script.min_grass_patch_dist) {
														
														
															float Mid_width_scale = 2;
															float Mid_height_scale = 2;
															float Fence_part_real_height = 4.75f;//the actual height of the object (not inspector scale, real height of prefab item in world space), divide to get final scale
															float Fence_part_real_width = 0.25f;
															float MidA_height_percent = 0.63f;
														
															float Vert_posA = Fence_part_real_height * MidA_height_percent * script.fence_scale;
														
															//display next fence
															if (script.Fence_poles_tmp.Count == 1) {
															
																script.Fence_poles_tmp [0].position = hit.point;
																script.Fence_poles_tmp [0].up = hit.normal;														
																script.Fence_poles_tmp [0].forward = hit.point - script.Fence_poles [script.Fence_poles.Count - 1].position;
															
															
																script.Fence_poles_midA_tmp [0].position = hit.point + script.Fence_poles_tmp [0].up * Vert_posA  
																	- script.Fence_poles_tmp [0].forward * script.Fence_poles_tmp [0].localScale.z * Fence_part_real_width;
															
																script.Fence_poles_midA_tmp [0].up = (script.Fence_poles [script.Fence_poles.Count - 1].position + script.Fence_poles [script.Fence_poles.Count - 1].up * Vert_posA) - (hit.point + script.Fence_poles_tmp [0].up * Vert_posA);
															
																script.Fence_poles_midA_tmp [0].forward = -script.Fence_poles_tmp [0].up;
															
																script.Fence_poles_midA_tmp [0].rotation = Quaternion.LookRotation (script.Fence_poles_tmp [0].up,
															                                                                   (script.Fence_poles [script.Fence_poles.Count - 1].position + script.Fence_poles [script.Fence_poles.Count - 1].up * Vert_posA) - (script.Fence_poles_tmp [0].position + script.Fence_poles_tmp [0].up * Vert_posA));
															
																Vector3 ScaleInit = script.Fence_poles_midA_tmp [0].localScale;
															
															
																float distA = Vector3.Distance (script.Fence_poles [script.Fence_poles.Count - 1].position + Vert_posA * script.Fence_poles [script.Fence_poles.Count - 1].up, 
															                                script.Fence_poles_tmp [0].position + Vert_posA * script.Fence_poles_tmp [0].up);
																script.Fence_poles_midA_tmp [0].localScale = new Vector3 (ScaleInit.x, distA / Fence_part_real_height, ScaleInit.z);	
															
															} else {
															
																GameObject TEMP = PrefabUtility.InstantiatePrefab (script.FencePrefabs [script.Grass_selector]) as GameObject;
																TEMP.transform.position = hit.point;											
																TEMP.transform.parent = script.Fences [script.Fences.Count - 1];//script.FencesHolder.transform;	
															
																TEMP.transform.up = hit.normal;
																TEMP.transform.localScale = TEMP.transform.localScale * script.fence_scale;
															
																TEMP.transform.forward = hit.point - script.Fence_poles [0].position;
															
																Undo.RegisterCreatedObjectUndo (TEMP, "destroy fence");	
																string name = "Fence " + script.Fence_poles.Count;
																TEMP.name = name;
																script.Fence_poles_tmp.Add (TEMP.transform);														
															
																TEMP = PrefabUtility.InstantiatePrefab (script.FenceMidPrefabs [script.Grass_selector]) as GameObject;
																TEMP.transform.position = hit.point + new Vector3 (0, Vert_posA, 0);											
																TEMP.transform.parent = script.Fences [script.Fences.Count - 1];//script.FencesHolder.transform;
																//float dist1 = 1.6f;
															
																TEMP.transform.up = hit.point - script.Fence_poles_tmp [0].position;
																//dist1 = (hit.point - script.Fence_poles_tmp [0].position).magnitude / 5;
															
																TEMP.transform.localScale = new Vector3 (TEMP.transform.localScale.x / Mid_width_scale, TEMP.transform.localScale.y, TEMP.transform.localScale.z / Mid_height_scale) * script.fence_scale;
															
																Undo.RegisterCreatedObjectUndo (TEMP, "destroy fence mid");	
																script.Fence_poles_midA_tmp.Add (TEMP.transform);
															}
														
														} else {
														
															for (int i = script.Fence_poles_tmp.Count-1; i>=0; i--) {
																if (i < script.Fence_poles_tmp.Count) {
																	DestroyImmediate (script.Fence_poles_tmp [i].gameObject);														
																	script.Fence_poles_tmp.RemoveAt (i);															
																
																	DestroyImmediate (script.Fence_poles_midA_tmp [i].gameObject);														
																	script.Fence_poles_midA_tmp.RemoveAt (i);
																}
															}
														}
													}
												
												}//END FENCE ADD ANDLE
											
											}
										}
									}
								
									if (erasing) {								
									
									}
								
								}
								Selection.activeGameObject = script.gameObject;
								Selection.activeObject = script.gameObject;
								Selection.activeTransform = script.transform;
							}
						}
					
					}				
				}// END FENCE PAINTING
			else {
				
					//Destroy left over parts
					for (int i = script.Fence_poles_tmp.Count-1; i>=0; i--) {
						if (i < script.Fence_poles_tmp.Count) {
							DestroyImmediate (script.Fence_poles_tmp [i].gameObject);														
							script.Fence_poles_tmp.RemoveAt (i);
						
							//
							DestroyImmediate (script.Fence_poles_midA_tmp [i].gameObject);														
							script.Fence_poles_midA_tmp.RemoveAt (i);
						}
					}
					//clear everything
					script.Fence_poles.Clear ();
					script.Fence_poles_midA.Clear ();//
				
					script.Fence_poles_tmp.Clear ();
					script.Fence_poles_midA_tmp.Clear ();//
				
					Fence_start_added = false;
				
				}
			
				//sceneView.Repaint ();
			}//END if ungrown
		}// END
		
		public int Last_drawn_number = 0;//how many drawn, so they get erased
		
		
		public override void  OnInspectorGUI() {
			
			
			serializedObject.Update ();
			
			EditorGUILayout.PropertyField(GrassMaterials,true);
			EditorGUILayout.PropertyField(ExtraGrassMaterials,true);//v1.4
			
			EditorGUILayout.PropertyField(GrassPrefabs, true);
			EditorGUILayout.PropertyField(GrassPrefabsIcons, true);//v1.3
			
			EditorGUILayout.PropertyField(RockPrefabs, true);
			EditorGUILayout.PropertyField(FencePrefabs, true);
			EditorGUILayout.PropertyField(FenceMidPrefabs, true);
			
			//FILL WITH PRESETS and PASS TO SCRIPT
			if (script.GrassMaterials.Count == 0  | script.GrassPrefabs.Count < 6) {
				script.GrassMaterials.Add(grass_material);
				script.GrassMaterials.Add(grass_vertex_material);
				script.GrassMaterials.Add(grass_flower_material);
				script.GrassMaterials.Add(grass_wheet_material);
				script.GrassMaterials.Add(grass_vertex2_material);
				script.GrassMaterials.Add(grass_vertexHEAVY_material);
				
				script.GrassMaterials.Add(grass_whites_material);
				script.GrassMaterials.Add(grass_curved_vertex_material);
				script.GrassMaterials.Add(grass_low_grass_material);
				script.GrassMaterials.Add(grass_vines_material);
			}
			if (script.GrassPrefabs.Count == 0 | script.GrassPrefabs.Count < 6 ) {
				
				script.GrassPrefabs.Add(grass_prefab as GameObject);
				script.GrassPrefabs.Add(grass_vertex_prefab as GameObject);
				script.GrassPrefabs.Add(grass_flower_prefab as GameObject);
				script.GrassPrefabs.Add(grass_wheet_prefab as GameObject);
				script.GrassPrefabs.Add(grass_vertex2_prefab as GameObject);
				script.GrassPrefabs.Add(grass_vertexHEAVY_prefab as GameObject);
				//script.GrassPrefabs.Add(grass_prefab);
				script.GrassPrefabs.Add(grass_whites_prefab as GameObject);
				script.GrassPrefabs.Add(grass_curved_vertex_prefab as GameObject);
				script.GrassPrefabs.Add(grass_low_grass_prefab as GameObject);
				script.GrassPrefabs.Add(grass_vines_prefab as GameObject);
			}

			//v1.3
			if (script.GrassPrefabsIcons.Count == 0 | script.GrassPrefabsIcons.Count < 6 ) {
				
				script.GrassPrefabsIcons.Add(GrassICON1);
				script.GrassPrefabsIcons.Add(GrassICON2);
				script.GrassPrefabsIcons.Add(GrassICON3);
				script.GrassPrefabsIcons.Add(GrassICON4);
				script.GrassPrefabsIcons.Add(GrassICON5);
				script.GrassPrefabsIcons.Add(GrassICON6);
				//script.GrassPrefabs.Add(grass_prefab);
				script.GrassPrefabsIcons.Add(GrassICON7);
				script.GrassPrefabsIcons.Add(GrassICON8);
				script.GrassPrefabsIcons.Add(GrassICON9);
				script.GrassPrefabsIcons.Add(GrassICON10);
			}
			
			if (script.RockPrefabs.Count == 0) {
				script.RockPrefabs.Add(rock_prefab as GameObject);
				script.RockPrefabs.Add(rock_prefab1 as GameObject);
				script.RockPrefabs.Add(rock_prefab2 as GameObject);
			}
			if (script.FencePrefabs.Count == 0) {
				script.FencePrefabs.Add(fence_prefab as GameObject);
				script.FencePrefabs.Add(fence_prefab1 as GameObject);
				
				
			}
			if (script.FenceMidPrefabs.Count == 0) {
				script.FenceMidPrefabs.Add(fence_mid_prefab as GameObject);
				script.FenceMidPrefabs.Add(fence_mid_prefab1 as GameObject);
			}
			
			//MASS PLACEMENT v1.4
			if (script.BrushSettings.Count == 0) {
				//define defaults once
				for(int i=0;i<10;i++){
					InfiniGRASS_BrushSettings Setting1 = new InfiniGRASS_BrushSettings();

					Define_init_settings(Setting1,i);

					script.BrushSettings.Add(Setting1);
				}
			}
			//check if more are added and init
			if (script.BrushSettings.Count < script.GrassPrefabs.Count) {
				int dif = (script.GrassPrefabs.Count - script.BrushSettings.Count);
				for (int i=0;i<dif;i++){
					InfiniGRASS_BrushSettings Setting1 = new InfiniGRASS_BrushSettings();
					
					Define_init_settings(Setting1,i+script.BrushSettings.Count+1);
					
					script.BrushSettings.Add(Setting1);
				}
			}
			
			////////////////////////////////////////////////////////////// PAINT OPTIONS /////
			EditorGUILayout.BeginVertical(GUILayout.MaxWidth(180.0f));

			GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));

			//v1.3
			if (script.UnGrown) {
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Grow gradually (play mode)", GUILayout.MaxWidth (200f), GUILayout.MinWidth (200f));
				script.GradualGrowth = EditorGUILayout.Toggle (script.GradualGrowth);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				//GUILayout.Label ("Grow near player", GUILayout.MaxWidth (150f));
				//script.UseDistFromPlayer = EditorGUILayout.Toggle (script.UseDistFromPlayer);
				GUILayout.Label ("Recreate Grass", GUILayout.MaxWidth (200f), GUILayout.MinWidth (200f));
				script.GradualRecreate = EditorGUILayout.Toggle (script.GradualRecreate);

				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.Slider (EnableDist, 80*(script.WorldScale/20), 1000.0f, new GUIContent ("Regrow below this distance"), GUILayout.MinWidth (400f));
				EditorGUILayout.EndHorizontal ();

			}



			if (!script.UnGrown) {
				//if(script.AdaptOnTerrain){
				EditorGUILayout.PropertyField (BrushTypePerSplat, new GUIContent ("Brush Type Per Splat"), true, GUILayout.MaxWidth (250f));
				EditorGUILayout.PropertyField (SubTypePerSplat, new GUIContent ("Sub Type Per Splat"), true, GUILayout.MaxWidth (250f));

				EditorGUILayout.IntSlider (MassPlantDensity, 2, 20*(int)(20/script.WorldScale), new GUIContent ("Density of Mass Grow"));

				//v1.4
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Use brush settings", GUILayout.MaxWidth (200f), GUILayout.MinWidth (200f));
				script.GrabSettingsMassPlace = EditorGUILayout.Toggle (script.GrabSettingsMassPlace);				
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.PropertyField (BrushSettings, new GUIContent ("Brush Settings"), true, GUILayout.MaxWidth (250f));

				//for(int i=0;i<script.BrushTypePerSplat.Count;i++){
				//	if(script.ScalePerTexture[i] <= 0){
				//		script.ScalePerTexture[i] = 1;
				//	}
				//}
				//}
				EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("Save brush settings"), GUILayout.Width (150))) {
					//if(script.Grass_selector > script.BrushSettings.Count-1){
						//if not defined, add to list
					//}

					InfiniGRASS_BrushSettings Setting1 = new InfiniGRASS_BrushSettings();
					
					SaveBrush(Setting1);
					
					script.BrushSettings[script.Grass_selector] = Setting1;

				}
				if (GUILayout.Button (new GUIContent ("Load brush settings"), GUILayout.Width (150))) {

					InfiniGRASS_BrushSettings Setting1 = new InfiniGRASS_BrushSettings();

					Setting1 = script.BrushSettings[script.Grass_selector];

					LoadBrush(Setting1);					

				}
				EditorGUILayout.EndHorizontal ();

				GUILayout.Label ("Mass paint zone");
			
				EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("Add Corner A"), GUILayout.Width (120))) {
					if (script.MassPlantAreaCornerA == null) {
						GameObject MassPlantAreaCornerA = new GameObject ();
						script.MassPlantAreaCornerA = MassPlantAreaCornerA.transform;

						MassPlantAreaCornerA.name = "Mass paint Corner A";
					} else {
						Debug.Log ("First corner exists");
					}
				}			
				script.MassPlantAreaCornerA = EditorGUILayout.ObjectField (script.MassPlantAreaCornerA, typeof(Transform), true, GUILayout.MaxWidth (180.0f)) as Transform;
				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("Add Corner B"), GUILayout.Width (120))) {
					if (script.MassPlantAreaCornerB == null) {
						GameObject MassPlantAreaCornerB = new GameObject ();
						script.MassPlantAreaCornerB = MassPlantAreaCornerB.transform;
					
						MassPlantAreaCornerB.name = "Mass paint Corner B";
					} else {
						Debug.Log ("Second corner exists");
					}
				}			
				script.MassPlantAreaCornerB = EditorGUILayout.ObjectField (script.MassPlantAreaCornerB, typeof(Transform), true, GUILayout.MaxWidth (180.0f)) as Transform;
				EditorGUILayout.EndHorizontal ();

				if(script.MassPlantAreaCornerB != null && script.MassPlantAreaCornerA != null && script.MassPlantAreaCornerB.position.x < script.MassPlantAreaCornerA.position.x){
					script.MassPlantAreaCornerB.position = script.MassPlantAreaCornerA.position + new Vector3(5,0,0);
					Debug.Log("Second corner x must be higher then first corner x");
				}
				if(script.MassPlantAreaCornerB != null && script.MassPlantAreaCornerA != null && script.MassPlantAreaCornerB.position.z < script.MassPlantAreaCornerA.position.z){
					script.MassPlantAreaCornerB.position = script.MassPlantAreaCornerA.position + new Vector3(0,0,5);
					Debug.Log("Second corner z must be higher then first corner z");
				}

				//v1.3 - mass placement
				if (GUILayout.Button (new GUIContent ("Mass Place"), GUILayout.Width (300))) {
					if (script.MassPlantAreaCornerA != null && script.MassPlantAreaCornerB != null) {
						//enable gradual growth
						//	script.GradualGrowth = true;
						//	script.UseDistFromPlayer = true;
						//	script.EnableDist = 500;

						//ungrow and disable all current grass
					//	script.UnGrown = true;

//						if (script.Grasses.Count > 0  & 1==0) {
//						
//							Undo.ClearUndo (script);
//							script.CleanUp = false;//stop clean up, so grass is not removed on regrow
//						
//							//ControlCombineChildrenINfiniDyGrass forest_holder = hit.collider.gameObject.GetComponent<GrassChopCollider> ().TreeHandler.Forest_holder.GetComponent<ControlCombineChildrenINfiniDyGrass> ();
//							//INfiniDyGrassField forest = hit.collider.gameObject.GetComponent<GrassChopCollider> ().TreeHandler;
//						
//							for (int j=0; j<script.Grasses.Count; j++) {
//							
//								ControlCombineChildrenINfiniDyGrass forest_holder = script.Grasses [j].Combiner;
//								INfiniDyGrassField forest = script.Grasses [j];
//								//DestroyImmediate (forest_holder.Added_items_handles [forest.Tree_Holder_Index].gameObject);
//								DestroyImmediate (forest_holder.Added_items [forest.Tree_Holder_Index].gameObject);
//							
//								forest_holder.Added_items_handles.RemoveAt (forest.Tree_Holder_Index);
//								forest_holder.Added_items.RemoveAt (forest.Tree_Holder_Index);
//							
//								//v1.2
//								forest_holder.Full_reset ();
//							
//								//EXTRAS
//								GameObject LEAF_POOL = new GameObject ();
//								LEAF_POOL.transform.parent = forest.transform;
//								forest.Leaf_pool = LEAF_POOL;
//							
//								forest.Combiner = null;
//								forest.Grow_in_Editor = false;
//								forest.growth_over = false;
//								forest.Registered_Brances.Clear ();//
//								//forest.root_tree = null;
//								forest.Branch_grew.Clear ();
//								forest.Registered_Leaves.Clear ();//
//								forest.Registered_Leaves_Rot.Clear ();//
//								forest.batching_ended = false;
//								forest.Branch_levels.Clear ();
//								forest.BranchID_per_level.Clear ();
//								//forest.Grass_Holder_Index = 0;
//								forest.Grow_level = 0;
//								forest.Grow_tree_ended = false;
//								forest.Health = forest.Max_Health;
//								forest.is_moving = false;
//								forest.Leaf_belongs_to_branch.Clear ();
//								forest.scaleVectors.Clear ();
//								forest.Leaves.Clear ();
//								forest.Tree_Holder_Index = 0;
//								forest.Grow_tree = false;
//								forest.rotation_over = false;
//							
//								forest.Forest_holder = null;						
//
//							
//								//adjust ids for items left
//								for (int i=0; i<forest_holder.Added_items.Count; i++) {
//									forest_holder.Added_items_handles [i].Tree_Holder_Index = i;
//								
//								}
//							
//								script.UnGrown = true;
//							
//								forest_holder.Added_item_count -= 1;																				
//							
//								//v1.3
//								script.Grasses [j].gameObject.SetActive (false);
//							
//								//Debug.Log(script.StaticCombiners.Count);
//							}
//						
//							for (int i=script.DynamicCombiners.Count-1; i>=0; i--) {
//								if (script.DynamicCombiners [i].gameObject != null) {
//									DestroyImmediate (script.DynamicCombiners [i].gameObject);
//								}
//							}
//							for (int i=script.StaticCombiners.Count-1; i>=0; i--) {
//								if (script.StaticCombiners [i].gameObject != null) {
//									DestroyImmediate (script.StaticCombiners [i].gameObject);
//								}
//							}
//							//v1.2 - remove these in all cases
//							script.DynamicCombiners.Clear ();
//							script.StaticCombiners.Clear ();
//						
//						}

						//MASS PLACE GRASS - based on corners and density
						float distA = (float)script.MassPlantDensity / 100f;
						int SepX = (int)(Mathf.Abs ((script.MassPlantAreaCornerA.position.x - script.MassPlantAreaCornerB.position.x) * (distA)));
						int SepZ = (int)(Mathf.Abs ((script.MassPlantAreaCornerA.position.z - script.MassPlantAreaCornerB.position.z) * (distA)));

						Debug.Log ("SepX=" + SepX + " SepZ=" + SepZ + " Dist=" + distA);

						for (int i=0; i<SepX; i++) {
							for (int j=0; j<SepZ; j++) {
								Vector3 startP = new Vector3 (script.MassPlantAreaCornerA.position.x, script.MassPlantAreaCornerA.position.y, script.MassPlantAreaCornerA.position.z);
								RaycastHit hit = new RaycastHit ();
								Ray ray = new Ray (startP + new Vector3 (i * (1 / distA), 0, j * (1 / distA)), -Vector3.up);

								///// PAINT

								if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {														
								
									//v1.2 - dont paint if out of editor view
									//Debug.Log(Vector3.Distance (hit.point, Camera.current.transform.position));
									//if (Vector3.Distance (hit.point, Camera.current.transform.position) < script.Editor_view_dist ) {
									
									
									bool is_Terrain = false;
									if ((Terrain.activeTerrain != null && hit.collider.gameObject != null && hit.collider.gameObject == Terrain.activeTerrain.gameObject)) {
										is_Terrain = true;
									}
									
									float Scaling = 1;
									bool enter = true;
									//if (script.AdaptOnTerrain & is_Terrain) { //v1.4
									if (is_Terrain) {//v1.4
										int Xpos = (int)(((hit.point.x - script.Tpos.x) * script.Tdata.alphamapWidth / script.Tdata.size.x));
										int Zpos = (int)(((hit.point.z - script.Tpos.z) * script.Tdata.alphamapHeight / script.Tdata.size.z));
										float[,,] splats = script.Tdata.GetAlphamaps (Xpos, Zpos, 1, 1);
										float[] Tarray = new float[splats.GetUpperBound (2) + 1];
										for (int k =0; k<Tarray.Length; k++) {
											Tarray [k] = splats [0, 0, k];
										}

										if (script.AdaptOnTerrain){
											Scaling = 0;
											for (int k =0; k<Tarray.Length; k++) {
												if (k > script.ScalePerTexture.Count - 1) {
													Scaling = Scaling + (1 * Tarray [k]);
												} else {
													Scaling = Scaling + (script.ScalePerTexture [k] * Tarray [k]);
												}
											}
										}

										if (script.BrushTypePerSplat.Count > 0) {
											//find dominant splat and do accordingly
											float max_value = Tarray [0];
											int max_index = 0;
											for (int k=0; k<Tarray.Length; k++) {
												if (Tarray [k] > max_value) {
													max_value = Tarray [k];
													max_index = k;
												}
											}
											float max_value2 = Tarray [0];
											int max_index2 = 0;
											for (int k=0; k<Tarray.Length; k++) {
												if (Tarray [k] > max_value2 & k != max_index) {
													max_value2 = Tarray [k];
													max_index2 = k;
												}
											}

											if (max_index < script.BrushTypePerSplat.Count) { // check if list has a defined value
												if (script.BrushTypePerSplat [max_index] < 0) {
													enter = false;
												} else {
													script.Grass_selector = script.BrushTypePerSplat [max_index];

													if (max_index2 < script.SubTypePerSplat.Count && max_value2 > 0.002f) {
														int rand = Random.Range (0, 18);
														if (rand != 1) {
															//Debug.Log(max_index2);
															if (script.SubTypePerSplat [max_index2] < 0) {
																//enter = false;
															} else {
																script.Grass_selector = script.SubTypePerSplat [max_index2];
															}
														}
													}
												}
											}
										}

									}


									if (((enter && is_Terrain) || (script.PaintonTag && hit.collider.gameObject != null && hit.collider.gameObject.tag == "PPaint"))) {//v1.1	
										
										
										//if (erasing) {									
											
										//} else 
										if(!script.GrabSettingsMassPlace | (script.BrushSettings.Count < (script.Grass_selector+1)))
										{											
											Object To_instantiate = grass_prefab;								
											
											To_instantiate = script.GrassPrefabs [script.Grass_selector];
											
											GameObject TEMP = PrefabUtility.InstantiatePrefab (To_instantiate) as GameObject;
											TEMP.transform.position = hit.point;
											
											TEMP.GetComponent<INfiniDyGrassField> ().Intial_Up_Vector = hit.normal;
											TEMP.GetComponent<INfiniDyGrassField> ().Grow_in_Editor = true;//v1.3 - disable growth
											
											//v1.1 - terrain adapt
											if (script.AdaptOnTerrain & is_Terrain) {
//												int Xpos = (int)(((hit.point.x - script.Tpos.x)*script.Tdata.alphamapWidth/script.Tdata.size.x));
//												int Zpos = (int)(((hit.point.z - script.Tpos.z)*script.Tdata.alphamapHeight/script.Tdata.size.z));
//												float[,,] splats = script.Tdata.GetAlphamaps(Xpos,Zpos,1,1);
//												float[] Tarray = new float[splats.GetUpperBound(2)+1];
//												for(int k =0;k<Tarray.Length;k++){
//													Tarray[k] = splats[0,0,k];
//												}
//												Scaling = 0;
//												for(int k =0;k<Tarray.Length;k++){
//													if(k > script.ScalePerTexture.Count-1){
//														Scaling = Scaling + (1*Tarray[k]);
//													}else{
//														Scaling = Scaling + (script.ScalePerTexture[k]*Tarray[k]);
//													}
//												}
												TEMP.GetComponent<INfiniDyGrassField> ().End_scale = Scaling * Random.Range (script.min_scale, script.max_scale);
												
											} else {
												TEMP.GetComponent<INfiniDyGrassField> ().End_scale = Random.Range (script.min_scale, script.max_scale);
											}
											
											
											TEMP.GetComponent<INfiniDyGrassField> ().Max_interact_holder_items = script.Max_interactive_group_members;//Define max number of trees grouped in interactive batcher that opens up. 
											//Increase to lower draw calls, decrease to lower spikes when group is opened for interaction
											TEMP.GetComponent<INfiniDyGrassField> ().Max_trees_per_group = script.Max_static_group_members;
											
											TEMP.GetComponent<INfiniDyGrassField> ().Interactive_tree = script.Interactive;
											//TEMP.GetComponent<INfiniDyGrassField> ().transform.localScale *= TEMP.GetComponent<INfiniDyGrassField> ().End_scale * script.Collider_scale; //1.2a
											TEMP.GetComponent<INfiniDyGrassField> ().colliderScale = Vector3.one * script.Collider_scale;
											
											if (script.Override_spread) {
												TEMP.GetComponent<INfiniDyGrassField> ().PosSpread = new Vector2 (Random.Range (script.Min_spread, script.Max_spread), Random.Range (script.Min_spread, script.Max_spread));
											}
											if (script.Override_density) {
												TEMP.GetComponent<INfiniDyGrassField> ().Min_Max_Branching = new Vector2 (script.Min_density, script.Max_density);
											}
											TEMP.GetComponent<INfiniDyGrassField> ().PaintedOnOBJ = hit.transform.gameObject.transform;
											TEMP.GetComponent<INfiniDyGrassField> ().GridOnNormal = script.GridOnNormal;
											TEMP.GetComponent<INfiniDyGrassField> ().max_ray_dist = script.rayCastDist;
											TEMP.GetComponent<INfiniDyGrassField> ().MinAvoidDist = script.MinAvoidDist;
											TEMP.GetComponent<INfiniDyGrassField> ().MinScaleAvoidDist = script.MinScaleAvoidDist;
											TEMP.GetComponent<INfiniDyGrassField> ().InteractionSpeed = script.InteractionSpeed;
											TEMP.GetComponent<INfiniDyGrassField> ().InteractSpeedThres = script.InteractSpeedThres;

											//v1.4
											TEMP.GetComponent<INfiniDyGrassField> ().Interaction_thres = script.Interaction_thres;
											TEMP.GetComponent<INfiniDyGrassField> ().Interaction_offset = script.Interaction_offset;
											
											TEMP.GetComponent<INfiniDyGrassField> ().LOD_distance = script.LOD_distance;
											TEMP.GetComponent<INfiniDyGrassField> ().LOD_distance1 = script.LOD_distance1;
											TEMP.GetComponent<INfiniDyGrassField> ().LOD_distance2 = script.LOD_distance2;
											TEMP.GetComponent<INfiniDyGrassField> ().Cutoff_distance = script.Cutoff_distance;
											
											TEMP.GetComponent<INfiniDyGrassField> ().Tag_based = false;
											TEMP.GetComponent<INfiniDyGrassField> ().GrassManager = script;
											TEMP.GetComponent<INfiniDyGrassField> ().Type = script.Grass_selector + 1;
											TEMP.GetComponent<INfiniDyGrassField> ().Start_tree_scale = TEMP.GetComponent<INfiniDyGrassField> ().End_scale / 4;
											
											TEMP.GetComponent<INfiniDyGrassField> ().RandomRot = script.RandomRot;
											TEMP.GetComponent<INfiniDyGrassField> ().RandRotMin = script.RandRotMin;
											TEMP.GetComponent<INfiniDyGrassField> ().RandRotMax = script.RandRotMax;
											
											TEMP.GetComponent<INfiniDyGrassField> ().GroupByObject = script.GroupByObject;
											TEMP.GetComponent<INfiniDyGrassField> ().ParentToObject = script.ParentToObject;
											TEMP.GetComponent<INfiniDyGrassField> ().MoveWithObject = script.MoveWithObject;
											TEMP.GetComponent<INfiniDyGrassField> ().AvoidOwnColl = script.AvoidOwnColl;
											
											TEMP.transform.parent = script.GrassHolder.transform;
											
											//Add to holder, in order to mass change properties
											script.Grasses.Add (TEMP.GetComponent<INfiniDyGrassField> ());
											script.GrassesType.Add (script.Grass_selector);
											
											TEMP.name = "GrassPatch" + script.Grasses.Count.ToString (); 
											
											TEMP.GetComponent<INfiniDyGrassField> ().Grass_Holder_Index = script.Grasses.Count - 1;//register id in grasses list
											
											Undo.RegisterCreatedObjectUndo (TEMP, "undo grass");

										}else{

											//v1.4 - use settings
											Object To_instantiate = grass_prefab;								
											
											To_instantiate = script.GrassPrefabs [script.Grass_selector];
											
											GameObject TEMP = PrefabUtility.InstantiatePrefab (To_instantiate) as GameObject;
											TEMP.transform.position = hit.point;
											
											TEMP.GetComponent<INfiniDyGrassField> ().Intial_Up_Vector = hit.normal;
											TEMP.GetComponent<INfiniDyGrassField> ().Grow_in_Editor = true;//v1.3 - disable growth
											
											//v1.1 - terrain adapt
											//if (script.AdaptOnTerrain & is_Terrain) {
											TEMP.GetComponent<INfiniDyGrassField> ().End_scale = Scaling * Random.Range (script.BrushSettings[script.Grass_selector].min_scale, script.BrushSettings[script.Grass_selector].max_scale);												
											//} else {
											//	TEMP.GetComponent<INfiniDyGrassField> ().End_scale = Random.Range (script.min_scale, script.max_scale);
											//}											
											
											TEMP.GetComponent<INfiniDyGrassField> ().Max_interact_holder_items = script.BrushSettings[script.Grass_selector].Max_interactive_group_members;//Define max number of trees grouped in interactive batcher that opens up. 
											//Increase to lower draw calls, decrease to lower spikes when group is opened for interaction
											TEMP.GetComponent<INfiniDyGrassField> ().Max_trees_per_group = script.BrushSettings[script.Grass_selector].Max_static_group_members;
											
											TEMP.GetComponent<INfiniDyGrassField> ().Interactive_tree = script.BrushSettings[script.Grass_selector].Interactive;
											//TEMP.GetComponent<INfiniDyGrassField> ().transform.localScale *= TEMP.GetComponent<INfiniDyGrassField> ().End_scale * script.Collider_scale; //1.2a
											TEMP.GetComponent<INfiniDyGrassField> ().colliderScale = Vector3.one * script.BrushSettings[script.Grass_selector].Collider_scale;
											
											if (script.BrushSettings[script.Grass_selector].Override_spread) {
												TEMP.GetComponent<INfiniDyGrassField> ().PosSpread = new Vector2 (Random.Range (script.BrushSettings[script.Grass_selector].Min_spread, script.BrushSettings[script.Grass_selector].Max_spread), Random.Range (script.BrushSettings[script.Grass_selector].Min_spread, script.BrushSettings[script.Grass_selector].Max_spread));
											}
											if (script.BrushSettings[script.Grass_selector].Override_density) {
												TEMP.GetComponent<INfiniDyGrassField> ().Min_Max_Branching = new Vector2 (script.BrushSettings[script.Grass_selector].Min_density, script.BrushSettings[script.Grass_selector].Max_density);
											}
											TEMP.GetComponent<INfiniDyGrassField> ().PaintedOnOBJ = hit.transform.gameObject.transform;
											TEMP.GetComponent<INfiniDyGrassField> ().GridOnNormal = script.BrushSettings[script.Grass_selector].GridOnNormal;
											TEMP.GetComponent<INfiniDyGrassField> ().max_ray_dist = script.BrushSettings[script.Grass_selector].rayCastDist;
											TEMP.GetComponent<INfiniDyGrassField> ().MinAvoidDist = script.BrushSettings[script.Grass_selector].MinAvoidDist;
											TEMP.GetComponent<INfiniDyGrassField> ().MinScaleAvoidDist = script.BrushSettings[script.Grass_selector].MinScaleAvoidDist;
											TEMP.GetComponent<INfiniDyGrassField> ().InteractionSpeed = script.BrushSettings[script.Grass_selector].InteractionSpeed;
											TEMP.GetComponent<INfiniDyGrassField> ().InteractSpeedThres = script.BrushSettings[script.Grass_selector].InteractSpeedThres;

											//v1.4
											TEMP.GetComponent<INfiniDyGrassField> ().Interaction_thres = script.BrushSettings[script.Grass_selector].Interaction_thres;
											TEMP.GetComponent<INfiniDyGrassField> ().Interaction_offset = script.BrushSettings[script.Grass_selector].Interaction_offset;
											
											TEMP.GetComponent<INfiniDyGrassField> ().LOD_distance = script.BrushSettings[script.Grass_selector].LOD_distance;
											TEMP.GetComponent<INfiniDyGrassField> ().LOD_distance1 = script.BrushSettings[script.Grass_selector].LOD_distance1;
											TEMP.GetComponent<INfiniDyGrassField> ().LOD_distance2 = script.BrushSettings[script.Grass_selector].LOD_distance2;
											TEMP.GetComponent<INfiniDyGrassField> ().Cutoff_distance = script.BrushSettings[script.Grass_selector].Cutoff_distance;
											
											TEMP.GetComponent<INfiniDyGrassField> ().Tag_based = false;
											TEMP.GetComponent<INfiniDyGrassField> ().GrassManager = script;
											TEMP.GetComponent<INfiniDyGrassField> ().Type = script.Grass_selector + 1;
											TEMP.GetComponent<INfiniDyGrassField> ().Start_tree_scale = TEMP.GetComponent<INfiniDyGrassField> ().End_scale / 4;
											
											TEMP.GetComponent<INfiniDyGrassField> ().RandomRot = script.BrushSettings[script.Grass_selector].RandomRot;
											TEMP.GetComponent<INfiniDyGrassField> ().RandRotMin = script.BrushSettings[script.Grass_selector].RandRotMin;
											TEMP.GetComponent<INfiniDyGrassField> ().RandRotMax = script.BrushSettings[script.Grass_selector].RandRotMax;
											
											TEMP.GetComponent<INfiniDyGrassField> ().GroupByObject = script.BrushSettings[script.Grass_selector].GroupByObject;
											TEMP.GetComponent<INfiniDyGrassField> ().ParentToObject = script.BrushSettings[script.Grass_selector].ParentToObject;
											TEMP.GetComponent<INfiniDyGrassField> ().MoveWithObject = script.BrushSettings[script.Grass_selector].MoveWithObject;
											TEMP.GetComponent<INfiniDyGrassField> ().AvoidOwnColl = script.BrushSettings[script.Grass_selector].AvoidOwnColl;
											
											TEMP.transform.parent = script.GrassHolder.transform;
											
											//Add to holder, in order to mass change properties
											script.Grasses.Add (TEMP.GetComponent<INfiniDyGrassField> ());
											script.GrassesType.Add (script.Grass_selector);
											
											TEMP.name = "GrassPatch" + script.Grasses.Count.ToString (); 
											
											TEMP.GetComponent<INfiniDyGrassField> ().Grass_Holder_Index = script.Grasses.Count - 1;//register id in grasses list
											
											Undo.RegisterCreatedObjectUndo (TEMP, "undo grass");
										}
									}
									

									//}
								}// END RAYCAST

								///// END PAINT

								///// UNGROW

//								if (script.Grasses.Count > 0 & 1==0) {
//								
//									Undo.ClearUndo (script);
//									script.CleanUp = false;//stop clean up, so grass is not removed on regrow
//								
//									//ControlCombineChildrenINfiniDyGrass forest_holder = hit.collider.gameObject.GetComponent<GrassChopCollider> ().TreeHandler.Forest_holder.GetComponent<ControlCombineChildrenINfiniDyGrass> ();
//									//INfiniDyGrassField forest = hit.collider.gameObject.GetComponent<GrassChopCollider> ().TreeHandler;
//								
//									//		for (int j=0; j<script.Grasses.Count; j++) {
//									
//									ControlCombineChildrenINfiniDyGrass forest_holder = script.Grasses [script.Grasses.Count - 1].Combiner;
//									INfiniDyGrassField forest = script.Grasses [script.Grasses.Count - 1];
//									//DestroyImmediate (forest_holder.Added_items_handles [forest.Tree_Holder_Index].gameObject);
//									//			DestroyImmediate (forest_holder.Added_items [forest.Tree_Holder_Index].gameObject);
//									
//									//			forest_holder.Added_items_handles.RemoveAt (forest.Tree_Holder_Index);
//									//			forest_holder.Added_items.RemoveAt (forest.Tree_Holder_Index);
//									
//									//v1.2
//									//			forest_holder.Full_reset();
//									
//									//EXTRAS
//									GameObject LEAF_POOL = new GameObject ();
//									LEAF_POOL.transform.parent = forest.transform;
//									forest.Leaf_pool = LEAF_POOL;
//									
//									forest.Combiner = null;
//									forest.Grow_in_Editor = false;
//									forest.growth_over = false;
//									forest.Registered_Brances.Clear ();//
//									//forest.root_tree = null;
//									forest.Branch_grew.Clear ();
//									forest.Registered_Leaves.Clear ();//
//									forest.Registered_Leaves_Rot.Clear ();//
//									forest.batching_ended = false;
//									forest.Branch_levels.Clear ();
//									forest.BranchID_per_level.Clear ();
//									//forest.Grass_Holder_Index = 0;
//									forest.Grow_level = 0;
//									forest.Grow_tree_ended = false;
//									forest.Health = forest.Max_Health;
//									forest.is_moving = false;
//									forest.Leaf_belongs_to_branch.Clear ();
//									forest.scaleVectors.Clear ();
//									forest.Leaves.Clear ();
//									forest.Tree_Holder_Index = 0;
//									forest.Grow_tree = false;
//									forest.rotation_over = false;
//									
//									//		forest.Forest_holder = null;
//									
//									//		forest_holder.MakeActive = false;
//									//forest.Trees.Clear();
//									//forest.Wind_strength
//									
//									//remove from script
//									//	script.Grasses.RemoveAt (forest.Grass_Holder_Index);
//									//	script.GrassesType.RemoveAt (forest.Grass_Holder_Index);
//									
//									//adjust ids for items left
//									//		for (int k=0; k<forest_holder.Added_items.Count; k++) {
//									//			forest_holder.Added_items_handles [k].Tree_Holder_Index = i;
//										
//									//		}									
//									
//									script.UnGrown = true;
//									
//									//		forest_holder.Added_item_count -= 1;																
//									
//									//v1.3
//									script.Grasses [script.Grasses.Count - 1].gameObject.SetActive (false);
//									
//									//Debug.Log(script.StaticCombiners.Count);
//									//		}
//								
//									for (int k=script.DynamicCombiners.Count-1; k>=0; k--) {
//										if (script.DynamicCombiners [k].gameObject != null) {
//											DestroyImmediate (script.DynamicCombiners [k].gameObject);
//										}
//									}
//									for (int k=script.StaticCombiners.Count-1; k>=0; k--) {
//										if (script.StaticCombiners [k].gameObject != null) {
//											DestroyImmediate (script.StaticCombiners [k].gameObject);
//										}
//									}
//									//v1.2 - remove these in all cases
//									script.DynamicCombiners.Clear ();
//									script.StaticCombiners.Clear ();
//								
//								}

								//// END UNGROW

							}//END GRID Y
						}//END GRID X

					} else {
						Debug.Log ("Please define the corners of the area to be planted using two Transforms in the TransformCornerA and B fields");
					}
				}
			}//END IF !UNGROWN check

			GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));

			//v1.2a
			bool enter_sizing = false;//signal entry if scale changes
			EditorGUILayout.PropertyField(WorldScale,new GUIContent("World scale"),true, GUILayout.MaxWidth (250f));
			if (script.WorldScale <= 0) {
				script.WorldScale=0.05f;
			}
			if (script.WorldScale != script.prev_WorldScale) {			
				script.prev_WorldScale = script.WorldScale;	
				enter_sizing = true;
			}

			///////////////////// PREVIEW
			if (script.UnGrown & !Application.isPlaying) {
				if (GUILayout.Button (new GUIContent ("Regrow in editor"), GUILayout.Width (120))) {
					for (int j=0; j<script.Grasses.Count; j++) {

						//v1.3
						script.Grasses[j].gameObject.SetActive(true);

						INfiniDyGrassField forest = script.Grasses [j];
						forest.Grow_in_Editor = true;
						script.UnGrown = false;
						forest.rotation_over = false;

						//v1.4 - rescale options
						forest.ScaleMin_grabbed = script.ScaleMin_grabbed;						
						forest.Start_tree_scale = forest.Start_tree_scale * script.Scale_grabbed;
						forest.End_scale = forest.End_scale * script.Scale_grabbed;

						forest.Start();
						forest.Update ();
						script.Grasses [j].Combiner.MakeActive = true;
					}
				}
			}

			EditorGUILayout.BeginHorizontal ();
			//GUILayout.Label ("Scale grabbed grass", GUILayout.MaxWidth (180f));
			EditorGUILayout.Slider (Scale_grabbed, 0.1f, 2.0f, new GUIContent ("Scale grabbed-ungrown grass"), GUILayout.MinWidth (400f));
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.Slider (ScaleMin_grabbed, 0.1f, 2.0f, new GUIContent ("Grass rand. min scale"), GUILayout.MinWidth (400f));
			EditorGUILayout.EndHorizontal ();

			if (!script.UnGrown | Application.isPlaying) {

				if (!Application.isPlaying) {
					if (GUILayout.Button (new GUIContent ("Ungrow in editor"), GUILayout.Width (120))) {
						if (script.Grasses.Count > 0) {

							Undo.ClearUndo(script);
							script.CleanUp = false;//stop clean up, so grass is not removed on regrow

							//ControlCombineChildrenINfiniDyGrass forest_holder = hit.collider.gameObject.GetComponent<GrassChopCollider> ().TreeHandler.Forest_holder.GetComponent<ControlCombineChildrenINfiniDyGrass> ();
							//INfiniDyGrassField forest = hit.collider.gameObject.GetComponent<GrassChopCollider> ().TreeHandler;
					
							for (int j=0; j<script.Grasses.Count; j++) {

								ControlCombineChildrenINfiniDyGrass forest_holder = script.Grasses [j].Combiner;
								INfiniDyGrassField forest = script.Grasses [j];
								//DestroyImmediate (forest_holder.Added_items_handles [forest.Tree_Holder_Index].gameObject);
								DestroyImmediate (forest_holder.Added_items [forest.Tree_Holder_Index].gameObject);
						
								forest_holder.Added_items_handles.RemoveAt (forest.Tree_Holder_Index);
								forest_holder.Added_items.RemoveAt (forest.Tree_Holder_Index);

								//v1.2
								forest_holder.Full_reset();

								//EXTRAS
								GameObject LEAF_POOL = new GameObject ();
								LEAF_POOL.transform.parent = forest.transform;
								forest.Leaf_pool = LEAF_POOL;

								forest.Combiner = null;
								forest.Grow_in_Editor = false;
								forest.growth_over = false;
								forest.Registered_Brances.Clear ();//
								//forest.root_tree = null;
								forest.Branch_grew.Clear ();
								forest.Registered_Leaves.Clear ();//
								forest.Registered_Leaves_Rot.Clear ();//
								forest.batching_ended = false;
								forest.Branch_levels.Clear ();
								forest.BranchID_per_level.Clear ();
								//forest.Grass_Holder_Index = 0;
								forest.Grow_level = 0;
								forest.Grow_tree_ended = false;
								forest.Health = forest.Max_Health;
								forest.is_moving = false;
								forest.Leaf_belongs_to_branch.Clear ();
								forest.scaleVectors.Clear ();
								forest.Leaves.Clear ();
								forest.Tree_Holder_Index = 0;
								forest.Grow_tree = false;
								forest.rotation_over = false;

								forest.Forest_holder = null;

						//		forest_holder.MakeActive = false;
								//forest.Trees.Clear();
								//forest.Wind_strength

								//remove from script
								//	script.Grasses.RemoveAt (forest.Grass_Holder_Index);
								//	script.GrassesType.RemoveAt (forest.Grass_Holder_Index);
						
								//adjust ids for items left
								for (int i=0; i<forest_holder.Added_items.Count; i++) {
									forest_holder.Added_items_handles [i].Tree_Holder_Index = i;
							
								}
								//	for (int i=0; i<script.Grasses.Count; i++) {
								//		script.Grasses [i].Grass_Holder_Index = i;
								//	}

								script.UnGrown = true;
						
								forest_holder.Added_item_count -= 1;

								//check if combiners erased
//								for(int i=script.DynamicCombiners.Count-1;i>=0;i--){
//									if(script.DynamicCombiners[i] == null){
//										script.DynamicCombiners.RemoveAt(i);
//									}
//								}
//								for(int i=script.StaticCombiners.Count-1;i>=0;i--){
//									if(script.StaticCombiners[i] == null){
//										script.StaticCombiners.RemoveAt(i);
//									}
//								}


								//v1.3
								script.Grasses[j].gameObject.SetActive(false);

								//Debug.Log(script.StaticCombiners.Count);
							}

							for(int i=script.DynamicCombiners.Count-1;i>=0;i--){
								if(script.DynamicCombiners[i].gameObject != null){
									DestroyImmediate(script.DynamicCombiners[i].gameObject);
								}
							}
							for(int i=script.StaticCombiners.Count-1;i>=0;i--){
								if(script.StaticCombiners[i].gameObject != null){
									DestroyImmediate(script.StaticCombiners[i].gameObject);
								}
							}
							//v1.2 - remove these in all cases
							script.DynamicCombiners.Clear();
							script.StaticCombiners.Clear();

						}
					}
				}

				/// v1.4 - grab play mode grass - copy from play mode (disabled parent) and insert here
				GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Grass parent from play mode", GUILayout.MaxWidth (180f));
				script.play_mode_grass_holder = EditorGUILayout.ObjectField (script.play_mode_grass_holder, typeof(Transform), true, GUILayout.MaxWidth (180.0f)) as Transform;
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Grass parent in editor", GUILayout.MaxWidth (180f));
				script.Editor_holder = EditorGUILayout.ObjectField (script.Editor_holder, typeof(Transform), true, GUILayout.MaxWidth (180.0f)) as Transform;
				EditorGUILayout.EndHorizontal ();


				if(!script.UnGrown){
					if (GUILayout.Button (new GUIContent ("Grab grass"), GUILayout.Width (150))) {
						INfiniDyGrassField[] GrassFields = (script.play_mode_grass_holder as Transform).GetComponentsInChildren<INfiniDyGrassField>(true);
						//for(int i=0;i< GrassFields.Length;i++){
						for(int i=GrassFields.Length-1;i>=0;i--){

							INfiniDyGrassField forest = GrassFields[i];

							forest.gameObject.SetActive(false);

							forest.PaintedOnOBJ = script.Editor_holder;
							forest.player = null;

							//reset the script
							GameObject LEAF_POOL = new GameObject ();
							LEAF_POOL.transform.parent = forest.transform;
							forest.Leaf_pool = LEAF_POOL;
							
							forest.Combiner = null;
							forest.Grow_in_Editor = false;
							forest.growth_over = false;
							forest.Registered_Brances.Clear ();//
							//forest.root_tree = null;
							forest.Branch_grew.Clear ();
							forest.Registered_Leaves.Clear ();//
							forest.Registered_Leaves_Rot.Clear ();//
							forest.batching_ended = false;
							forest.Branch_levels.Clear ();
							forest.BranchID_per_level.Clear ();
							//forest.Grass_Holder_Index = 0;
							forest.Grow_level = 0;
							forest.Grow_tree_ended = false;
							forest.Health = forest.Max_Health;
							forest.is_moving = false;
							forest.Leaf_belongs_to_branch.Clear ();
							forest.scaleVectors.Clear ();
							forest.Leaves.Clear ();
							forest.Tree_Holder_Index = 0;
							forest.Grow_tree = false;
							forest.rotation_over = false;
							
							forest.Forest_holder = null;

							//REGISTER GRASS
							script.Grasses.Add(forest);
							forest.transform.parent = script.GrassHolder.transform;
							forest.Grass_Holder_Index = script.Grasses.Count-1;
							forest.Restart = true;
							forest.ParentWhileGrowing = false;
						
							forest.ScaleMin_grabbed = script.ScaleMin_grabbed;

							forest.Start_tree_scale = forest.Start_tree_scale * script.Scale_grabbed;
							forest.End_scale = forest.End_scale * script.Scale_grabbed;

							//move to grass holder and add to grasses in Manager

							//v1.3
							//script.Grasses[j].gameObject.SetActive(true);
							forest.gameObject.SetActive(true);
							
							//INfiniDyGrassField forest = script.Grasses [j];
							forest.Grow_in_Editor = true;
							//script.UnGrown = false;
							forest.rotation_over = false;
							forest.Start();
							forest.Update ();
							//script.Grasses [j].Combiner.MakeActive = true;
							forest.Combiner.MakeActive = true;
						}
					}
				}


				if (script.ActivateHelp) {
					EditorGUILayout.HelpBox ("Steps to grab real time grass. a) paint grass in play mode on an item using the 'Move with object' option, b) disable the item and right click-copy it," +
					                         " c) exit play mode & paste to the scene hierarchy root, d) insert the pasted object in 'Grass parent from play mode' box & the equivelent item in the editor where grass will be parented," +
					                         " e) Press 'Grab grass' button.", MessageType.None);
				}

				GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));

				/////

				//v1.4 - tag based player
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Tag based player", GUILayout.MaxWidth (150f));
				script.Tag_based_player = EditorGUILayout.Toggle (script.Tag_based_player);
				EditorGUILayout.EndHorizontal ();

				//v1.4f - recheck for player
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Update player", GUILayout.MaxWidth (150f));
				script.recheckPlayer = EditorGUILayout.Toggle (script.recheckPlayer);
				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.BeginHorizontal ();
				//GUILayout.Label ("Player tag", GUILayout.MaxWidth (150f));
				//script.Tag_based_player = EditorGUILayout.Toggle (script.Tag_based_player);
				script.Player_tag = EditorGUILayout.TagField(Player_tag.stringValue, GUILayout.MaxWidth (250f));
				EditorGUILayout.EndHorizontal ();

				if (!Application.isPlaying) {

					if(script.Tag_based_player != script.Tag_based_player_prev | script.Player_tag != script.Player_tag_prev){
						script.Tag_based_player_prev = script.Tag_based_player;
						script.Player_tag_prev = script.Player_tag;
						script.player = null;

						if (script.Tag_based_player) {
							
							script.player = GameObject.FindGameObjectWithTag (script.Player_tag);
							
						} else {
							
							if (Camera.main != null) {
								script.player = Camera.main.gameObject;
							}
							
						}

						for(int i=0;i<script.DynamicCombiners.Count;i++){
							if(script.DynamicCombiners[i] != null){
								script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Tag_based_player = script.Tag_based_player;
								script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Player_tag = script.Player_tag;
								script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().player = null;
								//script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().LateUpdate();
							}
						}
						for(int i=0;i<script.StaticCombiners.Count;i++){
							if(script.StaticCombiners[i] != null){
								script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Tag_based_player = script.Tag_based_player;
								script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Player_tag = script.Player_tag;
								script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().player = null;
								//script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().LateUpdate();
							}
						}
					}
					//redefine state on grasses

					for(int i=0;i<script.Grasses.Count;i++){
						script.Grasses[i].Tag_based_player = script.Tag_based_player;
						script.Grasses[i].Player_tag = script.Player_tag;
					}
					for(int i=0;i<script.DynamicCombiners.Count;i++){
						if(script.DynamicCombiners[i] != null){
							script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Tag_based_player = script.Tag_based_player;
							script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Player_tag = script.Player_tag;
							//script.DynamicCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().player = null;
						}
					}
					for(int i=0;i<script.StaticCombiners.Count;i++){
						if(script.StaticCombiners[i] != null){
							script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Tag_based_player = script.Tag_based_player;
							script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().Player_tag = script.Player_tag;
							//script.StaticCombiners[i].GetComponent<ControlCombineChildrenINfiniDyGrass>().player = null;
						}
					}
				}
				//}

				GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));

				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Enable real time paint", GUILayout.MaxWidth (150f));
				script.Enable_real_time_paint = EditorGUILayout.Toggle (script.Enable_real_time_paint);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Enable real time erase", GUILayout.MaxWidth (150f));
				script.Enable_real_time_erase = EditorGUILayout.Toggle (script.Enable_real_time_erase);
				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Paint on 'PPaint' tagged", GUILayout.MaxWidth (150f));
				script.PaintonTag = EditorGUILayout.Toggle (script.PaintonTag);
				EditorGUILayout.EndHorizontal ();
			
				EditorGUILayout.Slider (Editor_view_dist, 10.0f, 2000.0f, new GUIContent ("Editor view distance"));
			
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Toggle Gizmos", GUILayout.MaxWidth (150f));
				script.GizmosOn = EditorGUILayout.Toggle (script.GizmosOn);
				EditorGUILayout.EndHorizontal ();
			
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Toggle Colliders", GUILayout.MaxWidth (150f));
				//script.EditorCollidersOn = EditorGUILayout.Toggle(script.EditorCollidersOn);
				string CollidersEditorOn = "Turn off Editor Colliders";
				if (!script.EditorCollidersOn) {
					CollidersEditorOn = "Turn on Editor Colliders";
				}
				if (GUILayout.Button (new GUIContent (CollidersEditorOn), GUILayout.Width (150))) {
					if (script.EditorCollidersOn) {
						script.EditorCollidersOn = false;
						script.DisableColliders ();
					} else {
						script.EditorCollidersOn = true;
						script.EnableColliders ();
					}
				}
				EditorGUILayout.EndHorizontal ();
			
				GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Toggle Wind", GUILayout.MaxWidth (150f));
				script.ShaderWind = EditorGUILayout.Toggle (script.ShaderWind, new GUILayoutOption[]{GUILayout.MaxWidth (60f)});
				EditorGUILayout.EndHorizontal ();
			
			
			
				GUILayout.Label ("Windzone");
			
				EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("Add windzone"), GUILayout.Width (120))) {
					if (script.windzone == null) {
						GameObject WindZone = new GameObject ();
						script.windzone = WindZone.transform;
						WindZone.AddComponent<WindZone> ();
						script.windzoneScript = script.windzone.GetComponent<WindZone> ();
						WindZone.name = "InfiniGRASS windzone";
					} else {
						Debug.Log ("Windzone exists");
					}
				}
				//Handle external zone
				if(script.windzone != null && script.windzoneScript==null){
					script.windzoneScript = script.windzone.GetComponent<WindZone>();
				}
			
				//Wind_Zone = EditorGUILayout.ObjectField (Wind_Zone, typeof(Transform), true, GUILayout.MaxWidth (180.0f)) as Transform;
				//script.windzone = Wind_Zone;
				script.windzone = EditorGUILayout.ObjectField (script.windzone, typeof(Transform), true, GUILayout.MaxWidth (180.0f)) as Transform;
			
				EditorGUILayout.EndHorizontal ();						
			
			
				EditorGUILayout.Slider (AmplifyWind, 0f, 6f, new GUIContent ("Wind modifier"));
				EditorGUILayout.Slider (WindTurbulence, 0f, 3f, new GUIContent ("Wind turbulence"));
			
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Preview wind in editor", GUILayout.MaxWidth (150f));
				script.Preview_wind = EditorGUILayout.Toggle (script.Preview_wind);
				EditorGUILayout.EndHorizontal ();
			
				GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));
			
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Toggle Grass Tint", GUILayout.MaxWidth (150f));
				script.TintGrass = EditorGUILayout.Toggle (script.TintGrass);
				EditorGUILayout.EndHorizontal ();
			
				EditorGUILayout.Slider (TintPower, 0f, 6f, new GUIContent ("Tint power"));
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Tint Color");
				script.tintColor = EditorGUILayout.ColorField (script.tintColor);
				EditorGUILayout.EndHorizontal ();

				//v1.1
				EditorGUILayout.Slider (TintFrequency, 0.01f, 0.5f, new GUIContent ("Tint frequency"));
			
				EditorGUILayout.Slider (SpecularPower, 0f, 6f, new GUIContent ("Specular power"));
			
				/////////////////////////////////////////////////////////////////////////////// GRASS SETUP /////////////////////////////////
			
				GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	
			
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Activate Help", GUILayout.MaxWidth (150f));
				script.ActivateHelp = EditorGUILayout.Toggle (script.ActivateHelp);
				EditorGUILayout.EndHorizontal ();
				if (script.ActivateHelp) {
					EditorGUILayout.HelpBox ("Press 'paint grass' to start planting while the script is active with the right mouse button. Press again to stop.  Hold left Shift to erase grass." +
						"Hold left Ctrl to stop painting and rotate camera view.  ", MessageType.None);
					EditorGUILayout.HelpBox ("Press 'Paint Fence' and click on the place the fence must start.  Stop creation by pressing 'Paint Fence' button while active.", MessageType.None);
					EditorGUILayout.HelpBox ("The system optionally requires 5 tags (INfiniDyForestInter, INfiniDyForest, INfiniDyTreeRoot, INfiniDyTree and Player), they only need to be defined +" +
						"if grass is grown without this GrassManager. Use PPaint tag for painting on objects besides Unity Terrain", MessageType.None);
					EditorGUILayout.HelpBox ("The LOD distances should not be changed during gameplay, because it can break the batching system", MessageType.None);
				}
			
				GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	
			
				EditorGUILayout.BeginHorizontal ();
			
				Color savedColor = GUI.color;
			
				string PaintTEXT = "Paint grass";
				if (script.Grass_painting) {
					PaintTEXT = "Painting";
					GUI.color = Color.cyan;
					if (script.Erasing) {
						PaintTEXT = "Erasing";
						GUI.color = Color.red;
					}
					if (script.Looking) {
						PaintTEXT = "Camera rot";
						GUI.color = Color.grey;
					}
				
				}
			
				if (GUILayout.Button (new GUIContent (PaintTEXT), GUILayout.Width (120))) {			
				
					if (script.Grass_painting) {
						script.Grass_painting = false;
					} else {
						script.Grass_painting = true;
						script.Rock_painting = false;
						script.Fence_painting = false;
					}
				}
			
				GUI.color = savedColor;
				PaintTEXT = "Paint rocks";
				if (script.Rock_painting) {
					PaintTEXT = "Painting";
					GUI.color = Color.gray;
				}
				if (GUILayout.Button (new GUIContent (PaintTEXT), GUILayout.Width (120))) {			
				
					if (script.Rock_painting) {
						script.Rock_painting = false;
					} else {
						script.Rock_painting = true;
						script.Grass_painting = false;
						script.Fence_painting = false;
					}
				}
				GUI.color = savedColor;
				PaintTEXT = "Paint fence";
				if (script.Fence_painting) {
					PaintTEXT = "Construct";
					GUI.color = Color.magenta;
				}
				if (GUILayout.Button (new GUIContent (PaintTEXT), GUILayout.Width (120))) {			
				
					if (script.Fence_painting) {
						script.Fence_painting = false;
					} else {
						script.Fence_painting = true;
						script.Grass_painting = false;
						script.Rock_painting = false;
					}
				}
			
				EditorGUILayout.EndHorizontal ();
			
			
				if (script.Grass_painting | script.Rock_painting | script.Fence_painting) {
				
					GUI.color = savedColor;
				
					if (script.Grass_painting) {
					
						if (script.Grass_painting) {
							EditorGUILayout.BeginHorizontal ();
							int count5 = 0;
							for(int i=0;i<script.GrassPrefabsIcons.Count;i++){

								if (GUILayout.Button (script.GrassPrefabsIcons[i], GUILayout.Width (80), GUILayout.Height (80))) {						
									script.Grass_selector = i;					
								}

								count5++;
								if(count5 > 4){
									count5=0;
									EditorGUILayout.EndHorizontal ();
									EditorGUILayout.BeginHorizontal ();
								}

							}

//							if (GUILayout.Button (GrassICON1, GUILayout.Width (80), GUILayout.Height (80))) {						
//								script.Grass_selector = 0;					
//							}
//							if (GUILayout.Button (GrassICON2, GUILayout.Width (80), GUILayout.Height (80))) {						
//								script.Grass_selector = 1;					
//							}
//							if (GUILayout.Button (GrassICON3, GUILayout.Width (80), GUILayout.Height (80))) {						
//								script.Grass_selector = 2;					
//							}
//							if (GUILayout.Button (GrassICON4, GUILayout.Width (80), GUILayout.Height (80))) {						
//								script.Grass_selector = 3;					
//							}
//							if (GUILayout.Button (GrassICON5, GUILayout.Width (80), GUILayout.Height (80))) {						
//								script.Grass_selector = 4;					
//							}
//							EditorGUILayout.EndHorizontal ();
//							EditorGUILayout.BeginHorizontal ();
//						
//							if (GUILayout.Button (GrassICON6, GUILayout.Width (80), GUILayout.Height (80))) {						
//								script.Grass_selector = 5;					
//							}
//							if (GUILayout.Button (GrassICON7, GUILayout.Width (80), GUILayout.Height (80))) {						
//								script.Grass_selector = 6;					
//							}
//							if (GUILayout.Button (GrassICON8, GUILayout.Width (80), GUILayout.Height (80))) {						
//								script.Grass_selector = 7;					
//							}
//							if (GUILayout.Button (GrassICON9, GUILayout.Width (80), GUILayout.Height (80))) {						
//								script.Grass_selector = 8;					
//							}
//							if (GUILayout.Button (GrassICON10, GUILayout.Width (80), GUILayout.Height (80))) {						
//								script.Grass_selector = 9;					
//							}
							EditorGUILayout.EndHorizontal ();
						}
					
						////////////////////////////////////////////////// DEFINE INTERACTIVE					
					
					
						GUILayout.Label ("----------- Select paint type -----------", EditorStyles.boldLabel);	
					
						if (script.Grass_painting) {
						
							if (script.Grass_selector > script.GrassPrefabs.Count - 1) {
								script.Grass_selector = 0;
							}
						
							string Grass_type = "Grass";
							if((script.prev_brush != script.Grass_selector | enter_sizing) & !Application.isPlaying){	//v1.2a
							//if(script.prev_brush != script.Grass_selector & !Application.isPlaying){

								//v1.2a
								enter_sizing = false;

								script.prev_brush = script.Grass_selector;
							//Grass
							if (script.Grass_selector == 0) {
								script.Min_density = 1;
								script.Max_density = 4;
								//script.SpecularPower = 4;
								script.Min_spread = 7;
								script.Max_spread = 9;
								script.min_scale = 0.4f;
								script.max_scale = 0.6f;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = false;
							}
							//Vertex grass
							if (script.Grass_selector == 1) {
								script.min_scale = 0.4f;
								script.max_scale = 0.8f;
								script.Min_density = 2.0f;
								script.Max_density = 3.0f;
								script.Min_spread = 7;
								script.Max_spread = 9;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = false;
							}
							//Red flowers
							if (script.Grass_selector== 2) {
								script.min_scale = 0.8f;
								script.max_scale = 0.9f;
								script.Min_density = 1.0f;
								script.Max_density = 1.0f;
								script.Min_spread = 7;
								script.Max_spread = 10;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = true;
							}
							//Wheet
							if (script.Grass_selector == 3) {
								script.min_scale = 1.0f;
								script.max_scale = 1.5f;
								script.Min_density = 1.0f;
								script.Max_density = 1.0f;
								script.Min_spread = 15;
								script.Max_spread = 20;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = false;
							}
							//Detailed vertex
							if (script.Grass_selector == 4) {
								script.min_scale = 1.0f;
								script.max_scale = 1.2f;
								script.Min_density = 1.0f;
								script.Max_density = 3.0f;
								script.Min_spread = 7;
								script.Max_spread = 10;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = false;
							}
							
							//Simple vertex
							if (script.Grass_selector == 5) {
								script.min_scale = 0.5f;
								script.max_scale = 1.0f;
								script.Min_density = 2.0f;
								script.Max_density = 3.0f;
								script.Min_spread = 7;
								script.Max_spread = 10;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = false;
							}
							//White flowers
							if (script.Grass_selector == 6) {
								script.min_scale = 0.6f;
								script.max_scale = 0.9f;
								script.Min_density = 1.0f;
								script.Max_density = 1.0f;
								script.Min_spread = 7;
								script.Max_spread = 10;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = true;
							}
							//Curved grass
							if (script.Grass_selector == 7) {
								script.min_scale = 0.5f;
								script.max_scale = 1.5f;
								script.Min_density = 1.0f;
								script.Max_density = 4.0f;
								script.Min_spread = 7;
								script.Max_spread = 8;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = false;
							}
							//Low grass - FOR LIGHT DEMO without Sky Master and real time use
							if (script.Grass_selector == 8) {
								script.min_scale = 1.2f;
								script.max_scale = 1.3f;
								script.Min_density = 1.0f;
								script.Max_density = 3.0f;
								script.Min_spread = 4;
								script.Max_spread = 6;
								//script.SpecularPower = 4;
								script.Collider_scale = 0.4f;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = false;
							}
							//Vines
							if (script.Grass_selector == 9) {
								script.min_scale = 1.5f;
								script.max_scale = 1.5f;
								script.Min_density = 3.0f;
								script.Max_density = 3.0f;
								script.Min_spread = 7;
								script.Max_spread = 7;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = false;
							}
							
							//Mushrooms Brown and red
							if (script.Grass_selector == 10 | script.Grass_selector == 11) {
								script.min_scale = 0.4f;
								script.max_scale = 1.0f;
								script.Min_density = 1.0f;
								script.Max_density = 4.0f;
								script.Min_spread = 7;
								script.Max_spread = 9;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 80;
								script.LOD_distance1 = 120;
								script.LOD_distance2 = 520;
								
								script.RandomRot = false;
							}
							//Ground leaves
							if (script.Grass_selector == 12) {
								script.min_scale = 0.5f;
								script.max_scale = 0.8f;
								script.Min_density = 1.0f;
								script.Max_density = 3.0f;
								script.Min_spread = 7;
								script.Max_spread = 11;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = true;
							}
							//Noisy grass
							if (script.Grass_selector == 13) {
								script.min_scale = 0.5f;
								script.max_scale = 1.5f;
								script.Min_density = 2.0f;
								script.Max_density = 3.0f;
								script.Min_spread = 7;
								script.Max_spread = 9;
								//script.SpecularPower = 4;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
								
								script.RandomRot = true;
							}
							//Rocks
							if (script.Grass_selector == 14) {
								script.min_scale = 0.7f;
								script.max_scale = 1.2f;
								script.Min_density = 1.0f;
								script.Max_density = 3.0f;
								script.Min_spread = 7;
								script.Max_spread = 11;
								//script.SpecularPower = 4;							
								
								script.Cutoff_distance = 520;
								script.LOD_distance = 220;
								script.LOD_distance1 = 270;
								script.LOD_distance2 = 410;
								
								script.RandomRot = false;
							}
							if (script.Grass_selector > 14) {
								script.Min_density = 2;
								script.Max_density = 4;
								//Setting1.SpecularPower = 4;
								script.Min_spread = 4;
								script.Max_spread = 5;
								script.min_scale = 0.7f;
								script.max_scale = 0.9f;
								
								script.Cutoff_distance = 530;
								script.LOD_distance = 520;
								script.LOD_distance1 = 523;
								script.LOD_distance2 = 527;
									//Debug.Log(script.Grass_selector);
								script.RandomRot = false;
							}

								script.min_scale = script.min_scale*0.55f;
								script.max_scale = script.max_scale*0.55f;


								//v1.2a
								script.min_scale = script.min_scale*(script.WorldScale/20);
								script.max_scale =  script.max_scale*(script.WorldScale/20);
								//	script.Min_density =  script.Min_density*(script.WorldScale/20);
								//	script.Max_density =  script.Max_density*(script.WorldScale/20);
								script.Min_spread =  script.Min_spread*(script.WorldScale/20);
								script.Max_spread =  script.Max_spread*(script.WorldScale/20);
								//script.SpecularPower = 4;									
								script.Cutoff_distance = script.Cutoff_distance*(script.WorldScale/20);
								script.LOD_distance =  script.LOD_distance*(script.WorldScale/20);
								script.LOD_distance1 =  script.LOD_distance1*(script.WorldScale/20);
								script.LOD_distance2 =  script.LOD_distance2*(script.WorldScale/20);
								
								script.AmplifyWind = 1*(script.WorldScale/20);
								script.WindTurbulence = 0.5f*(script.WorldScale/20);
								
								script.Editor_view_dist = 4500*(script.WorldScale/20);
								script.min_grass_patch_dist = 1;
								script.Stop_Motion_distance = 20*(script.WorldScale/20);
								script.Grass_Fade_distance = script.Cutoff_distance - 60*((script.WorldScale/20));
								
								for(int i=0;i<script.GrassMaterials.Count;i++){
									script.GrassMaterials[i].SetFloat("_SmoothMotionFactor",255);
									script.GrassMaterials[i].SetVector("_TimeControl1",new Vector4(2,1,1,0));

									if(script.Grass_selector == i && script.GrassMaterials[i].HasProperty("_FadeThreshold")){
										script.Grass_Fade_distance = script.GrassMaterials[i].GetFloat("_FadeThreshold");
									}
									if(script.Grass_selector == i && script.GrassMaterials[i].HasProperty("_StopMotionThreshold")){
										script.Stop_Motion_distance = script.GrassMaterials[i].GetFloat("_StopMotionThreshold");
									}
								}

								//v1.4
								for (int i=0; i<script.ExtraGrassMaterials.Count; i++) {
									for (int j=0; j<script.ExtraGrassMaterials[i].ExtraMaterials.Count; j++) {
										script.ExtraGrassMaterials[i].ExtraMaterials[j].SetFloat("_SmoothMotionFactor",255);
										script.ExtraGrassMaterials[i].ExtraMaterials[j].SetVector("_TimeControl1",new Vector4(2,1,1,0));
									}
								}

								script.Override_density = true;
								script.Override_spread = true;
								script.MinAvoidDist = 2/10;
								script.MinScaleAvoidDist= 4/10;
								script.SphereCastRadius = (script.WorldScale/20)*50;
							//	script.Grass_Fade_distance = script.Cutoff_distance - 60*((script.WorldScale/20));
								script.Gizmo_scale = (script.WorldScale/20)*3;
								script.Collider_scale = (script.WorldScale/20);
								//END v1.2a


							if (script.Grass_selector == 1) {
								Grass_type = "Vertex Grass";
							}
							if (script.Grass_selector == 2) {
								Grass_type = "Red Flowers";
							}
							if (script.Grass_selector == 3) {
								Grass_type = "Wheet";
							}
							if (script.Grass_selector == 4) {
								Grass_type = "Detailed Vertex";
							}
						
							if (script.Grass_selector == 5) {
								Grass_type = "Thick Vertex";
							}
							if (script.Grass_selector == 6) {
								Grass_type = "White flowers";
							}
							if (script.Grass_selector == 7) {
								Grass_type = "Curved Vertex";
							}
							if (script.Grass_selector == 8) {
								Grass_type = "Low grass";
							}
							if (script.Grass_selector == 9) {
								Grass_type = "Vines";
							}
							if (script.Grass_selector > 9) {
								Grass_type = "Custom brush";
							}

							}
							//						if (script.Grass_selector == 10) {
							//							Grass_type = "Wheet";
							//						}
							EditorGUILayout.IntSlider (Grass_selector, 0, script.GrassPrefabs.Count - 1, new GUIContent ("Grass type:" + Grass_type));
						}
					
					
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Rotate brush with normal", GUILayout.MaxWidth (150f));
						script.GridOnNormal = EditorGUILayout.Toggle (script.GridOnNormal);
						EditorGUILayout.EndHorizontal ();
					
						GUILayout.Label ("----------- Control interactivity -----------", EditorStyles.boldLabel);

						//v1.4 - set ineractor
						script.Interactor = EditorGUILayout.ObjectField (script.Interactor, typeof(Transform), true, GUILayout.MaxWidth (180.0f)) as Transform;

						if (1==1) {
							EditorGUILayout.BeginHorizontal ();
							if (GUILayout.Button (new GUIContent ("Set Interactor"), GUILayout.Width (120))) {
								//script.Apply_Interactive ();
								if(script.Interactor != null){
									for (int i=0; i<script.Grasses.Count; i++) {
										if(script.apply_to_all){
											script.Grasses[i].player = script.Interactor.gameObject;
										}else{
											if(script.GrassesType[i] == script.Grass_selector){
												script.Grasses[i].player = script.Interactor.gameObject;
											}
										}
									}
								}else{
									Debug.Log("Please enter a gameobject in the Interactor field");
								}
							}
							if (GUILayout.Button (new GUIContent ("Reset Interactor (use Main Canera or Tag based)"), GUILayout.Width (310))) {
								//script.Apply_Interactive ();
								//if(script.Interactor != null){
									for (int i=0; i<script.Grasses.Count; i++) {
										if(script.apply_to_all){
											script.Grasses[i].player = null;
										}else{
											if(script.GrassesType[i] == script.Grass_selector){
												script.Grasses[i].player = null;
											}
										}
									}
								//}
							}
							EditorGUILayout.EndHorizontal ();
						}
						if (!Application.isPlaying) {
							if (GUILayout.Button (new GUIContent ("Set Interactive"), GUILayout.Width (120))) {
								script.Apply_Interactive ();
							}
						}

						//v1.4
						EditorGUILayout.Slider (Interaction_thres, 0.5f, 50f, new GUIContent ("Interaction Radius"));//radius to affect grass with 
						EditorGUILayout.Slider (Interaction_offset, 0.1f, 5f, new GUIContent ("Interaction Offset"));//smooth out offset when entering - leaving radius
					
						//interactive control
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Apply Interactive to all", GUILayout.MaxWidth (150f));
						script.apply_to_all = EditorGUILayout.Toggle (script.apply_to_all, GUILayout.MaxWidth (30f));
						GUILayout.Label ("(Set Interactive will apply to all grass)", GUILayout.MaxWidth (250f));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Make grass interactive", GUILayout.MaxWidth (150f));
						script.Interactive = EditorGUILayout.Toggle (script.Interactive, GUILayout.MaxWidth (30f));
						GUILayout.Label ("(Set Interactive will apply to all in current type)", GUILayout.MaxWidth (300f));
						EditorGUILayout.EndHorizontal ();
					
						GUILayout.Label ("----------- Slow wind on interact -----------", EditorStyles.boldLabel);
						EditorGUILayout.Slider (Stop_Motion_distance, 0.0f, 2000.0f, new GUIContent ("Stop Motion Distance"));

						GUILayout.Label ("----------- Interaction parameters -----------", EditorStyles.boldLabel);
						EditorGUILayout.Slider (InteractionSpeed, 0.1f, 20f, new GUIContent ("Interaction Power"));//speed to affect grass with 
						EditorGUILayout.Slider (InteractSpeedThres, 0f, 5f, new GUIContent ("Min speed for interact"));//affect grass only if hero speed above this value
					
						////////////////////////////////////////////////// DEFINE LODs
						GUILayout.Label ("----------- LOD distances -----------", EditorStyles.boldLabel);
						if (!Application.isPlaying) {
							EditorGUILayout.BeginHorizontal ();
							//EDIT GRASS PROPERTIES
							if (GUILayout.Button (new GUIContent ("Set LOD"), GUILayout.Width (120))) {
								for (int i=0; i<script.Grasses.Count; i++) {
									if(script.Apply_LOD_to_all | (!script.Apply_LOD_to_all && (script.Grasses[i].Type-1) == script.Grass_selector )){
										script.Grasses [i].Combiner.Deactivate_hero_dist = script.LOD_distance;
										script.Grasses [i].Combiner.Deactivate_hero_distCUT = script.Cutoff_distance;							
										script.Grasses [i].Combiner.Deactivate_hero_dist1 = script.LOD_distance1;
										script.Grasses [i].Combiner.Deactivate_hero_dist2 = script.LOD_distance2;
									}
								}
							}
						
							EditorGUILayout.EndHorizontal ();	

							//v1.4
							EditorGUILayout.BeginHorizontal ();
							GUILayout.Label ("Apply LOD to all", GUILayout.MaxWidth (150f));
							script.Apply_LOD_to_all = EditorGUILayout.Toggle (script.Apply_LOD_to_all, GUILayout.MaxWidth (30f));
							EditorGUILayout.EndHorizontal ();
					
							EditorGUILayout.Slider (LOD_distance, 2, 2000, new GUIContent ("LOD distance (Close)"));
							EditorGUILayout.Slider (LOD_distance1, 2, 2000, new GUIContent ("LOD distance1 (Mid)"));
							EditorGUILayout.Slider (LOD_distance2, 2, 2000, new GUIContent ("LOD distance2 (Far)"));
						
							EditorGUILayout.Slider (Cutoff_distance, 2, 2500, new GUIContent ("Cut off distance"));
						}
					
						GUILayout.Label ("----------- Grass Fade distance -----------", EditorStyles.boldLabel);
						//v1.4
						EditorGUILayout.BeginHorizontal ();
						//EDIT GRASS PROPERTIES
						if (GUILayout.Button (new GUIContent ("Auto assign fade"), GUILayout.Width (120))) {
							script.Grass_Fade_distance = script.Cutoff_distance - 60*((script.WorldScale/20));
						}
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Slider (Grass_Fade_distance, 0.0f, 2000.0f, new GUIContent ("Grass Fade Distance"));
					
						GUILayout.Label ("----------- Grass Grouping Control -----------", EditorStyles.boldLabel);
						EditorGUILayout.IntSlider (Max_interactive_group_members, 2, 20, new GUIContent ("Max Interactive Group Members"));
						EditorGUILayout.IntSlider (Max_static_group_members, 8, 150, new GUIContent ("Max Static Group Members"));

						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Group by object", GUILayout.MaxWidth (150f));
						script.GroupByObject = EditorGUILayout.Toggle (script.GroupByObject);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Parent to Object", GUILayout.MaxWidth (150f));
						script.ParentToObject = EditorGUILayout.Toggle (script.ParentToObject);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Move with Object", GUILayout.MaxWidth (150f));
						script.MoveWithObject = EditorGUILayout.Toggle (script.MoveWithObject);
						EditorGUILayout.EndHorizontal ();
					

					
						EditorGUIUtility.wideMode = false;
					
						EditorGUILayout.BeginHorizontal ();				
					
						EditorGUILayout.EndHorizontal ();
					
					
						//GRASS PROPS				
					
						GUILayout.Label ("----------- Paint distances -----------", EditorStyles.boldLabel);	
						EditorGUILayout.Slider (min_grass_patch_dist, 0.0f, 20.0f, new GUIContent ("Grass Patch Distance"));
						EditorGUILayout.Slider (rayCastDist, 1f, 15f, new GUIContent ("Raycast distance"));//distance to move along surface normal before raycast, this is doubled for the actual raycast.
					
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Mass erase", GUILayout.MaxWidth (150f));
						script.MassErase = EditorGUILayout.Toggle (script.MassErase);
						EditorGUILayout.EndHorizontal ();					
						EditorGUILayout.Slider (SphereCastRadius, 1f, 100f, new GUIContent ("Erase radius"));
						//EditorGUILayout.BeginHorizontal();
					
					
						//display values
						if (script.Rock_painting | script.Grass_painting) {
						
							if (script.Rock_painting) {
								GUILayout.Label ("----------- Rocks scale -----------", EditorStyles.boldLabel);	
							} else {
								GUILayout.Label ("----------- Grass scale - density -----------", EditorStyles.boldLabel);	
							}
						
							EditorGUILayout.MinMaxSlider (new GUIContent ("Object scale (" + script.min_scale.ToString ("F2") + " - " + script.max_scale.ToString ("F2") + ")"), ref script.min_scale, ref script.max_scale, 0.01f, 5);
							EditorGUILayout.BeginHorizontal ();
							GUILayout.Label ("Scale values (min-max):");
							script.min_scale = EditorGUILayout.FloatField (script.min_scale, GUILayout.Width (120));
							script.max_scale = EditorGUILayout.FloatField (script.max_scale, GUILayout.Width (120));
							EditorGUILayout.EndHorizontal ();

							//v1.1 Enable random rotation
							EditorGUILayout.BeginHorizontal ();
							GUILayout.Label ("Randomize rotation", GUILayout.MaxWidth (150f));
							script.RandomRot = EditorGUILayout.Toggle (script.RandomRot);
							EditorGUILayout.EndHorizontal ();
							//v1.1 randomize rotation
							EditorGUILayout.MinMaxSlider (new GUIContent ("Object rotation (" + script.RandRotMin.ToString ("F2") + " - " + script.RandRotMax.ToString ("F2") + ")"), ref script.RandRotMin, ref script.RandRotMax, 0.01f, 5);
							EditorGUILayout.BeginHorizontal ();
							GUILayout.Label ("Object rotation (min-max):");
							script.RandRotMin = EditorGUILayout.FloatField (script.RandRotMin, GUILayout.Width (120));
							script.RandRotMax = EditorGUILayout.FloatField (script.RandRotMax, GUILayout.Width (120));
							EditorGUILayout.EndHorizontal ();
						}
					
						if (script.Grass_painting) {
							EditorGUILayout.Slider (Collider_scale, 0.1f, 5f, new GUIContent ("Collider scale"));
							EditorGUILayout.Slider (Gizmo_scale, 0.1f, 6f, new GUIContent ("Gizmos scale"));
						}
					
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Override Grass density", GUILayout.MaxWidth (150f));
						script.Override_density = EditorGUILayout.Toggle (script.Override_density);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.MinMaxSlider (new GUIContent ("Grass density (" + script.Min_density.ToString ("F2") + " - " + script.Max_density.ToString ("F2") + ")"), ref script.Min_density, ref script.Max_density, 1, 6);
					
						//v1.1a
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Density (min-max):");
						script.Min_density = EditorGUILayout.FloatField (script.Min_density, GUILayout.Width (120));
						script.Max_density = EditorGUILayout.FloatField (script.Max_density, GUILayout.Width (120));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Override Grass spread", GUILayout.MaxWidth (150f));
						script.Override_spread = EditorGUILayout.Toggle (script.Override_spread);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.MinMaxSlider (new GUIContent ("Grass spread (" + script.Min_spread.ToString ("F2") + " - " + script.Max_spread.ToString ("F2") + ")"), ref script.Min_spread, ref script.Max_spread, 0.1f, 50);
					
						//v1.1a
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Spread (min-max):");
						script.Min_spread = EditorGUILayout.FloatField (script.Min_spread, GUILayout.Width (120));
						script.Max_spread = EditorGUILayout.FloatField (script.Max_spread, GUILayout.Width (120));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Scale per splat map", GUILayout.MaxWidth (150f));
						script.AdaptOnTerrain = EditorGUILayout.Toggle (script.AdaptOnTerrain);
						EditorGUILayout.EndHorizontal ();
						if(script.AdaptOnTerrain){
							EditorGUILayout.PropertyField(ScalePerTexture,new GUIContent("Scale per splat map"),true, GUILayout.MaxWidth (250f));
							for(int i=0;i<script.ScalePerTexture.Count;i++){
								if(script.ScalePerTexture[i] <= 0){
									script.ScalePerTexture[i] = 1;
								}
							}
						}

						GUILayout.Label ("----------- Grass clean up -----------", EditorStyles.boldLabel);
					
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Clean up low blade count grass", GUILayout.MaxWidth (150f));
						script.CleanUp = EditorGUILayout.Toggle (script.CleanUp);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.IntSlider (MinBranches, 5, 30, new GUIContent ("Minimal blade count for clean up"));
					
						///////////////////////////////////////////////////////////////////////////
					
						EditorGUILayout.Slider (MinAvoidDist, 0.1f, 6f, new GUIContent ("Near distance for elimination"));
						EditorGUILayout.Slider (MinScaleAvoidDist, 0.1f, 5f, new GUIContent ("Near distance for scaling"));

						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Avoid own collider", GUILayout.MaxWidth (150f));
						script.AvoidOwnColl = EditorGUILayout.Toggle (script.AvoidOwnColl);
						EditorGUILayout.EndHorizontal ();
					

					
					}
				
				
					////// SPECIFIC TOOLS PER PAINT SYSTEM
				
					if (script.Rock_painting) {
					
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (RockICON1, GUILayout.Width (80), GUILayout.Height (80))) {						
							script.Grass_selector = 0;					
						}
						if (GUILayout.Button (RockICON2, GUILayout.Width (80), GUILayout.Height (80))) {						
							script.Grass_selector = 1;					
						}
						if (GUILayout.Button (RockICON3, GUILayout.Width (80), GUILayout.Height (80))) {						
							script.Grass_selector = 2;					
						}
						EditorGUILayout.EndHorizontal ();
					
						GUILayout.Label ("----------- Select rock type -----------", EditorStyles.boldLabel);	
					
						if (script.Grass_selector > script.RockPrefabs.Count - 1) {
							script.Grass_selector = 0;
						}
					
						string Grass_type = "White Rock";
						if (script.Grass_selector == 1) {
							Grass_type = "Dark Rock";
						}
					
						EditorGUILayout.IntSlider (Grass_selector, 0, script.RockPrefabs.Count - 1, new GUIContent ("Rock type:" + Grass_type));
					}
					if (script.Fence_painting) {
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (FenceICON1, GUILayout.Width (80), GUILayout.Height (80))) {						
							script.Grass_selector = 0;					
						}
						if (GUILayout.Button (FenceICON2, GUILayout.Width (80), GUILayout.Height (80))) {						
							script.Grass_selector = 1;					
						}
						EditorGUILayout.EndHorizontal ();
					
						GUILayout.Label ("----------- Select fence type -----------", EditorStyles.boldLabel);	
						if (script.Fence_painting) {
						
							if (script.Grass_selector > script.FencePrefabs.Count - 1) {
								script.Grass_selector = 0;
							}
						
							string Grass_type = "Brown fence";
							if (script.Grass_selector == 1) {
								Grass_type = "Dark Brown fence";
							}
						
							EditorGUILayout.IntSlider (Grass_selector, 0, script.FencePrefabs.Count - 1, new GUIContent ("Fence type:" + Grass_type));
						}
					
						GUILayout.Label ("----------- Fence post distance -----------", EditorStyles.boldLabel);	
						EditorGUILayout.Slider (min_grass_patch_dist, 1.0f, 40.0f, new GUIContent ("Rock Patch Distance"));
					}
				
				
					if (script.Rock_painting) {
						GUILayout.Label ("----------- Paint distances -----------", EditorStyles.boldLabel);	
						EditorGUILayout.Slider (min_grass_patch_dist, 0.0f, 40.0f, new GUIContent ("Rock Patch Distance"));
					
						//display values
						if (script.Rock_painting | script.Grass_painting) {
						
							if (script.Rock_painting) {
								GUILayout.Label ("----------- Rocks scale -----------", EditorStyles.boldLabel);	
							} else {
								GUILayout.Label ("----------- Grass scale - density -----------", EditorStyles.boldLabel);	
							}
						
							EditorGUILayout.MinMaxSlider (new GUIContent ("Object scale (" + script.min_scale.ToString ("F2") + " - " + script.max_scale.ToString ("F2") + ")"), ref script.min_scale, ref script.max_scale, 0.1f, 20);
							EditorGUILayout.BeginHorizontal ();
							GUILayout.Label ("Scale values (min-max):");
							script.min_scale = EditorGUILayout.FloatField (script.min_scale, GUILayout.Width (120));
							script.max_scale = EditorGUILayout.FloatField (script.max_scale, GUILayout.Width (120));
							EditorGUILayout.EndHorizontal ();
						}
					}
				
					if (script.Fence_painting) {
						GUILayout.Label ("----------- Fence scale -----------", EditorStyles.boldLabel);	
						EditorGUILayout.Slider (fence_scale, 0.0f, 10.0f, new GUIContent ("Fence scale"));
					}
				
					GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));
				
				}//END PAINT CONTROLS --------------
			

			}//END if UNGROWN check

			EditorGUILayout.EndVertical();
			
			serializedObject.ApplyModifiedProperties ();

			//v1.2
			//EditorUtility.SetDirty (script);
		}

		//v1.4 - define initial setttings for brush struct
		void Define_init_settings(InfiniGRASS_BrushSettings Setting1, int i){
			//grass main
			if (i  == 0) {
				Setting1.Min_density = 1;
				Setting1.Max_density = 4;
				//Setting1.SpecularPower = 4;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 9;
				Setting1.min_scale = 0.4f;
				Setting1.max_scale = 0.6f;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = false;
			}
			//Vertex grass
			if (i  == 1) {
				Setting1.min_scale = 0.4f;
				Setting1.max_scale = 0.8f;
				Setting1.Min_density = 2.0f;
				Setting1.Max_density = 3.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 9;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = false;
			}
			//Red flowers
			if (i == 2) {
				Setting1.min_scale = 0.8f;
				Setting1.max_scale = 0.9f;
				Setting1.Min_density = 1.0f;
				Setting1.Max_density = 1.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 10;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = true;
			}
			//Wheet
			if (i  == 3) {
				Setting1.min_scale = 1.0f;
				Setting1.max_scale = 1.5f;
				Setting1.Min_density = 1.0f;
				Setting1.Max_density = 1.0f;
				Setting1.Min_spread = 15;
				Setting1.Max_spread = 20;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = false;
			}
			//Detailed vertex
			if (i  == 4) {
				Setting1.min_scale = 1.0f;
				Setting1.max_scale = 1.2f;
				Setting1.Min_density = 1.0f;
				Setting1.Max_density = 3.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 10;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = false;
			}
			
			//Simple vertex
			if (i  == 5) {
				Setting1.min_scale = 0.5f;
				Setting1.max_scale = 1.0f;
				Setting1.Min_density = 2.0f;
				Setting1.Max_density = 3.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 10;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = false;
			}
			//White flowers
			if (i  == 6) {
				Setting1.min_scale = 0.6f;
				Setting1.max_scale = 0.9f;
				Setting1.Min_density = 1.0f;
				Setting1.Max_density = 1.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 10;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = true;
			}
			//Curved grass
			if (i  == 7) {
				Setting1.min_scale = 0.5f;
				Setting1.max_scale = 1.5f;
				Setting1.Min_density = 1.0f;
				Setting1.Max_density = 4.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 8;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = false;
			}
			//Low grass - FOR LIGHT DEMO without Sky Master and real time use
			if (i  == 8) {
				Setting1.min_scale = 1.2f;
				Setting1.max_scale = 1.3f;
				Setting1.Min_density = 1.0f;
				Setting1.Max_density = 3.0f;
				Setting1.Min_spread = 4;
				Setting1.Max_spread = 6;
				//Setting1.SpecularPower = 4;
				Setting1.Collider_scale = 0.4f;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = false;
			}
			//Vines
			if (i  == 9) {
				Setting1.min_scale = 1.5f;
				Setting1.max_scale = 1.5f;
				Setting1.Min_density = 3.0f;
				Setting1.Max_density = 3.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 7;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = false;
			}
			
			//Mushrooms Brown and red
			if (i  == 10 | i  == 11) {
				Setting1.min_scale = 0.4f;
				Setting1.max_scale = 1.0f;
				Setting1.Min_density = 1.0f;
				Setting1.Max_density = 4.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 9;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 80;
				Setting1.LOD_distance1 = 120;
				Setting1.LOD_distance2 = 520;
				
				Setting1.RandomRot = false;
			}
			//Ground leaves
			if (i  == 12) {
				Setting1.min_scale = 0.5f;
				Setting1.max_scale = 0.8f;
				Setting1.Min_density = 1.0f;
				Setting1.Max_density = 3.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 11;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = true;
			}
			//Noisy grass
			if (i  == 13) {
				Setting1.min_scale = 0.5f;
				Setting1.max_scale = 1.5f;
				Setting1.Min_density = 2.0f;
				Setting1.Max_density = 3.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 9;
				//Setting1.SpecularPower = 4;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = true;
			}
			//Rocks
			if (i  == 14) {
				Setting1.min_scale = 0.7f;
				Setting1.max_scale = 1.2f;
				Setting1.Min_density = 1.0f;
				Setting1.Max_density = 3.0f;
				Setting1.Min_spread = 7;
				Setting1.Max_spread = 11;
				//Setting1.SpecularPower = 4;							
				
				Setting1.Cutoff_distance = 520;
				Setting1.LOD_distance = 220;
				Setting1.LOD_distance1 = 270;
				Setting1.LOD_distance2 = 410;
				
				Setting1.RandomRot = false;
			}
			if (i > 14) {
				Setting1.Min_density = 2;
				Setting1.Max_density = 4;
				//Setting1.SpecularPower = 4;
				Setting1.Min_spread = 4;
				Setting1.Max_spread = 5;
				Setting1.min_scale = 0.7f;
				Setting1.max_scale = 0.9f;
				
				Setting1.Cutoff_distance = 530;
				Setting1.LOD_distance = 520;
				Setting1.LOD_distance1 = 523;
				Setting1.LOD_distance2 = 527;
				
				Setting1.RandomRot = false;
			}
			
			Setting1.min_scale = Setting1.min_scale*0.55f;
			Setting1.max_scale = Setting1.max_scale*0.55f;
			
			//v1.2a
			Setting1.min_scale = Setting1.min_scale*(script.WorldScale/20);
			Setting1.max_scale =  Setting1.max_scale*(script.WorldScale/20);
			//	Setting1.Min_density =  Setting1.Min_density*(script.WorldScale/20);
			//	Setting1.Max_density =  Setting1.Max_density*(script.WorldScale/20);
			Setting1.Min_spread =  Setting1.Min_spread*(script.WorldScale/20);
			Setting1.Max_spread =  Setting1.Max_spread*(script.WorldScale/20);
			//Setting1.SpecularPower = 4;									
			Setting1.Cutoff_distance = Setting1.Cutoff_distance*(script.WorldScale/20);
			Setting1.LOD_distance =  Setting1.LOD_distance*(script.WorldScale/20);
			Setting1.LOD_distance1 =  Setting1.LOD_distance1*(script.WorldScale/20);
			Setting1.LOD_distance2 =  Setting1.LOD_distance2*(script.WorldScale/20);
			
			Setting1.AmplifyWind = 1*(script.WorldScale/20);
			Setting1.WindTurbulence = 0.5f*(script.WorldScale/20);
			
			//Setting1.Editor_view_dist = 4500*(script.WorldScale/20);
			Setting1.min_grass_patch_dist = 1;
			Setting1.Stop_Motion_distance = 20*(script.WorldScale/20);
			
			//					for(int i=0;i<Setting1.GrassMaterials.Count;i++){
			//						Setting1.GrassMaterials[i].SetFloat("_SmoothMotionFactor",255);
			//						Setting1.GrassMaterials[i].SetVector("_TimeControl1",new Vector4(2,1,1,0));
			//					}
			Setting1.Override_density = true;
			Setting1.Override_spread = true;
			Setting1.MinAvoidDist = 2/10;
			Setting1.MinScaleAvoidDist= 4/10;
			Setting1.SphereCastRadius = (script.WorldScale/20)*50;
			Setting1.Grass_Fade_distance = Setting1.Cutoff_distance - 60*((script.WorldScale/20));
			//Setting1.Gizmo_scale = (script.WorldScale/20)*3;
			Setting1.Collider_scale = (script.WorldScale/20);
			
			//REST
			Setting1.Min_density = script.Min_density;
			Setting1.Max_density = script.Max_density;
			Setting1.rayCastDist  = script.rayCastDist;
			Setting1.InteractionSpeed  = script.InteractionSpeed;
			Setting1.InteractSpeedThres  = script.InteractSpeedThres;
			Setting1.Interaction_thres  = (script.WorldScale/20)*20;
			Setting1.Interaction_offset  = (script.WorldScale/20)*2;
			Setting1.RandRotMin  = script.RandRotMin;
			Setting1.RandRotMax  = script.RandRotMax;
			Setting1.RandomRot  = script. RandomRot;
			
			Setting1.GroupByObject  = script.GroupByObject;
			Setting1.ParentToObject  = script.ParentToObject;
			Setting1.MoveWithObject  = script.MoveWithObject;
			Setting1.AvoidOwnColl  = script.AvoidOwnColl;
			
			Setting1.AdaptOnTerrain  = script.AdaptOnTerrain;
			Setting1.Max_interactive_group_members  = script.Max_interactive_group_members;
			Setting1.Max_static_group_members  = script.Max_static_group_members;
			Setting1.Interactive  = script.Interactive;
			Setting1.GridOnNormal  = script.GridOnNormal;
		}
		void LoadBrush(InfiniGRASS_BrushSettings Setting1){

			//v1.2a
			script.min_scale = Setting1.min_scale;
			script.max_scale = Setting1.max_scale;
			script.Min_density = Setting1.Min_density;
			script.Max_density = Setting1.Max_density;
			script.Min_spread = Setting1.Min_spread;
			script.Max_spread = Setting1.Max_spread;
			script.SpecularPower = Setting1.SpecularPower;									
			script.Cutoff_distance = Setting1.Cutoff_distance;
			script.LOD_distance = Setting1.LOD_distance;
			script.LOD_distance1 = Setting1.LOD_distance1;
			script.LOD_distance2 = Setting1.LOD_distance2;
			
			script.AmplifyWind = Setting1.AmplifyWind;
			script.WindTurbulence = Setting1.WindTurbulence;
			
			//script.Editor_view_dist = 4500*(script.WorldScale/20);
			script.min_grass_patch_dist = Setting1.min_grass_patch_dist;
			script.Stop_Motion_distance = Setting1.Stop_Motion_distance;
			
			//					for(int i=0;i<script.GrassMaterials.Count;i++){
			//						script.GrassMaterials[i].SetFloat("_SmoothMotionFactor",255);
			//						script.GrassMaterials[i].SetVector("_TimeControl1",new Vector4(2,1,1,0));
			//					}
			script.Override_density = Setting1.Override_density;
			script.Override_spread = Setting1.Override_spread;
			script.MinAvoidDist = Setting1.MinAvoidDist;
			script.MinScaleAvoidDist= Setting1.MinScaleAvoidDist;
			script.SphereCastRadius = Setting1.SphereCastRadius;
			script.Grass_Fade_distance = Setting1.Grass_Fade_distance;
			//script.Gizmo_scale = (script.WorldScale/20)*3;
			script.Collider_scale = Setting1.Collider_scale;
			
			//REST
			script.Min_density = Setting1.Min_density;
			script.Max_density = Setting1.Max_density;
			script.rayCastDist  = Setting1.rayCastDist;
			script.InteractionSpeed  = Setting1.InteractionSpeed;
			script.InteractSpeedThres  = Setting1.InteractSpeedThres;
			script.Interaction_thres  = Setting1.Interaction_thres;
			script.Interaction_offset  = Setting1.Interaction_offset;
			script.RandRotMin  = Setting1.RandRotMin;
			script.RandRotMax  = Setting1.RandRotMax;
			script.RandomRot  = Setting1. RandomRot;
			
			script.GroupByObject  = Setting1.GroupByObject;
			script.ParentToObject  = Setting1.ParentToObject;
			script.MoveWithObject  = Setting1.MoveWithObject;
			script.AvoidOwnColl  = Setting1.AvoidOwnColl;
			
			script.AdaptOnTerrain  = Setting1.AdaptOnTerrain;
			script.Max_interactive_group_members  = Setting1.Max_interactive_group_members;
			script.Max_static_group_members  = Setting1.Max_static_group_members;
			script.Interactive  = Setting1.Interactive;
			script.GridOnNormal  = Setting1.GridOnNormal;

		}
		void SaveBrush(InfiniGRASS_BrushSettings Setting1){
						
			//v1.2a
			Setting1.min_scale = script.min_scale;
			Setting1.max_scale = script.max_scale;
			Setting1.Min_density = script.Min_density;
			Setting1.Max_density = script.Max_density;
			Setting1.Min_spread = script.Min_spread;
			Setting1.Max_spread = script.Max_spread;
			Setting1.SpecularPower = script.SpecularPower;									
			Setting1.Cutoff_distance = script.Cutoff_distance;
			Setting1.LOD_distance = script.LOD_distance;
			Setting1.LOD_distance1 = script.LOD_distance1;
			Setting1.LOD_distance2 = script.LOD_distance2;
			
			Setting1.AmplifyWind = script.AmplifyWind;
			Setting1.WindTurbulence = script.WindTurbulence;
			
			//Setting1.Editor_view_dist = 4500*(script.WorldScale/20);
			Setting1.min_grass_patch_dist = script.min_grass_patch_dist;
			Setting1.Stop_Motion_distance = script.Stop_Motion_distance;
			
			//					for(int i=0;i<Setting1.GrassMaterials.Count;i++){
			//						Setting1.GrassMaterials[i].SetFloat("_SmoothMotionFactor",255);
			//						Setting1.GrassMaterials[i].SetVector("_TimeControl1",new Vector4(2,1,1,0));
			//					}
			Setting1.Override_density = script.Override_density;
			Setting1.Override_spread = script.Override_spread;
			Setting1.MinAvoidDist = script.MinAvoidDist;
			Setting1.MinScaleAvoidDist= script.MinScaleAvoidDist;
			Setting1.SphereCastRadius = script.SphereCastRadius;
			Setting1.Grass_Fade_distance = script.Grass_Fade_distance;
			//Setting1.Gizmo_scale = (script.WorldScale/20)*3;
			Setting1.Collider_scale = script.Collider_scale;
			
			//REST
			Setting1.Min_density = script.Min_density;
			Setting1.Max_density = script.Max_density;
			Setting1.rayCastDist  = script.rayCastDist;
			Setting1.InteractionSpeed  = script.InteractionSpeed;
			Setting1.InteractSpeedThres  = script.InteractSpeedThres;
			Setting1.Interaction_thres  = script.Interaction_thres;
			Setting1.Interaction_offset  = script.Interaction_offset;
			Setting1.RandRotMin  = script.RandRotMin;
			Setting1.RandRotMax  = script.RandRotMax;
			Setting1.RandomRot  = script. RandomRot;
			
			Setting1.GroupByObject  = script.GroupByObject;
			Setting1.ParentToObject  = script.ParentToObject;
			Setting1.MoveWithObject  = script.MoveWithObject;
			Setting1.AvoidOwnColl  = script.AvoidOwnColl;
			
			Setting1.AdaptOnTerrain  = script.AdaptOnTerrain;
			Setting1.Max_interactive_group_members  = script.Max_interactive_group_members;
			Setting1.Max_static_group_members  = script.Max_static_group_members;
			Setting1.Interactive  = script.Interactive;
			Setting1.GridOnNormal  = script.GridOnNormal;
		}
		
	}
}
