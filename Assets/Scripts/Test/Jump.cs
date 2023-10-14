using System;
using UnityEngine;

namespace Test
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float jump;
        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
                rb.velocity = new Vector3(0, jump, 0);
        }
    }
}