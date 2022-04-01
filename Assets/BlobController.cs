using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform blob = transform.GetChild(i);

            blob.gameObject.SetActive(true);
        }
    }
}
