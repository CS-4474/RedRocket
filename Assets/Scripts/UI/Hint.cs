using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    public void CloseHint()
    {
        this.gameObject.SetActive(false);
    }
}
