using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour {

	public Transform rainParent;
	public Transform rain;
	
	public float waitTime = 3;

	// Use this for initialization
	void Start () {
		Rain();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Rain(){
		int currSpawn = Random.Range(0, rainParent.childCount);
		GameObject currDrop;
		
		currDrop = Instantiate(rain.gameObject, rainParent.GetChild(currSpawn)); 
		currDrop.transform.parent = null;
		
		StartCoroutine(Wait(waitTime));
	}
	
	IEnumerator Wait(float delay){
		yield return new WaitForSeconds(delay);
		
		Rain();
	}
}
