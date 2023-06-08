using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMoving : AiState
{
    private Ray m_BackRay;
    private Ray m_LeftRay;
    private Ray m_RightRay;
    private RaycastHit m_BackHit;
    private RaycastHit m_LeftHit;
    private RaycastHit m_RightHit;
    private Vector3 m_BackObstDir;

    public AiMoving(AIStateMachine stateMachine) : base(stateMachine)
    {
        m_Animator.SetBool(combat, true);
        m_BackObstDir = Vector3.zero;
    }

    public override void UpdateExecute()
    {
        Vector3 playerPosition = player.transform.position;
        m_PlayerDistance = Vector3.Distance(playerPosition, m_Transform.position);
        m_Transform.LookAt(playerPosition);
        if (m_PlayerDistance < m_attackDistance && m_PlayerDistance > m_SafeDistance)
        {
            _AiStateMachine.SetState(new AiDenfending(_AiStateMachine));
        }

        if (m_PlayerDistance > m_TriggerDistance)
        {
            _AiStateMachine.SetState(new AiIdle(_AiStateMachine));
        }
        else if (m_PlayerDistance > m_attackDistance)
        {
            m_Animator.SetInteger(moveState, 1);
            m_NavmeshAgent.destination = playerPosition;
        }
        else if (m_PlayerDistance < m_SafeDistance)
        {
            Vector3 direction;
            Vector3 pointAtSafeDistance;
            Vector3 currentPosition = m_Transform.position;
            m_BackRay = new Ray(currentPosition, -m_Transform.forward);
            m_LeftRay = new Ray(currentPosition, -m_Transform.right);
            m_RightRay = new Ray(currentPosition, m_Transform.right);
            if (Physics.Raycast(m_LeftRay, out m_LeftHit, 4.0f))
            {
                m_BackObstDir = Vector3.zero;
                m_Animator.SetInteger(moveState, 4);

                direction = (currentPosition + m_Transform.right) - currentPosition;
                pointAtSafeDistance = currentPosition + direction.normalized * m_SafeDistance;
            }
            else if (Physics.Raycast(m_RightRay, out m_RightHit, 4.0f))
            {
                m_BackObstDir = Vector3.zero;
                m_Animator.SetInteger(moveState, 3);

                direction = (currentPosition - m_Transform.right) - currentPosition;
                pointAtSafeDistance = currentPosition + direction.normalized * m_SafeDistance;
            }
            else if (Physics.Raycast(m_BackRay, out m_BackHit, 2.0f))
            {
                if (m_BackObstDir == Vector3.zero)
                {
                    int chances = Random.Range(0, 2);
                    m_BackObstDir = chances == 1 ? m_Transform.right : -m_Transform.right;
                }
                m_Animator.SetInteger(moveState, 4);

                direction = (currentPosition + m_BackObstDir) - currentPosition;
                pointAtSafeDistance = currentPosition + direction.normalized * m_SafeDistance;
            }
            else
            {
                m_Animator.SetInteger(moveState, 2);
                Vector3 playerPos = player.transform.position;
                direction = m_Transform.position - playerPosition;
                pointAtSafeDistance = playerPos + direction.normalized * m_SafeDistance - m_Transform.forward;
            }
            m_NavmeshAgent.destination = pointAtSafeDistance;
        }

        m_Transform.LookAt(playerPosition);
    }

    public override void FixedUpdateExecute()
    {
        
    }
}