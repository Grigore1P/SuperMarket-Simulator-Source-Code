using System.Collections;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] private Transform RayOrigin;
    public float speed = 50f;
    public float rotationSpeed = 2f; // Adjusted for smoother rotation
    [SerializeField] private int currentWaypointIndex = 1;
    [SerializeField] private bool move = true;
    public bool Move => move;
    Vector3 targetPosition;
    [SerializeField] private LayerMask playerLayer;


    void Start()
    {
        targetPosition = waypoints[currentWaypointIndex].position;
    }

    void Update()
    {
        if (Physics.Raycast(RayOrigin.position, transform.forward, out RaycastHit hit, 6f, playerLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                move = false;
            }
        }
        else
        {
            move = true;
        }
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        if (move)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 4f)
            {
                Vector3 pose = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                transform.position = Vector3.MoveTowards(transform.position, pose, speed * Time.deltaTime);
            }
            else
            {
                currentWaypointIndex++;
                if (currentWaypointIndex == waypoints.Length)
                {
                    currentWaypointIndex = 1;
                    transform.position = new Vector3(waypoints[0].position.x, transform.position.y, waypoints[0].position.z);
                    targetPosition = waypoints[currentWaypointIndex].position;
                }
                else
                {
                    targetPosition = waypoints[currentWaypointIndex].position;
                }
            }

            // Smoothly rotate towards the target position
            Vector3 rotationPose = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            Vector3 direction = (rotationPose - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
        // Move towards the target position
    }
    void OnDrawGizmos()
    {
        // Draw the raycast line in the editor for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawRay(RayOrigin.position, transform.forward * 3f);
    }
}
