using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float m_Speed;

    private GameObject m_Target;

    private Vector3 m_InitialPosition;

    private Vector3 m_HeightOffset;

    private Vector3 m_InitialTargetVelocity;
    private Rigidbody m_Rigidbody;
    private float m_InitialDistance;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        m_Target = null;
    }

    private void OnEnable()
    {
        transform.GetChild(0).GetComponent<TrailRenderer>().Clear();
        
    }

    void Update()
    {
        if (m_Target != null)
        {
            
            Vector3 currentPos = transform.position;
            Vector3 currentTargetPos = m_Target.transform.position + m_HeightOffset;
            Vector3 newVelocity = (currentTargetPos - currentPos).normalized;
            
            transform.LookAt(currentPos + newVelocity);

            
            Vector3 IT = currentTargetPos - m_InitialPosition;
            Vector3 IC = currentTargetPos - m_InitialPosition;

            float IT_magnitude = IT.magnitude;
            Vector3 IT_norm = IT / IT_magnitude;

            float distance = Vector3.Dot(IC, IT_norm);

            distance = Mathf.Abs(distance);

            float t = (m_InitialDistance - distance) / m_InitialDistance;
            
            Debug.Log(t);
            
            m_Rigidbody.velocity = Vector3.Lerp(m_InitialTargetVelocity, transform.forward * m_Speed, t);
            
            if (Vector3.Distance(transform.position, m_Target.transform.position) <= 1.0f || Vector3.Distance(transform.position, m_InitialPosition) > 10.0f)
            {
                LevelManager.instance.ToggleInactive(gameObject);
            }
        }
    }

    public void SetTarget(GameObject _target, Vector3 _pos)
    {
        if (_target != null)
        {
            m_Target = _target;
            m_HeightOffset = new Vector3(0, transform.position.y, 0);
            Vector3 targetPos = m_Target.transform.position + m_HeightOffset;
            m_InitialPosition = _pos;
            m_InitialDistance = Mathf.Abs(Vector3.Distance(_pos, targetPos));
            m_InitialTargetVelocity = (targetPos - _pos).normalized + (new Vector3(180, 0, 0).normalized);
        }
        else
        {
            Debug.Log("null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            LevelManager.instance.ToggleInactive(gameObject);
        }
    }
}