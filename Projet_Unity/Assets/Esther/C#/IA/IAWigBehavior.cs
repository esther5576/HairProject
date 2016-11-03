using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IAWigBehavior : MonoBehaviour {

	public float m_fMoveSpeed = 1f;
	public float m_fDepth = 10f;
	Vector3 m_vDestination;

	List<Vector3> m_avLinePoints = new List<Vector3>();

	LineRenderer m_pLineRenderer;
	public float m_fStartWidth = 1.0f;
	public float m_fEndWidth = 1.0f;
	public float m_fThreshold = 0.001f;
	Camera m_pCamera;
	int m_iLineCount = 0;

	Vector3 m_vLastPos = Vector3.one * float.MaxValue;

	public float m_fLifespan = 10f;
	float m_fActualLifeSpan;

	void Awake()
	{
		m_pCamera = Camera.main;
		m_pLineRenderer = GetComponent<LineRenderer>();

	}

	// Use this for initialization
	void Start () {
	
		m_vDestination = GetNewScreenPos ();
		GetComponent<LineRenderer> ().material = GetPlayerMat ();
		m_fActualLifeSpan = 0;

	}
	
	// Update is called once per frame
	void Update () {
	
		CheckPos ();
		MoveToPoint ();
		Draw ();
		UpdateLine();
		Live ();
		
	}

	Vector3 GetNewScreenPos()
	{
		int iX = Random.Range (0, Screen.width);;
		int iY = Random.Range (0, Screen.height);;
		Vector3 vNewPos = m_pCamera.ScreenToWorldPoint(new Vector3 (iX, iY, m_fDepth));
		Debug.Log ("vNewPos >>>>" + vNewPos);
		return vNewPos;
	}

	void MoveToPoint()
	{
		transform.position = Vector3.Lerp (transform.position, m_vDestination, Time.deltaTime * m_fMoveSpeed);
	}

	void CheckPos()
	{
		if (Vector3.Distance (transform.position, m_vDestination) <= 1) {
		
			m_vDestination = GetNewScreenPos ();
		}
	}

	void Draw()
	{
		m_vLastPos = transform.position;
		if (m_avLinePoints == null)
			m_avLinePoints = new List<Vector3>();
		m_avLinePoints.Add(transform.position);
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

	void Live()
	{
		m_fActualLifeSpan += Time.deltaTime;
		if (m_fActualLifeSpan >= m_fLifespan)
			StartCoroutine (FadeInLife ());
	}

	Material GetPlayerMat()
	{
		Material pMat = GameObject.Find ("Pointer").GetComponent<PointerPaintBehaviour> ()._savedMaterial;
		return pMat;
	}

	IEnumerator FadeInLife()
	{
		Color pColor = GetComponent<Renderer> ().material.color;
		pColor.a = Mathf.Lerp (pColor.a, 0, Time.deltaTime);
		GetComponent<Renderer> ().material.color = pColor;
		yield return new WaitForSeconds (2f);
		Destroy (this.gameObject);
	}
}
