using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float m_Speed;

    private GameObject m_Target;

    private Vector2 m_InitialPosition;
    private Vector2 m_InitialTargetPosition;

    private void OnEnable()
    {
        transform.GetChild(0).GetComponent<TrailRenderer>().Clear();
        m_InitialPosition = new Vector2(transform.position.z, transform.position.z);
    }

    private void OnDisable()
    {
        m_Target = null;
        m_InitialTargetPosition = Vector2.zero;
        m_InitialPosition = Vector2.zero;
    }

    void Start()
    {
        // m_InitialPosition = Vector3.zero;
        // m_InitialTargetPosition = Vector3.zero;
        // m_Target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target != null)
        {
            Vector2 _i = (m_InitialTargetPosition - m_InitialPosition).normalized * m_Speed * Time.deltaTime;
            Debug.Log("la" + _i);
            transform.Translate(new Vector3(_i.x, 0, _i.y));
        }
    }

    public void SetTarget(GameObject _target)
    {
        if (_target != null)
        {
            m_Target = _target;
            m_InitialTargetPosition =  new Vector2(m_Target.transform.position.x, m_Target.transform.position.z);
        }
        else
        {
            Debug.Log("null");
        }
    }
}