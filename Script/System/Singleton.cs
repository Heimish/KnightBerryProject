using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T I
    {
        get
        {
            if (typeof(T) == typeof(BUSceneManager))
            {
                if (_instance == null)
                {
                    T obj = FindObjectOfType<T>();

                    if (obj == null)
                    {
                        GameObject obj2 = new GameObject();
                        obj = obj2.AddComponent<T>();
                    }
                    _instance = obj;
                }
            }
            else
            {
                if (_instance == null)
                {
                    T obj = FindObjectOfType<T>();

                    if (obj == null)
                    {
                        GameObject obj2 = new GameObject();
                        obj = obj2.AddComponent<T>();
                    }
                    _instance = obj;
                }
            }

            return _instance;
        }
    }
	void Start ()
    {
		
	}
	void Update ()
    {
		
	}
}
