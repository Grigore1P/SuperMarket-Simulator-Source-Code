using System.Collections;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] private Transform RayOrigin;
    [SerializeField] private Transform RayOrigin1;
    [SerializeField] private Transform RayOrigin2;
    public float speed = 50f;
    public float rotationSpeed = 2f; // Adjusted for smoother rotation
    [SerializeField] private int currentWaypointIndex = 1;
    [SerializeField] private bool move = true;
    public bool Move => move;
    Vector3 targetPosition;
    [SerializeField] private LayerMask playerLayer;
    public YelbAudioManager yelbAudio;
  

    void Start()
    {
        targetPosition = waypoints[currentWaypointIndex].position;
    }

    void Update()
    {
        bool wasMoving = move;

        if (Physics.Raycast(RayOrigin.position, RayOrigin.forward, out RaycastHit hit, 6f, playerLayer))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Car"))
            {
                move = false;
                if (wasMoving && hit.collider.CompareTag("Player"))
                {
                    yelbAudio.PipPlay();
                }
            }
            else
            {
                move = true;
            }
        }
        else if (Physics.Raycast(RayOrigin1.position, RayOrigin1.forward, out RaycastHit hit1, 2.5f, playerLayer))
        {
            if (hit1.collider.CompareTag("Player") || hit1.collider.CompareTag("Car"))
            {
                move = false;
                if (wasMoving && hit1.collider.CompareTag("Player"))
                {
                    yelbAudio.PipPlay();
                }
            }
            else
            {
                move = true;
            }
        }
        else if (Physics.Raycast(RayOrigin2.position, RayOrigin2.forward, out RaycastHit hit2, 2.5f, playerLayer))
        {
            if (hit2.collider.CompareTag("Player") || hit2.collider.CompareTag("Car"))
            {
                move = false;
                if (wasMoving && hit2.collider.CompareTag("Player"))
                {
                    yelbAudio.PipPlay();
                }
            }
            else
            {
                move = true;
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
    }

    void OnDrawGizmos()
    {
        // Draw the raycast line in the editor for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawRay(RayOrigin.position, RayOrigin.forward * 3f);
        Gizmos.DrawRay(RayOrigin1.position, RayOrigin1.forward * 2.5f);
        Gizmos.DrawRay(RayOrigin2.position, RayOrigin2.forward * 2.5f);
    }
}
