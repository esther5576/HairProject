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

    public GameObject m_pDrawer;
    

    //public List<Material> m_apMaterial = new List<Material>();


    void Awake()
    {
        m_pCamera = Camera.main;
        m_pLineRenderer = m_pDrawer.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
            Draw();
            

        UpdateLine();
    }

    void Draw()
    {
        Vector3 vMousePos = Input.mousePosition;
        vMousePos.z = m_pCamera.nearClipPlane + 89;
        Vector3 vMouseWorld = m_pCamera.ScreenToWorldPoint(vMousePos);

        float fDist = Vector3.Distance(m_vLastPos, vMouseWorld);
        if (fDist <= m_fThreshold)
            return;

        m_vLastPos = vMouseWorld;
        if (m_avLinePoints == null)
            m_avLinePoints = new List<Vector3>();
        m_avLinePoints.Add(vMouseWorld);
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
