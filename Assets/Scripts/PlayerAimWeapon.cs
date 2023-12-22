using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler OnShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 shellPosition;
    }

    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Animator aimAnimator;

    public PlayerController playerController; // Reference to the player controller script
    private bool wasFacingRight;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        wasFacingRight = playerController.FacingRight; // Initialize with player's current facing direction
    }

    private void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Check if player's facing direction has changed
        if (playerController.FacingRight != wasFacingRight)
        {
            FlipGun();
            wasFacingRight = playerController.FacingRight;
        }

        // Set rotation to always point towards the mouse
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void HandleShooting() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePosition = GetMouseWorldPosition();

            aimAnimator.SetTrigger("Shoot");

            OnShoot?.Invoke(this, new OnShootEventArgs { 
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });
        }
    }

    private void FlipGun()
    {
        // Flip the gun by adjusting the y-scale
        Vector3 localScale = aimTransform.localScale;
        localScale.y *= -1;
        aimTransform.localScale = localScale;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f; // Set Z to 0 as we are working in 2D
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        // Adjust the Z value in the screen position to correctly project onto the player's plane
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, worldCamera.nearClipPlane - worldCamera.transform.position.z));
        return worldPosition;
    }
}
