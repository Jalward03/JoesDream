using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using WipeOut;

using Cursor = UnityEngine.Cursor;

namespace WipeOut
{
	public class SkinSelection : MonoBehaviour
	{
		[SerializeField] private CameraController cameraController;
		[SerializeField] private CharacterMover charMover;

		public Camera cam;
		public Transform endTransform;
		public Transform startTransform;

		public GameObject hud;
		public TimeTrial timer;
		public GameObject partyHat;
		public GameObject propellerHat;
		public GameObject cowboyHat;
		public GameObject sombreroHat;

		
		public Slider redSlider;
		public Slider greenSlider;
		public Slider blueSlider;
		public Slider metallicSlider;
		public Slider smoothnessSlider;

		public TextMeshProUGUI redText;
		public TextMeshProUGUI greenText;
		public TextMeshProUGUI blueText;
		public TextMeshProUGUI metallicText;
		public TextMeshProUGUI smoothText;

		void Awake()
		{
			cameraController.canTurn = false;
			cameraController.canLookAtPlayer = false;
			charMover.canMove = false;
			cam = Camera.main;
			startTransform = Camera.main.transform;
		}

		private void Update()
		{
			if(GetCurrentHat() != null)
			{
				GameObject currentHat = GetCurrentHat();
				Material currentMat = currentHat.GetComponent<MeshRenderer>().material;

				//currentMat.color = new Color(redSlider.value, greenSlider.value, blueSlider.value);
				currentMat.SetColor("_Color",new Color(redSlider.value, greenSlider.value, blueSlider.value));
				currentMat.SetFloat("_Metallic", metallicSlider.value);
				currentMat.SetFloat("_Smoothness", smoothnessSlider.value);
			}
			SliderValueUpdate();

		}

		public void SliderValueUpdate()
		{
			redText.text = (redSlider.value * 255).ToString("0");
			greenText.text = (greenSlider.value * 255).ToString("0");
			blueText.text = (blueSlider.value * 255).ToString("0");

			metallicText.text = metallicSlider.value.ToString("F2");
			smoothText.text = smoothnessSlider.value.ToString("F2");
		}
		public GameObject GetCurrentHat()
		{
			if(partyHat.activeSelf)
			{
				return partyHat;
			}

			if(propellerHat.activeSelf)
			{
				return propellerHat;
			}

			if(cowboyHat.activeSelf)
			{
				return cowboyHat;
			}

			if(sombreroHat.activeSelf)
			{
				return sombreroHat;
			}

			return null;
		}

		public void ChangeHatOnClick(GameObject hat)
		{
			RemoveAllHats();
			hat.SetActive(true);
			redSlider.value = 1;
			blueSlider.value = 1;
			greenSlider.value = 1;
			metallicSlider.value = 0;
			smoothnessSlider.value = 0.5f;
		}

		public void RemoveHatOnClick()
		{
			RemoveAllHats();
			
		}

		public void StartGameOnClick()
		{
			charMover.canMove = true;
			cameraController.canTurn = true;
			cameraController.canLookAtPlayer = true;
			cam.transform.position = endTransform.position;
			cam.transform.rotation = endTransform.rotation;

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			hud.SetActive(true);
			timer.timerCanStart = true;
			gameObject.SetActive(false);
		}

		public void RemoveAllHats()
		{
			propellerHat.SetActive(false);
			partyHat.SetActive(false);
			cowboyHat.SetActive(false);
			sombreroHat.SetActive(false);
		}
		
	}
}