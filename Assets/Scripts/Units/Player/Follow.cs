using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.position;
    }

   
    void Update()
    {
        transform.position = player.position + offset;
    }
}
