using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private GameObject laser;
	private Transform laserTransform;
	private Vector3 hitPoint;
	private GameObject reticle;
	private Transform teleportReticleTransform;
	private bool shouldTeleport;

	public GameObject laserPrefab;
	public LayerMask teleportMask;
	public Transform headTransform;
	public Vector3 teleportReticleOffset;
	public Transform cameraRigTransform;
	public GameObject teleportReticlePrefab;

	void Start (){
		laser = Instantiate (laserPrefab);
		laser.SetActive (false);
		laserTransform = laser.transform;
		reticle = Instantiate (teleportReticlePrefab);
		reticle.SetActive (false);
		teleportReticleTransform = reticle.transform;
	}

	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	private void ShowLaser (RaycastHit hit){
		laser.SetActive (true);
		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
		laserTransform.LookAt (hitPoint);
		laserTransform.localScale = new Vector3 (laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
	}

	private void TeleportPlayer(){
		shouldTeleport = false;
		reticle.SetActive (false);
		Vector3 difference = cameraRigTransform.position - headTransform.position;
		difference.y = 0;
		cameraRigTransform.position = hitPoint + difference;
	}

	void Update () {
		if (cameraRigTransform.localScale.x < 1) {
			if (Controller.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
				RaycastHit hit;
				if (Physics.Raycast (trackedObj.transform.position, transform.forward, out hit, 100, teleportMask)) {
					GameObject hitObject = hit.collider.gameObject;
					if (hitObject.layer == 8) { // layer 8 = "Land"
						hitPoint = hit.point;
						ShowLaser (hit);
						reticle.SetActive (true);
						teleportReticleTransform.position = hitPoint + teleportReticleOffset;
						shouldTeleport = true;
					}
				}
			} else {
				laser.SetActive (false);
				reticle.SetActive (false);
			}
			if (Controller.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport) {
				TeleportPlayer ();
			}
		}
	}
}
