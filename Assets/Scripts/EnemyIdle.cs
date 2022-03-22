using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : MonoBehaviour
{
    private float timeOffset;

    // Start is called before the first frame update
    void Start()
    {
        timeOffset = Random.Range(0, 100);
    }

    private void OnEnable()
    {
        transform.rotation.SetEulerAngles(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Mathf.Sin(Time.time) * .2f));
    }
}
