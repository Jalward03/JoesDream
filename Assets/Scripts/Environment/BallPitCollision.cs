using Cinemachine;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPitCollision : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			Vector3 dirToPlayer = (gameObject.transform.position - collision.transform.position ).normalized;
			
			if(gameObject.GetComponent<Rigidbody>())
				gameObject.GetComponent<Rigidbody>().AddForce(dirToPlayer * 5, ForceMode.Impulse);
		}
	}
}
