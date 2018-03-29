using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn: MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}
	private GameObject previewHill;

	public GameObject hillPrefab;


	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		previewHill = Instantiate (hillPrefab);
		previewHill.transform.parent = transform;
	}

	void SpawnHill() {
		GameObject hill = Instantiate (hillPrefab);
		hill.SetActive(true);
		hill.transform.position = previewHill.transform.position;
		hill.transform.rotation = previewHill.transform.rotation;
	}
	
	void Update () {
		if (Controller.GetHairTrigger () && !transform.GetChild(1).name.Contains("Menu")) {
			SpawnHill();
		}
	}
}
