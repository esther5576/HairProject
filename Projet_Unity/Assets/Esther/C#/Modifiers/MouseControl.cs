using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseControl : MonoBehaviour {

	public float m_fSpriteDepth = 1f;


	// Use this for initialization
	void Start () {
	

	}

    
	
	// Update is called once per frame
	void Update () {
	
		UpdateMousePos ();
	}

	void UpdateMousePos()
	{
		transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + m_fSpriteDepth));

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Modifier") {
				
			GetComponent<LineRenderer> ().material = other.gameObject.GetComponent<MeshRenderer> ().material;
			Debug.Log (other.gameObject.GetComponent<MeshRenderer> ().material.name);

		}
	}



}
