using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySelf : MonoBehaviour
{
    float delay = 4.0f; //This implies a delay of 2 seconds.

    private void Start()
    {
        StartCoroutine(nameof(DestroyExplosion));
    }
    IEnumerator DestroyExplosion()
        {
        while (true)
        {

            yield return new WaitForSeconds(delay);
            Destroy(gameObject);

            yield return null;
        }
    }
    
}
