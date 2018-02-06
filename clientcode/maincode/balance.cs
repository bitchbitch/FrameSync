using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class balance : MonoBehaviour {

    // Use this for initialization
    public Button vectory;
    public Button defeat;
    public Text content;
    void Start () {
        Button vbtn = vectory.GetComponent<Button>();
        Button dbtn = defeat.GetComponent<Button>();
        ST pp = ST.getInstance();
        content.text = pp.result;

        vbtn.onClick.AddListener(TaskOnClinkv);
        dbtn.onClick.AddListener(TaskOnClinkd);
    }
	
	// Update is called once per frame
    void TaskOnClinkv()
    {
        SceneManager.LoadScene(0);
    }
    void TaskOnClinkd()
    {
        Application.Quit();
    }

}
