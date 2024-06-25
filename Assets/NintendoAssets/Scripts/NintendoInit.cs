using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NintendoInit : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator  Start()
    {
        yield return new WaitUntil(() => SaveBridge.CacheIsLoaded);

        SceneManager.LoadScene(1);
    }

}
