using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactivateCamera : MonoBehaviour
{
    [SerializeField] private GameObject fps_camera;
    IEnumerator Start()
    {
        fps_camera.SetActive(false);
        yield return new WaitForSeconds(1f);
        fps_camera.SetActive(true);
    }

}
