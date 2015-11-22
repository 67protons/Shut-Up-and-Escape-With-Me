using UnityEngine;
using System.Collections;

public class IndicatorManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Invoke("CleanUp", 5f);
    }

    void CleanUp()
    {
        Destroy(this.gameObject);
    }
}
