﻿using System.Collections;
using Units.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Units.Enemy
{
    
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private float speed;
        [SerializeField] private float TimeToRevert;
        [SerializeField] private GameObject spine;
        private float currentTimeToRevert;
        private Rigidbody rb;
        private int i;
        private bool faceRight = false;
        private Quaternion currentQuaternion;
        private NavMeshAgent agent;
        public static bool CanMove = true;

        private const int IdleState = 0;
        private const int WalkState = 1;
        private const int RevertState = 2;
        private const int Detected = 3;
        
        private int currentState;
        private int previusState;

        private bool isOnDetected = false;
        private bool playerDetected = false;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            currentState = WalkState;
        }

        private void Update()
        {
            if (currentTimeToRevert >= TimeToRevert)
            {
                currentState = RevertState;
                currentTimeToRevert = 0;
            }

            switch (currentState)
            {
                case IdleState:
                    EnemyEvents.onWalked?.Invoke(false);
                    currentTimeToRevert += Time.deltaTime;
                    if (i == points.Length)
                    {
                        i = 0;
                    }

                    break;
                case WalkState:
                    agent.destination = points[i].position;
                    EnemyEvents.onWalked?.Invoke(true);
                    if (agent.remainingDistance < 0.1f)
                    {
                        print(agent.remainingDistance);
                        currentState = IdleState;
                        print("IdleState");
                        i++;
                    }

                    break;
                case RevertState:
                    speed *= -1;
                    currentState = WalkState;
                    print("walkState");
                    FLip();
                    break;
                case Detected:
                    if (!isOnDetected)
                        StartCoroutine(OnDetected());
                    break;
            }
        }

        private void OnDeath()
        {
            Destroy(GetComponent<Health>());
            Destroy(GetComponent<EnemyAttack>());
            Destroy(this);
        }

        private IEnumerator OnDetected()
        {
            isOnDetected = true;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(1);
            }
            if(playerDetected)
                EnemyEvents.onFired?.Invoke();
            isOnDetected = false;
            StopCoroutine(OnDetected());
        }
        private void FLip()
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.x,rot.y+180,rot.z);
            transform.rotation = Quaternion.Euler(rot);
            faceRight = !faceRight; 
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerDetected = true;
                spine.transform.LookAt(other.transform);
                previusState = currentState;
                currentQuaternion = transform.rotation;
                EnemyEvents.onWalked?.Invoke(false);
                EnemyEvents.onPlayerDetect?.Invoke();
                transform.LookAt(other.transform);
                agent.isStopped = true;
                currentState = Detected;
                print("Enemy event: True");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EnemyEvents.onWalked?.Invoke(true);
                playerDetected = false;
                StopCoroutine(OnDetected());
                EnemyEvents.onPlayerUndetect?.Invoke();
                transform.rotation = currentQuaternion;
                agent.isStopped = false;
                currentState = previusState;
            }
        }
    }
}