using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanterBoost : MonoBehaviour
{
    private Light myLight;
    [SerializeField] private int alpha;
    private bool boost;
    private float t;
    private float spotAngle;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        alpha = 120;
        boost = false;
        spotAngle = myLight.spotAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (myLight.spotAngle > spotAngle)
        {
            myLight.spotAngle = myLight.spotAngle - 0.1f;
        }
        if (!boost)
        {
            if (Input.GetKeyDown("space"))
            {
                Debug.Log("Space was pressed");
                myLight.spotAngle = alpha;
                boost = true;
                t = 0;
            }
        } else
        {
            if (t >= 30)
            {
                boost = false;
            }
            t = t + Time.deltaTime;
        }
    }

}
