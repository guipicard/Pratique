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

    public AiMoving(AIStateMachine stateMachine) : base(stateMachine)
    {
        m_Animator.SetBool(running, true);
        m_Animator.SetBool(combat, true);
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
            m_BackRay = new Ray(m_Transform.position, -m_Transform.forward);
            m_LeftRay = new Ray(m_Transform.position, -m_Transform.right);
            m_RightRay = new Ray(m_Transform.position, m_Transform.right);
            if (Physics.Raycast(m_LeftRay, out m_LeftHit, 4.0f))
            {
                m_Animator.SetInteger(moveState, 4);

                Vector3 currentPos = m_Transform.position;
                direction = (currentPos + m_Transform.right) - currentPos;
                pointAtSafeDistance = currentPos + direction.normalized * m_SafeDistance;
            }
            else if (Physics.Raycast(m_RightRay, out m_RightHit, 4.0f))
            {
                m_Animator.SetInteger(moveState, 3);

                Vector3 currentPos = m_Transform.position;
                direction = (currentPos - m_Transform.right) - currentPos;
                pointAtSafeDistance = currentPos + direction.normalized * m_SafeDistance;
            }
            else if (Physics.Raycast(m_BackRay, out m_BackHit, 2.0f))
            {
                m_Animator.SetInteger(moveState, 4);

                Vector3 currentPos = m_Transform.position;
                direction = (currentPos + m_Transform.right) - currentPos;
                pointAtSafeDistance = currentPos + direction.normalized * m_SafeDistance;
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