using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {

	public float speed = 2f;
	public Transform target;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt (target.position);
		this.transform.Translate (0, 0, speed * Time.deltaTime);
	}
}
