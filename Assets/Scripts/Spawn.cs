﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn: MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}
	private GameObject preview;
	private Collider parentCol;

	public GameObject objPrefab;


	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		setPreview ();
		parentCol = GetComponent<Collider>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.name != objPrefab.name && !other.name.Contains("plane") && !other.name.Contains("Clone")) {
			objPrefab = other.gameObject;
			setPreview ();
		}
	}

	void setPreview() {
		Destroy (preview);
		preview = Instantiate (objPrefab);
		preview.transform.parent = transform;
		preview.transform.position = trackedObj.transform.position;
		preview.transform.rotation = trackedObj.transform.rotation;
	}

	void SpawnObject() {
		if (!preview.name.Contains("Erase")) {
			GameObject obj = Instantiate (objPrefab);
			obj.SetActive (true);
			obj.transform.position = preview.transform.position;
			obj.transform.rotation = preview.transform.rotation;
		}
	}
	
	void Update () {
		if (Controller.GetHairTriggerDown ()) {
			SpawnObject();
		}
	}
}
