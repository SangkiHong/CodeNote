using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SK
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                Instantiate(bulletPrefab, transform.position, transform.rotation);
            }
        }
    }
}
