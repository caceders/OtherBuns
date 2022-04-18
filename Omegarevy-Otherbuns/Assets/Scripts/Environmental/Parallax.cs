using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Transform followTransform;
    [SerializeField] private float parallaxIntensity = 10;
    [SerializeField] private bool parallaxVertically = false;
    [SerializeField] private bool cloudDrift = false;
    [SerializeField] private float DriftIntensity = 1f;
    private float drift = 0;

    private void Update()
    {
        if(cloudDrift)
        {
            this.transform.position = new Vector3(((followTransform.position.x + drift)/parallaxIntensity)%getDriftReset(), this.transform.position.y, 0);
            drift += DriftIntensity * 0.001f;
        }
        else
        {
            this.transform.position = new Vector3((followTransform.position.x/parallaxIntensity)%getDriftReset(), this.transform.position.y, 0);
        }
        if(parallaxVertically)
        {
            this.transform.position = new Vector3(this.transform.position.x, followTransform.position.y/parallaxIntensity , 0);
        }
        
    }

    private float getDriftReset()
    {
        return 8 * this.gameObject.transform.localScale.x;
    }
}
