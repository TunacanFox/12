using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] float _seconds = 3.0f;
    private void Start()
    {
        Destroy(gameObject, _seconds);
    }
}
