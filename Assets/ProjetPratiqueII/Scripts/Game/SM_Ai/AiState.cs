using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class AiState
{
    AIStateMachine _AiStateMachine;
    
    // SERIALIZED
    private Transform m_Bullet;
    private Transform m_BulletSpawner;
    private string m_DamageTag;
    private string m_BulletTag;
    private float m_TriggerDistance;
    private float m_attackDistance;
    private Canvas m_AiCanvas;
    private Slider m_HealthBar;
    private float m_Cooldown;
    
    // MEMBERS
    public float HP;
    private Camera m_MainCamera;
    private GameObject player;
    private Rigidbody m_Rigidbody;
    private NavMeshAgent m_NavmeshAgent;
    private Animator m_Animator;
    private float m_PlayerDistance;
    private bool m_IsStabbing;
    private bool m_OutOfRange;
    private float m_CooldownElapsed;
    private Outline m_OutlineScript;
    
    public AiState(AIStateMachine stateMachine)
    {
        Init(stateMachine);
    }

    private void Init(AIStateMachine _stateMachine)
    {
        _AiStateMachine = _stateMachine;
    }
    public abstract void UpdateExecute();
    public abstract void FixedUpdateExecute();

    private void LoadSerializable(AIStateMachine _stateMachine)
    {
        
    }

    private void LoadComponents(AIStateMachine _stateMachine)
    {
        
    }

    private void LoadMembers(AIStateMachine _stateMachine)
    {
        
    }
}
