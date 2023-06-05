using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIStateMachine : MonoBehaviour
{
    public float HP;

    private Camera m_MainCamera;

    private GameObject player;
    [SerializeField] private Transform m_Bullet;
    [SerializeField] private Transform m_BulletSpawner;
    [SerializeField] private string m_DamageTag;

    [SerializeField] private string m_BulletTag;

    private Rigidbody m_Rigidbody;
    private NavMeshAgent m_NavmeshAgent;
    private Animator m_Animator;

    private float m_PlayerDistance;
    [SerializeField] private float m_TriggerDistance;
    [SerializeField] private float m_attackDistance;

    private bool m_IsStabbing;
    private bool m_OutOfRange;

    [SerializeField] private Canvas m_AiCanvas;
    [SerializeField] private Slider m_HealthBar;

    [SerializeField] private float m_Cooldown;
    private float m_CooldownElapsed;

    private Outline m_OutlineScript;
    
    AiState _currentState;
    
    public void SetState(AiState state)
    {
        _currentState = state;
    }
    void Start()
    {
        Init();
    }

    private void Init()
    {
        m_CooldownElapsed = 0;
        HP = 100;
        player = GameObject.Find("Player");
        m_Rigidbody = GetComponent<Rigidbody>();
        m_NavmeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_IsStabbing = false;
        m_OutOfRange = true;
        m_HealthBar.value = HP / 100;
        m_MainCamera = Camera.main;
        m_OutlineScript = GetComponent<Outline>();
        m_OutlineScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateExecute();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateExecute();
    }
}
