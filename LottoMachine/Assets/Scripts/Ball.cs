using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("BallMove");
    }
    IEnumerator BallMove()
    {
        while(true)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)) * 30, ForceMode.Impulse);
            yield return new WaitForSeconds(1);
        }
    }
}
