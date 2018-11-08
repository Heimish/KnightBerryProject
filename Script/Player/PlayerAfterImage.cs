using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    private Renderer[] _renderer;
    private bool _off;
    private float _alpha;
    private Color _color;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Update_Off();

    }

    public void InitRenderer()
    {
        _off = false;
        _renderer = GetComponentsInChildren<Renderer>();
    }

    private void Update_Off()
    {
        if (!_off)
            return;

        foreach (Renderer r in _renderer)
        {
            Color a = r.material.GetColor("_Color");
            a.a -= 0.05f;
            r.material.SetColor("_Color", a);

            if (a.a <= 0.0f)
            {
                Destroy(transform.root.gameObject);
                return;
            }
        }
    }

    public void EndAfterImage()
    {
        _off = true;
    }
}
