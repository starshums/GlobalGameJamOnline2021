using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossTrigger : MonoBehaviour
{
    [SerializeField] AudioClip begin;
    [SerializeField] AudioClip fight;
    [SerializeField] AudioClip end;
    [SerializeField] AudioClip letsFight;

    [SerializeField] SkullatronHandController handController;

    void Start() => GetComponent<AudioSource>().PlayOneShot(begin);

    private void OnTriggerEnter(Collider other)
    {
#if UNITY_EDITOR
        Debug.Log("Boss Fight Starts");
#endif  
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().volume =0.4f;
        GetComponent<AudioSource>().PlayOneShot(letsFight);
        GetComponent<AudioSource>().loop = true;
        GameObject.FindObjectOfType<SkullatronHandController>().isBossFightStarted = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<AudioSource>().volume = 0.2f;
        GetComponent<AudioSource>().PlayOneShot(fight);
        GetComponent<AudioSource>().loop = true;
        GetComponent<BoxCollider>().enabled = false;
    }
}
