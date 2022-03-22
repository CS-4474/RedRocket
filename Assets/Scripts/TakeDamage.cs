using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    private int health = 1;

    [SerializeField]
    private GameObject explosionPrefab;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHealth()
    {
        health--;
        if (health <= 0)
        {
            if (explosionPrefab != null) Instantiate(explosionPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
