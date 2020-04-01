using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabler : MonoBehaviour {

	public GameObject[] objectsToDisable = new GameObject[1];
	void Start () {
		foreach(GameObject go in objectsToDisable)
		{
			go.SetActive(false);
		}
	}
}
