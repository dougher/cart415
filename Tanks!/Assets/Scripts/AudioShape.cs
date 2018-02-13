using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioShape : MonoBehaviour {
	public bool m_collided = false;
	
	private const int WAIT_TIME = 500;
	private Rigidbody m_rb;
	private AudioSource m_mySound;
	private bool m_soundOn = false;
	// Use this for initialization
	void Start () {
		m_mySound = GetComponent<AudioSource>();
		m_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (InMovement()){
			if (!m_soundOn){
				m_mySound.enabled = true;
				m_mySound.Play();
				m_soundOn = true;
			}
		} else {
			StartCoroutine(Wait());
		}
	}
	
	bool InMovement(){
		return m_rb.transform.position.x < 0.1f || m_rb.transform.position.x > 0.1f || 
				m_rb.transform.position.y < 0.1f || m_rb.transform.position.y > 0.1f ||
				m_rb.transform.position.z < 0.1f || m_rb.transform.position.z > 0.1f;
	}
	
	IEnumerator Wait(){
		bool stillWaiting = true;
		
		while (stillWaiting){
			yield return new WaitForSeconds(WAIT_TIME);
			
			if (!InMovement()){
				m_mySound.enabled = false;
				m_soundOn = false;
				
				stillWaiting = false;
			}
		}
	}
	
	/*
	void OnCollisionStay(Collision collider){
		if (collider.transform.position.tag == "Player"){
			m_collided = true;
		}
	}
	
	void OnCollisionExit(Collision collider){
		if (collider.transform.position.tag == "Player"){
			m_collided = false;
		}
	}
	*/
}
