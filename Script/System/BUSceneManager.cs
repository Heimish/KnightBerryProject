using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BUSceneManager : Singleton<BUSceneManager>
{
    private Scene[] _allScenes;

    public Scene[] AllScenes { get { return _allScenes; } }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public Scene[] GetAllScenes()
    {
        List<Scene> scens = new List<Scene>();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            scens.Add(SceneManager.GetSceneAt(i));
        }

        return scens.ToArray();
    }

    public T[] FindObjectsAtAllScene<T>() where T : Object
    {
        List<T> obj = new List<T>();

        //foreach (Scene scene in GetAllScenes())
        //{
        //    SceneManager.SetActiveScene(scene);

            foreach (T o in FindObjectsOfType<T>())
            {
                obj.Add(o);
            }
        //}
        return obj.ToArray();
    }

    public T GetSingletonClassAtAllScene<T>() where T : Object
    {
        T singleton = null;

        //foreach (Scene scene in GetAllScenes())
        //{
        //    SceneManager.SetActiveScene(scene);
            singleton = FindObjectOfType<T>();
        //}

        return singleton;
    }
}
