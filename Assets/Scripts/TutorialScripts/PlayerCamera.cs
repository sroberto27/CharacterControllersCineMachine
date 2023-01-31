using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] new Transform camera;	
	[SerializeField] float velocityPredictionFactor = 1;
	[SerializeField] float velocityPredictionAcceleration = 1;	
	Vector3 lastPosition;
	Vector3 playerVelocity;
	Vector3 velocityPredition;
	Vector3 cameraBaseOffset;
	public float lookSensitivity = 150;
	Vector3 rotationEuler;
	Vector3 towardsCamera;
	float startCameraDist;
	float cameraZoomIn;	
	[SerializeField] float closestDist = 2;

	void Awake(){
		player = GameObject.FindGameObjectsWithTag("Player")[0];
	}

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		cameraBaseOffset = camera.localPosition;
		rotationEuler = transform.rotation.eulerAngles;
		startCameraDist = Vector3.Distance (transform.position, camera.position);
	}
	
	// Update is called once per frame    
	void Update () {        
		if (player){		
			playerVelocity = Vector3.MoveTowards(playerVelocity, ((player.transform.position - lastPosition) / Time.deltaTime), Time.deltaTime * velocityPredictionAcceleration);
			towardsCamera = (camera.position - player.transform.position).normalized;
			if (hit.transform)
				cameraZoomIn = Mathf.MoveTowards (cameraZoomIn, startCameraDist - hit.distance, 5 * Time.deltaTime);
			else
				cameraZoomIn = Mathf.MoveTowards (cameraZoomIn, 0, 5 * Time.deltaTime);
			cameraZoomIn = Mathf.Clamp (cameraZoomIn, 0, startCameraDist - closestDist);			
			velocityPredition = playerVelocity * velocityPredictionFactor;
			velocityPredition = Vector3.MoveTowards(velocityPredition, playerVelocity * velocityPredictionFactor, Time.deltaTime * velocityPredictionAcceleration); ;
			transform.position = Vector3.Lerp(transform.position, player.transform.position + velocityPredition - towardsCamera * cameraZoomIn, Time.deltaTime * 0.25f);
			lastPosition = player.transform.position;
		}
		          
		rotationEuler.y += Input.GetAxis("Mouse X") * Time.deltaTime * lookSensitivity;
		rotationEuler.x += Input.GetAxis("Mouse Y") * Time.deltaTime * -lookSensitivity;
		rotationEuler.x = Mathf.Clamp(rotationEuler.x, -20, 20);
		transform.rotation = Quaternion.Euler(rotationEuler);
	}

	RaycastHit hit;
	[SerializeField] LayerMask mask;
	void FixedUpdate(){
		if (player){			
			Physics.SphereCast (player.transform.position + Vector3.up, 0.1f, camera.position - player.transform.position, out hit, startCameraDist, mask);
		}
	}
}
