using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour 
{

	GameObject _creature;
	Kvant.WigController _creatureController;

	GameObject _pointer;

	InputSwitch _inputSwitch;

	[Range(0.1f,20)]
	[Tooltip("Follow speed of creature")]
	public float m_fMoveSpeed = 1f;

	[Range(0.1f,20)]
	[Tooltip("Death Speed of creature")]
	public float _creatureDeathSpeed = 1f;

	bool Once;

	// Use this for initialization
	void Start () 
	{
		_inputSwitch = GameObject.Find ("InputSystem").GetComponent<InputSwitch> ();
		_creature = GameObject.Find ("Creature");
		_creatureController = _creature.GetComponent<Kvant.WigController> ();
		_pointer = GameObject.Find ("Pointer");
	}

	// Update is called once per frame
	void Update () 
	{
		UpdateCreaturePosition ();

		if (_inputSwitch.hasPointer ()) {
			if (Once == true) {
				_creatureController.length = 0.01f;
				_creatureController.template._filamentCount = 10;
				_creatureController.Reset ();
				Once = false;
			}

			if (_creatureController.length < 5) {
				_creatureController.length += Time.deltaTime * 10;
			}
		} else {
			if (Once == false) {
				_creatureController.length = 0.01f;
				_creatureController.template._filamentCount = 1;
				_creatureController.Reset ();
				Once = true;
			}

			if (_creatureController.length < 5) {
				_creatureController.length += Time.deltaTime * 10;
			}
		}
	}

	public void UpdateCreaturePosition()
	{
		if ((_inputSwitch.getLasersPositions () [0].x > -9 && _inputSwitch.getLasersPositions () [0].x < 9) && (_inputSwitch.getLasersPositions () [0].y > -5 && _inputSwitch.getLasersPositions () [0].y < 5)) 
		{
			_pointer.transform.position = Vector3.Lerp(_pointer.transform.position,_inputSwitch.getLasersPositions () [0],Time.deltaTime *m_fMoveSpeed);
		}
	}
}
