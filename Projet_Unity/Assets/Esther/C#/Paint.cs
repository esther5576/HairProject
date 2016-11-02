using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Paint : MonoBehaviour {

    
    List<Vector3> m_avLinePoints = new List<Vector3>();
    
    LineRenderer m_pLineRenderer;
    public float m_fStartWidth = 1.0f;
    public float m_fEndWidth = 1.0f;
    public float m_fThreshold = 0.001f;
    Camera m_pCamera;
    int m_iLineCount = 0;

    Vector3 m_vLastPos = Vector3.one * float.MaxValue;
	    
	GameObject _pointer;
	InputSwitch _inputSwitch;

    void Awake()
    {
        m_pCamera = Camera.main;
        m_pLineRenderer = GetComponent<LineRenderer>();
		_pointer = GameObject.Find ("Pointer");
		_inputSwitch = GameObject.Find ("InputSystem").GetComponent<InputSwitch>();
    }

    void Update()
    {
		if (_inputSwitch.hasPointer ()) {
			Draw ();
		}

        UpdateLine();
    }

    void Draw()
    {
		m_vLastPos = _pointer.transform.position;
		if (m_avLinePoints == null)
			m_avLinePoints = new List<Vector3>();
		m_avLinePoints.Add(_pointer.transform.position);
    }

    public void ChangeMaterial(Material _pMat)
    {
        GetComponent<LineRenderer>().material = _pMat;
        
        m_avLinePoints = new List<Vector3>();
    }

    void SavePreviousArray(List<Vector3> _avLinePoints)
    {
        List<Vector3> avTempList = _avLinePoints;
    }

    void UpdateLine()
    {
        m_pLineRenderer.SetWidth(m_fStartWidth, m_fEndWidth);
        m_pLineRenderer.SetVertexCount(m_avLinePoints.Count);

        for (int i = m_iLineCount; i < m_avLinePoints.Count; i++)
        {
            m_pLineRenderer.SetPosition(i, m_avLinePoints[i]);
        }
        m_iLineCount = m_avLinePoints.Count;
    }
}
