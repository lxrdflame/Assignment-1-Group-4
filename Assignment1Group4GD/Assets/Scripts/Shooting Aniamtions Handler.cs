using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShootingAniamtionsHandler : MonoBehaviour
{
    public Animator NormalGunAnimator, MachineGunRevolve, BazookaShoot;

    public IEnumerator SMGShoot()
    {
        NormalGunAnimator.SetBool("SMGShoot", true);
        BazookaShoot.SetBool("BazookaShoot", true) ;    
        MachineGunRevolve.SetBool("Revolve", true);
        yield return new WaitForSeconds(0);
    }

    public IEnumerator SMGStop()
    {
        NormalGunAnimator.SetBool("SMGShoot", false);
        BazookaShoot.SetBool("BazookaShoot", false);
        MachineGunRevolve.SetBool("Revolve", false );
        yield return new WaitForSeconds(0);

    }



}
