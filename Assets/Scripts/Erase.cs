using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase: MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}
	private GameObject erasableObj;

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.name.Contains ("Clone"))
			erasableObj = col.gameObject;
	}

	void OnTriggerExit(Collider col) {
		erasableObj = null;
	}

	void Update() {
		if (Controller.GetHairTrigger () && transform.GetChild(1).name.Contains("Erase") && erasableObj != null) {
			Destroy (erasableObj);
		}
	}
}

