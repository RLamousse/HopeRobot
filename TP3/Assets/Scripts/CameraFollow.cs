using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;


        private Animator m_Anim;            // Reference to the player's animator component.

        // Use this for initialization
        private void Start()
        {

            m_Anim = target.GetComponent<Animator>();
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {


            Debug.Log(target.position);

            // only update lookahead pos if accelerating or changed direction
            float zMoveDelta = (target.position - m_LastTargetPosition).z;

            bool updateLookAheadTarget = Mathf.Abs(zMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(zMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            //if (!m_Anim.GetBool("Ground"))
            //{
            //    newPos.y = transform.position.y;
            //}

            newPos.y = transform.position.y;

            transform.position = newPos;
            m_LastTargetPosition = target.position;
        }
    }
}
