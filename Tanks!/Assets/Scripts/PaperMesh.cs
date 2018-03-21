using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperMesh : MonoBehaviour {
	public Transform m_master;
	public Transform m_pivot;
	public Texture[] m_idle;
	
	//public int m_currImg = 0;
	//public bool m_firstMovement = true;
	
	private Renderer m_rend;
	

	void Start(){
		m_rend = GetComponent<Renderer>();

		switch (m_master.gameObject.GetComponent<TankMovement>().m_PlayerNumber){
			case 1:
				m_rend.material.SetTexture("_MainText", m_idle[0]);
				break;
			case 2:
				m_rend.material.SetTexture("_MainText", m_idle[1]);
				break;
			case 3:
				m_rend.material.SetTexture("_MainText", m_idle[2]);
				break;
			case 4:
				m_rend.material.SetTexture("_MainText", m_idle[3]);
				break;
		}
	}
	
	void Update () {
		if (m_pivot.position != m_master.position)
			m_pivot.position = m_master.position;
	}
	
	/*
	IEnumerator Animation(){
		yield return new WaitForSeconds(0.5f);
		
		if (m_currImg < m_moving.Length){
			m_rend.material.SetTexture("_MainText", m_moving[m_currImg]);
			m_currImg++;
		} else {
			m_currImg = 0;
		}
		
		StartCoroutine(Animation());
	}*/
	
}
