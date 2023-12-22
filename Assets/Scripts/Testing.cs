using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // [SerializeField] private PlayerAimWeapon playerAimWeapon;

    // private void Start() {
    //     playerAimWeapon.OnShoot += PlayerAimWeapon_OnShoot;
    // }

    // private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e) {
    //     ShakeCamera(.6f, .05f);
    //     WeaponTracer.Create(e.gunEndPointPosition, e.shootPosition);
    //     Shoot_Flash.AddFlash(e.gunEndPointPosition);

    //     Vector3 shootDir = (e.shootPosition - e.gunEndPointPosition).normalized;
    //     shootDir = UtilsClass.ApplyRotationToVector(shootDir, 90f);
    //     ShellParticleSystemHandler.Instance.SpawnShell(e.shellPosition, shootDir);
    // }

    // public static void ShakeCamera(float intensity, float timer) {
    //     Vector3 lastCameraMovement = Vector3.zero;
    //     FunctionUpdater.Create(delegate () {
    //         timer -= Time.unscaledDeltaTime;
    //         Vector3 randomMovement = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * intensity;
    //         Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
    //         lastCameraMovement = randomMovement;
    //         return timer <= 0f;
    //     }, "CAMERA_SHAKE");
    // }
}
