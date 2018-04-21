using System.Collections;
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
	public Transform cameraRigTransform;

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
		preview.transform.Rotate (new Vector3 (-90, 0, 0));
	}

	void SpawnObject() {
		if (!preview.name.Contains("Erase") && cameraRigTransform.localScale.x == 1) {
			GameObject obj = Instantiate (objPrefab);
			obj.SetActive (true);
			obj.transform.position = preview.transform.position;
			obj.transform.rotation = preview.transform.rotation;
			if (preview.name.Contains("hill") || preview.name.Contains("mountain"))
				obj.layer = 8;
		}
	}
	
	void Update () {
		if (Controller.GetHairTriggerDown ()) {
			SpawnObject();
		}
	}
}
