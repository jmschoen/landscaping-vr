using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move: MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device device;
	private float x;
	private float y;
	
	public Transform cameraRigTransform;

	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Update () {
		if (cameraRigTransform.localScale == new Vector3 (1, 1, 1)) {
			if (Controller.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
				device = SteamVR_Controller.Input ((int)trackedObj.index);
				if (device.GetAxis ().x != 0 || device.GetAxis ().y != 0) {
					cameraRigTransform.Translate (gameObject.transform.forward.x * Time.deltaTime, 0, gameObject.transform.forward.z * Time.deltaTime);
				} 	
			}
		}
	}
}
