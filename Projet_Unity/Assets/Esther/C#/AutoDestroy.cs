using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public float m_fLifespan = 20f;
	float m_fActualLifespan;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (m_fActualLifespan < m_fLifespan) {
			m_fActualLifespan += Time.deltaTime;
		} else if (m_fActualLifespan >= m_fLifespan)
			Destroy (this.gameObject);
	}
}
