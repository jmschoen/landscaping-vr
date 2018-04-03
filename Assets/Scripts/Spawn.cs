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
		setPreview ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name != objPrefab.name) {
			objPrefab = other.gameObject;
			setPreview ();
		}
	}

	void setPreview() {
		Destroy (preview);
		preview = Instantiate (objPrefab);
		deleteColliderIfHasCollider (preview);
		preview.transform.parent = transform;
		preview.transform.position = trackedObj.transform.position;
		preview.transform.rotation = trackedObj.transform.rotation;
	}

	void SpawnObject() {
		GameObject obj = Instantiate (objPrefab);
		deleteColliderIfHasCollider (obj);
		obj.SetActive(true);
		obj.transform.position = preview.transform.position;
		obj.transform.rotation = preview.transform.rotation;
	}

	void deleteColliderIfHasCollider(GameObject o) {
		Collider col;
		if (col = o.GetComponent<Collider> ())
			Destroy (col);
	}
	
	void Update () {
		if (Controller.GetHairTriggerDown ()) {
			SpawnObject();
		}
	}
}
