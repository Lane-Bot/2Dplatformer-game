using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform luanchpoint;
    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, luanchpoint.position,projectilePrefab.transform.rotation);
        
        Vector3 origScale = projectile.transform.localScale;

        // Flip the projectile's facing direction and Movement based on the direction the character is facing at time of luanch
        projectile.transform.localScale = new Vector3(
             origScale.x * transform.localScale.x > 0 ? 1 : -1,
             origScale.y,
             origScale.z); ;
    }

}
