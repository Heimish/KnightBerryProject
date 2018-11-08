using UnityEngine;
using System.Collections;

public class FairyMeteorAddForceDemo : MonoBehaviour {

    public int strength;
    public Vector3 direction;

    protected Rigidbody rgbd;

    public void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        rgbd.velocity = (direction * strength);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StatueObject")
        {
            gameObject.SetActive(false);
            other.GetComponent<StatueObject>().SetDestroyEffect();
        }
    }
}
