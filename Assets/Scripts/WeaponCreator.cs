using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WeaponCreator : MonoBehaviour
{
    public WeaponController Weapon;

    [SerializeField] private GameObject startPoint;

    [SerializeField] private bool isScaleChanging;


    private DemonController demon;

    private void Awake()
    {
        demon = GetComponent<DemonController>();
    }

    public IEnumerator CreateWeapon(Vector2 direction)
    {
        yield return new WaitForSeconds(0.5f);
        var newWeapon = Instantiate(Weapon, startPoint.transform.position, Quaternion.identity);

        if (isScaleChanging && demon != null)
        {
            var light = newWeapon.transform.GetChild(1).GetComponent<Light2D>();
            newWeapon.transform.localScale = new Vector3(demon.Damage / 10, demon.Damage / 10, 1);
            light.pointLightOuterRadius = newWeapon.transform.localScale.x;

            Debug.Log("Orb size: " + newWeapon.transform.localScale + " Demon damage: " + demon.Damage);
            Debug.Log("light radius: " + light.pointLightOuterRadius);

            if (demon.Health <= 333)
            {
                newWeapon.GetComponent<SpriteRenderer>().color = Color.red;
                light.color = Color.red;
            }
        }

        var controller = newWeapon.GetComponent<WeaponController>();
        controller.gameObject.SetActive(true);
        controller.SetDirection(direction);
    }
}
