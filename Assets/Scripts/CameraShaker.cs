using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    
    public static CameraShaker instance;

    CinemachineVirtualCamera cinemachineCamera;
    CinemachineBasicMultiChannelPerlin shakeChannel;
    float shakeTime;
    float timer;
    float currentIntensity;

    void Awake()
    {
        //Singleton
        if (instance == null) instance =  this;
        else  Destroy(this);
        
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        shakeChannel = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            shakeChannel.m_AmplitudeGain = Mathf.Lerp(0, currentIntensity, timer/shakeTime);
        }
    }

    public void ShakeCamera(float timer, float intensity)
    {
        shakeChannel.m_AmplitudeGain = intensity;
        currentIntensity = intensity;
        shakeTime = timer;
        this.timer = timer;
    }
}
