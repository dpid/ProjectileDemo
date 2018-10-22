using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperreal
{
    public class CannonController : MonoBehaviour
    {
        [SerializeField]
        Cannon cannon;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cannon.Launch();
            }
        }
    }
}

