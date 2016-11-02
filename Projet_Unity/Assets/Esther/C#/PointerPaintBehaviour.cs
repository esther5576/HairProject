using UnityEngine;
using System.Collections;

public class PointerPaintBehaviour : MonoBehaviour 
{
	public GameObject _drawManagerPref;
	GameObject _drawManagerAct;
	InputSwitch _inputSwitch;

	bool _doneOncePointer;

	public Material _savedMaterial;

	// Use this for initialization
	void Start () {
		_drawManagerAct = GameObject.Find ("DrawManager");
		_inputSwitch = GameObject.Find ("InputSystem").GetComponent<InputSwitch> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_inputSwitch.hasPointer () && !_doneOncePointer) {
			_doneOncePointer = true;

			Debug.Log ("Change ME");

			_drawManagerAct.GetComponent<Paint> ().enabled = false;
			_drawManagerAct.name = "OldDrawManager";
			_drawManagerAct = Instantiate (_drawManagerPref, _drawManagerAct.transform.position, Quaternion.identity) as GameObject;
			_drawManagerAct.GetComponent<LineRenderer> ().material = _savedMaterial;

		} else if(!_inputSwitch.hasPointer () && _doneOncePointer) {
			_doneOncePointer = false;

		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Modifier") 
		{
			_savedMaterial = col.GetComponent<Renderer>().material;

			Debug.Log ("Change ME");
			_drawManagerAct.GetComponent<Paint> ().enabled = false;
			_drawManagerAct.name = "OldDrawManager";
			_drawManagerAct = Instantiate (_drawManagerPref, _drawManagerAct.transform.position, Quaternion.identity) as GameObject;
			_drawManagerAct.GetComponent<LineRenderer> ().material = _savedMaterial;
			Destroy (col.gameObject);
		}
	}
}
