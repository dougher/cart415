using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SavedInput: int {
	A = 0,
	B = 1,
	C = 2,
	D = 3,
}

public class InputSorter: MonoBehaviour {
	public static InputSorter mySorter; //Singleton method to make component easily accesible.
	public TankMovement[] m_TanksInput; //We copy the TankMovements to be able to monitor their inputs.
	//The sorters serve to create a boolean representation for each input to easily link them. They are used
	//together to catch any input change.
	public bool[] m_Sorter;
	public bool[] m_previousSorter;
	public int m_nbOfInputs = 0;
	
	
	
	private bool gameOn = false; //Makes sure it stops inbetween games.
	private int NB_OF_INPUTS = 4;
	//This number is the amount of input being currently registered. Right now, it counts the forward and
	//backward motion as well as turning left and right. Possible implementation: registering the
	//bullet as a 5th.
	
	void Awake(){
		mySorter = this;
	}
	
	// Use this for initialization
	void Start() {
		GameManager manager = GetComponent<GameManager>();

		m_TanksInput = new TankMovement[manager.m_Tanks.Length];
		for (int i = 0; i < manager.m_Tanks.Length; i++){
			m_TanksInput[i] = manager.m_Tanks[i].GetMovement();
		}
		
		m_Sorter = new bool[m_TanksInput.Length * NB_OF_INPUTS]; 
		//We set the size of the sorters with the amount of players times the number of inputs to look out for.
		m_previousSorter = new bool[m_Sorter.Length];
		
		ResetSorter(m_Sorter);
		ResetSorter(m_previousSorter);
	}
	
	public void Update() {

	}
	
	public void Enable(){
		gameOn = true;
	}
	
	public void Disable(){
		gameOn = false;
	}
	
	public void CheckInput(){
		if (gameOn){
			//bool[] m_previousSorter = new bool[m_Sorter.Length];
			
			for (int i = 0; i < m_previousSorter.Length; i++){
				if(m_Sorter[i])
					m_previousSorter[i] = true;
				else
					m_previousSorter[i] = false;
			}
			
			//The bulk of the input sorting is done here. We cycle through the player tanks and verify
			//whether any of their registered input is being activated; keeps track of  each light input
			//in an array  for easy access form the light manager.
			for (int i = 0; i < m_TanksInput.Length; i++){
				int currTankIndex = (i * NB_OF_INPUTS);
		
				//There can only be active boolean per axis (one vertical, one horizontal).
				if (m_TanksInput[i].GetMovementValue() > 0){
					m_Sorter[currTankIndex + (int) SavedInput.A] = true; // Light A or E on
					m_Sorter[currTankIndex + (int) SavedInput.B] = false; // Light B or F off
				} else if (m_TanksInput[i].GetMovementValue() < 0){
					m_Sorter[currTankIndex + (int) SavedInput.B] = true; // Light B on
					m_Sorter[currTankIndex + (int) SavedInput.A] = false;
				} else {
					m_Sorter[currTankIndex + (int) SavedInput.A] = false;
					m_Sorter[currTankIndex + (int) SavedInput.B] = false;
				}
				
				if (m_TanksInput[i].GetTurnValue() > 0){
					m_Sorter[currTankIndex + (int) SavedInput.C] = true;
					m_Sorter[currTankIndex + (int) SavedInput.D] = false;
				} else if (m_TanksInput[i].GetTurnValue() < 0){
					m_Sorter[currTankIndex + (int) SavedInput.D] = true;
					m_Sorter[currTankIndex + (int) SavedInput.C] = false;
				} else {
					m_Sorter[currTankIndex + (int) SavedInput.C] = false;
					m_Sorter[currTankIndex + (int) SavedInput.D] = false;
				}		
			}
		}
	}
	
	//Method called from game manager to see if an input is registered (to avoid running the check even when
	//nothing happens.
	public bool IsThereInput(){
		bool input = false;
		for (int i = 0; i < m_Sorter.Length; i++){
			if (m_Sorter[i]){
				input = true;
				m_nbOfInputs++;
			}
		}
		
		return input;
	}
	
	//Called from the light manager to only change the light once per continuous input. This is where the
	//previousSorter comes in handy.
	public bool DidInputChange(int current){
		bool change = false;
		
		if (m_previousSorter[current] != m_Sorter[current])
			change = true;
		
		return change;
	}
	
	//To avoid the light getting on/off constantly, we set the previousSorter to the current's new value
	//until a change is registered again (e.g. a pressed button being released)
	public void InputJustChanged(int current){
		if (m_Sorter[current])
			m_previousSorter[current] = true;
		else	
			m_previousSorter[current] = false;
	}
	
	public void ResetSorter(bool[] toReset){
		for (int i = 0; i < toReset.Length; i++){
			toReset[i] = false;
		}
	}
}

