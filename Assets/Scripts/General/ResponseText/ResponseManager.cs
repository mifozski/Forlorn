using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ResponseManager : MonoBehaviour
{
    private Queue<string> responses;

    void Start()
    {
        responses = new Queue<string>();
    }
}