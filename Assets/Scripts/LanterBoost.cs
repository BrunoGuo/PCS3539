using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanterBoost : MonoBehaviour
{
    private Light myLight;
    [SerializeField] private int maxAngle;
    // private bool boost;
    private float t;
    private float spotAngle;
    [SerializeField] private float angleRate;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        maxAngle = 120;
        // boost = false;
        spotAngle = myLight.spotAngle;
        angleRate = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        if (myLight.spotAngle > spotAngle)
        {
            myLight.spotAngle = myLight.spotAngle - angleRate;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "vela" && Input.GetKeyDown("space"))
        {
            Destroy(other.gameObject);
            Debug.Log("Roubo de vela");
            myLight.spotAngle = maxAngle;
            myLight.intensity = 14;
        }
    }
}
