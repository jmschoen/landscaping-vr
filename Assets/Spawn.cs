using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn: MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private GameObject objectInHand;
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	public GameObject hillPrefab;


	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void SpawnHill() {
		GameObject hill = Instantiate (hillPrefab);
		hill.SetActive(true);
		hill.transform.position = gameObject.transform.position;
	}
	
	void Update () {
		if (Controller.GetHairTriggerDown ()) {
			SpawnHill();
		}
	}
}
