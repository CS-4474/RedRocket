using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        StartCoroutine("destroyAfter", 5);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.time - startTime > 5) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hittable")
        {
            if (collision.GetComponent<TakeDamage>() is TakeDamage takeDamage) takeDamage.LoseHealth();
            Destroy(gameObject);
        }
    }

    private IEnumerator destroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
