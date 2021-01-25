using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Transform> zombiePos = new List<Transform>();
    public List<Zombie> zombies = new List<Zombie>();
    public List<GameObject> items = new List<GameObject>();
    public GameObject prefab;
    int zombieCount = 4;
    int score=0;

    public Text scoreUI;
    public GameObject endUI;
    public Text targetUI;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        GetComponent<AudioSource>().volume = VolumeSetting.volume;
    }

    float timeToAdd = 0;
    // Update is called once per frame
    void Update()
    {
        if(timeToAdd<0)
        {
            timeToAdd = 1;
            zombieCount++;
        }
        timeToAdd -= Time.deltaTime;

        Spawn();


        if(Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene(0);

    }

    public void SpawnAt(Vector3 pos)
    {
        var item = items[Random.Range(0, 3)];
        var ob = Instantiate(item, pos, item.transform.rotation);
        ob.SetActive(true);
        score += 1;
        if(score<100)
            targetUI.text = $"Target: Kill 100 zombies ({score}/100)";
        else
            targetUI.text = $"Target: Kill 100 zombies (100/100)\nTry to kill more zombies";
    }

    void Spawn()
    {
        if(zombies.Count<zombieCount)
        {
            zombiePos.Sort((x,y) => -Vector3.Distance(x.position, Player.instance.transform.position).CompareTo(Vector3.Distance(y.position, Player.instance.transform.position)));
            var pos=zombiePos[0].position;
            while(zombies.Count < zombieCount)
            {
                var ob=Instantiate(prefab, pos, prefab.transform.rotation);
                ob.SetActive(true);
                zombies.Add(ob.GetComponent<Zombie>());
            }
        }
    }

    public void End()
    {
        endUI.SetActive(true);
        scoreUI.text = "Your Score: " + score.ToString()+"00";
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

}
