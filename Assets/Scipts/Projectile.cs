using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperreal
{
    [RequireComponent(typeof(Rigidbody))]
    class Projectile : MonoBehaviour
    {
        [SerializeField]
        bool isLookingAtVelocityDirection;

        new Rigidbody rigidbody;

        bool isCollided;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            isCollided = true;
        }

        internal void LaunchAtTarget(Transform target, float launchAngle)
        {
            Vector3 projectileXZPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 targetXZPos = new Vector3(target.position.x, transform.position.y, target.position.z);

            transform.LookAt(targetXZPos);

            float distanceXZ = Vector3.Distance(projectileXZPos, targetXZPos);
            float gravity = Physics.gravity.y;
            float tanAlpha = Mathf.Tan(launchAngle * Mathf.Deg2Rad);
            float distanceY = target.position.y - transform.position.y;

            float Vz = Mathf.Sqrt(gravity * distanceXZ * distanceXZ / (2.0f * (distanceY - distanceXZ * tanAlpha)));
            float Vy = tanAlpha * Vz;

            Vector3 localVelocity = new Vector3(0f, Vy, Vz);
            Vector3 globalVelocity = transform.TransformDirection(localVelocity);

            rigidbody.velocity = globalVelocity;

            if (isLookingAtVelocityDirection)
            {
                StartUpdatingRotation();
            }
        }

        void StartUpdatingRotation()
        {
            StopAllCoroutines();
            StartCoroutine(UpdateRotationCoroutine());
        }

        IEnumerator UpdateRotationCoroutine()
        {
            while (isCollided == false)
            {
                LookAtVelocityDirection();
                yield return new WaitForEndOfFrame();
            }
        }

        void LookAtVelocityDirection()
        {
            transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
        }
    }
}

