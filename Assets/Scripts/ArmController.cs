using UnityEngine;

public class GunController : MonoBehaviour
{

    private Transform aimTransform;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }

    private void Update()
    {
        
    }

}
