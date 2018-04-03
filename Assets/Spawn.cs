using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn: MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}
	private GameObject preview;

	public GameObject objPrefab;


	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		preview = Instantiate (objPrefab);
		preview.transform.parent = transform;
	}

	void SpawnObject() {
		GameObject obj = Instantiate (objPrefab);
		obj.SetActive(true);
		obj.transform.position = preview.transform.position;
		obj.transform.rotation = preview.transform.rotation;
	}
	
	void Update () {
		if (Controller.GetHairTrigger () && !transform.GetChild(1).name.Contains("Menu")) {
			SpawnObject();
		}
	}
}
