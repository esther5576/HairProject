using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour 
{

	public GameObject _lacets;
	public GameObject _pointer;

	GameObject _lacetsInstantiate;
	GameObject _pointerInstantiate;

	Vector3 offset;
	Vector3 screenPoint;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			_pointerInstantiate = GameObject.Instantiate (_pointer, Vector3.zero, Quaternion.identity) as GameObject;

			_lacetsInstantiate = GameObject.Instantiate (_lacets, Input.mousePosition, Quaternion.identity) as GameObject;
			_lacetsInstantiate.GetComponent<Kvant.WigController> ().target = _pointerInstantiate.transform;
			_lacetsInstantiate.GetComponent<Kvant.WigController> ().Reset ();
			_lacetsInstantiate.GetComponent<Kvant.WigController> ().ResetSimulation ();
		}
		if (Input.GetMouseButton (0)) 
		{
			Debug.Log (">>>>> " + Input.mousePosition);
		}
		if (Input.GetMouseButtonUp (0)) 
		{
			_lacetsInstantiate = null;
			_pointerInstantiate = null;
		}
	}
}
