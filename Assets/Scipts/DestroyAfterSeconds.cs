using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperreal
{
    public class DestroyAfterSeconds : MonoBehaviour
    {
        [SerializeField]
        float seconds;

        void OnEnable()
        {
            CancelInvoke();
            Invoke("SelfDestruct", seconds);
        }

        void SelfDestruct()
        {
            Destroy(gameObject);
        }
    }
}