using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    public Transform axeGrabPoint;

    PlayerController player;

    void Start() {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && player.hasWeapon)
        {
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.E) && !player.hasWeapon)
        {
            player.hasWeapon = true;
            projectilePrefab.transform.parent = player.playerBody;
            projectilePrefab.GetComponent<Rotator>().shouldRotate = false;
            projectilePrefab.transform.position = axeGrabPoint.position;
            projectilePrefab.transform.rotation = axeGrabPoint.rotation;
            projectilePrefab.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Shoot()
    {
        player.animator.Play("throw_weapon");
        projectilePrefab.GetComponent<Rotator>().shouldRotate = true;

        projectilePrefab.transform.parent = null;
        GameObject projectile = projectilePrefab;

        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        Vector3 targetPoint;
        
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000);
        }

        Vector3 direction = (targetPoint - firePoint.position).normalized;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = direction * projectileSpeed;

        player.hasWeapon = false;
    }
}