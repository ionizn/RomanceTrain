using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{

    public float degreesPerSec = 360f;
    public float speed = 1f;
    void Start()
    {
    }

    void Update()
    {
        float rotAmount = degreesPerSec * Time.deltaTime * speed;
        float curRot = transform.localRotation.eulerAngles.z;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
    }

}