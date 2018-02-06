using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager: MonoBehaviour {
	//This simple class deals with the turning on or off of the lighting scheme depending on the lengthier
	//InputSorter's results.
	
	public static LightManager myManager; //Singleton method to make component easily accesible.
	public Light m_Sun;
	public Light A;
	public Light B;
	public Light C;
	public Light D;
	public Light E;
	public Light F;
	public Light G;
	public Light H;
	public Light[] m_Lights;
	
	private float initIntensity;

	void Awake(){
		myManager = this;
		
		initIntensity = m_Sun.intensity;
		
		m_Lights = new Light[8];
		m_Lights[0] = A;
		m_Lights[1] = B;
		m_Lights[2] = C;
		m_Lights[3] = D;
		m_Lights[4] = E;
		m_Lights[5] = F;
		m_Lights[6] = G;
		m_Lights[7] = H;
		//Must find a way to automate this...
		//through string probbly?
	}
	
	void Start(){
		ResetLightScheme();
	}
	
	void Update(){

	}
	
	public void SortLightScheme(){
		if (!IsSunDisabled())
			DisableSun();
	
		for (int i = 0; i < m_Lights.Length; i++){
			if (InputSorter.mySorter.DidInputChange(i)){ //We check if the sorter has changed from before.
				m_Lights[i].enabled = !m_Lights[i].enabled; //If so, we inverse its state.This is to allow in the future 
				//the implementation of the light being affected dually by the players'inputs.
				InputSorter.mySorter.InputJustChanged(i); //We copy this new change to the sorter copy to
				//avoid seizure spotlights.
			}
		}
	}
	
	//Reset method that is used externally when all inputs stop being registered, followed by simple
	//implementation of light enabling and disabling methods.
	public void ResetLightScheme(){
		DisableLights();
		EnableSun();
		
		InputSorter.mySorter.m_nbOfInputs = 0;
	}
	
	private void DisableLights(){
		for (int i = 0; i < m_Lights.Length; i++){
			m_Lights[i].enabled = false;
		}
	}
	
	private void EnableSun(){
		m_Sun.intensity = initIntensity;
	}
	
	private void DisableSun(){
		m_Sun.intensity = m_Sun.intensity / 2;
	}
	
	private bool IsSunDisabled(){
		return (m_Sun.intensity != initIntensity); //We use the return boolean to see whether the sun is at the same
		//intensity as at the beginning.
	}
}