using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink: MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}
	private bool hasShrunk = false;

	public Transform playerArea;

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void shrinkOrGrow() {
		if (hasShrunk) {
			playerArea.localScale = new Vector3 (1, 1, 1);
			playerArea.position = new Vector3 (0, 0, 0);
		} else {
			playerArea.localScale = new Vector3 (.01f, .01f, .01f);
			playerArea.position = new Vector3 (0, 1, 0);
		}
		hasShrunk = !hasShrunk;
	}

	void Update() {
		if (Controller.GetPressUp (SteamVR_Controller.ButtonMask.Grip)) {
			shrinkOrGrow ();
		}
	}
		
}

