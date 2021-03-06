﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas: MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	public GameObject layout;

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.Contains("plane") && !other.name.Contains("Clone")) {
			if (layout.transform.childCount > 1)
				Destroy(layout.transform.GetChild(0).gameObject);
			GameObject obj = Instantiate (other.gameObject);
			obj.transform.SetParent (layout.transform);
			//deleteColliderIfHasCollider (obj);
			obj.transform.position = new Vector3 (0, 1, 0);
			obj.transform.localScale = new Vector3 (200, 200, 200);
			obj.layer = 8;
		}
	}

	void deleteColliderIfHasCollider(GameObject o) {
		Collider col;
		if (col = o.GetComponent<Collider> ())
			Destroy (col);
	}

}