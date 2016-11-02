using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModifierSpawn : MonoBehaviour {

	public float m_fMinSpawnTime = 5.0f;
	public float m_fMaxSpawnTime = 10.0f;
	public float m_fDistFromCamera = 5.0f;

	private float m_fTimer = 0.0f;
	private float m_fNextTime;

	public List<GameObject> m_apPrefab = new List<GameObject>();

	void Start () {
		m_fNextTime = Random.Range(m_fMinSpawnTime, m_fMaxSpawnTime);    
	}

	void Update () {
		m_fTimer += Time.deltaTime;

		if (m_fTimer > m_fNextTime) {
			int iIndex = Random.Range (0, m_apPrefab.Count);
			Vector3 pos = new Vector3(Random.value, Random.value, m_fDistFromCamera);
			pos = Camera.main.ViewportToWorldPoint(pos);

			Instantiate(m_apPrefab[iIndex], pos, Quaternion.identity);

			Debug.Log("Object created");

			m_fTimer = 0.0f;
			m_fNextTime = Random.Range(m_fMinSpawnTime, m_fMaxSpawnTime);
		}
	}
}
