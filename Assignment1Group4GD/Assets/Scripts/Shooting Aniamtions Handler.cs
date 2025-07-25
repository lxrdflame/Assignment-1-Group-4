using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShootingAniamtionsHandler : MonoBehaviour
{
    public Animator NormalGunAnimator;

    public IEnumerator ShootAnimation()
    {
        NormalGunAnimator.SetBool("Shoot", false);
        NormalGunAnimator.SetBool("Shoot", true);
        yield return new WaitForSeconds(0.3f);
        NormalGunAnimator.SetBool("Shoot", false);
    }

    public IEnumerator ReloadAnimation()
    {
        NormalGunAnimator.SetBool("Reload", false);
        NormalGunAnimator.SetBool("Reload", true);
        yield return new WaitForSeconds(1.1f);
        NormalGunAnimator.SetBool("Reload", false);
    }
}
