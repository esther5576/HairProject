using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PaintBehavior : MonoBehaviour {

    GameObject m_pDrawManager;
    Camera m_pCamera;

    // Use this for initialization
    void Start () {

        m_pDrawManager = GameObject.Find("DrawManager");
        m_pCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {

        UpdatePosition();
	}

    void UpdatePosition()
    {
        Vector3 vMousePos = Input.mousePosition;
        vMousePos.z = m_pCamera.nearClipPlane + 89;
        Vector3 vMouseWorld = m_pCamera.ScreenToWorldPoint(vMousePos);
        transform.position = vMouseWorld;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Modifier")
        {
            Debug.Log("Hit");
            Material pMat = other.gameObject.GetComponent<Renderer>().material;
            if (m_pDrawManager)
                m_pDrawManager.GetComponent<Paint>().ChangeMaterial(pMat);
        }
    }
}
