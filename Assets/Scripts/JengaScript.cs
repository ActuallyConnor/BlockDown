using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaScript : MonoBehaviour
{
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {        
        StartCoroutine("Wait");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds((float)0.255);
        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }
}
