using Units.Abstract;
using Units.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
        [SerializeField] private Transform[] points;
        [SerializeField] private Transform player;
        [SerializeField] private float speed;
        [SerializeField] private float TimeToRevert;
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
        private const int Attack = 4;
        
        private int currentState;
        private int previusState;

        private bool isOnDetected = false;
        private bool playerDetected = false;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            currentState = WalkState;
            agent.speed = 5;
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
                    agent.isStopped = false;
                    agent.destination = points[i].position;
                    EnemyEvents.onWalked?.Invoke(true);
                    if (agent.remainingDistance < 0.1f)
                    {
                        currentState = IdleState;
                        i++;
                    }

                    break;
                case RevertState:
                    speed *= -1;
                    currentState = WalkState;
                    FLip();
                    break;
                case Detected:
                    if (!isOnDetected)
                    {
                        EnemyEvents.onZombieDeatectPlayer?.Invoke();
                        agent.destination = player.position;
                        if (agent.remainingDistance < 0.1f)
                            currentState = Attack;
                    }
                    break;
                case Attack:
                {
                    agent.isStopped = true;
                    EnemyEvents.onZombieAttack?.Invoke();
                    break;
                }
            }
        }

        private void OnDeath()
        {
            Destroy(GetComponent<Health>());
            Destroy(GetComponent<EnemyAttack>());
            Destroy(this);
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
                previusState = currentState;
                currentQuaternion = transform.rotation;
                EnemyEvents.onPlayerDetect?.Invoke();
                transform.LookAt(other.transform);
                currentState = Detected;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EnemyEvents.onWalked?.Invoke(true);
                playerDetected = false;
                EnemyEvents.onPlayerUndetect?.Invoke();
                transform.rotation = currentQuaternion;
                currentState = previusState;
            }
        }
}
