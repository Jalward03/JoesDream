using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using WipeOut;

public class PodiumTrigger : MonoBehaviour
{
	public GameObject player;
	public GameObject exitButton;
	public SpaceZone spaceZone;
	public Transform newCamTransform;
	public Transform newPlayerTransform;


	
	private Camera cam;
	private bool hasTriggered;
	private bool canExit;
	private bool canConfetti;
	private CameraController cameraController;
	private CharacterMover characterMover;
	

	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
		cameraController = cam.GetComponent<CameraController>();
		characterMover = player.GetComponent<CharacterMover>();
	}

	void StopMovement()
	{
		cameraController.canTurn = false;
		cameraController.canLookAtPlayer = false;
		
		characterMover.canMove = false;
		
	
		
	}

	private IEnumerator CameraPan()
	{
		hasTriggered = true;

		float t = 0;
		float panTime = 1.0f;

		Vector3 startPos = cam.transform.position;
		Vector3 endPos = newCamTransform.position;

		Quaternion startRot = cam.transform.rotation;
		Quaternion endRot = newCamTransform.rotation;

		while(t < 1.0f * 1.1f)
		{
			cam.transform.position = Vector3.Lerp(startPos, endPos, t / panTime);
			cam.transform.rotation = Quaternion.Slerp(startRot, endRot, t / panTime);

			yield return null;

			t += Time.deltaTime;
		}

		canExit = true;
		canConfetti = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		player.transform.position = newPlayerTransform.position;
		player.transform.rotation = newPlayerTransform.rotation;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && !hasTriggered)
		{
			StopMovement();
			StartCoroutine(CameraPan());
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(canExit)
			exitButton.SetActive(true);
	}

	public void OnClickConfetti(ParticleSystem confetti)
	{
		if(canConfetti)
		{
			
			confetti.Play();
		}
	}
}