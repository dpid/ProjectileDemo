using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperreal
{
    class Cannon : MonoBehaviour
    {
        [SerializeField]
        Projectile projectilePrefab;
        void SetProjectilePrefab(Projectile value)
        {
            projectilePrefab = value;
        }

        [SerializeField]
        Transform target;
        Transform GetTarget()
        {
            return target;
        }
        void SetTarget(Transform value)
        {
            target = value;
        }

        const float minLaunchAngle = 20.0f;
        const float maxLaunchAngle = 75.0f;
        const float defaultLaunchAngle = 60.0f;

        [SerializeField]
        [Range(minLaunchAngle, maxLaunchAngle)]
        float launchAngle = defaultLaunchAngle;

        [SerializeField]
        Transform projectileSpawnTransform;

        [ContextMenu("Launch")]
        internal void Launch()
        {
            Vector3 targetXZPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetXZPos);

            Vector3 localEulerAngles = transform.localEulerAngles;
            localEulerAngles.x = launchAngle;
            transform.localEulerAngles = localEulerAngles;

            Projectile projectileInstance = GetProjectileInstance();
            projectileInstance.LaunchAtTarget(target, launchAngle);

        }



        Projectile GetProjectileInstance()
        {
            Projectile projectileInstance = Instantiate(projectilePrefab, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
            projectileInstance.name = "Projectile";
            return projectileInstance;
        }


    }
}