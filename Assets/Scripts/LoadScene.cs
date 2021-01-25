using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(() => { SceneManager.LoadScene(sceneName); });

    }
}
