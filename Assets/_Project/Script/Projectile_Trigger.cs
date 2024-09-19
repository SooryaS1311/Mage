using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Trigger : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    // Start is called before the first frame update
    void Update()
    {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(this.gameObject, 3f);
    }

}
