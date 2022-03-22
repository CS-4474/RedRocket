using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawner : MonoBehaviour
{
    [SerializeField]
    private float averageSpawnTime;

    [SerializeField]
    private Rigidbody2D cometPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value * 10 < Time.deltaTime)
        {
            Vector2 cameraPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
            Rigidbody2D comet = Instantiate(cometPrefab, new Vector3(cameraPos.x + Random.Range(-20, 20), cameraPos.y + 15, 0), Quaternion.identity, transform);
            comet.AddForce(averageSpawnTime * (cameraPos - comet.position).normalized * (1 + Random.value), ForceMode2D.Impulse);
        }
    }
}
