using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    private static QuestManager instance = null;

    [SerializeField]
    private BlinkEffect blink;
    [SerializeField]
    private Collider2D warpCollider;
    private bool warp_check = false;
    private bool disable_idle = false;

    private GameObject currentStateBaloon;



    public static QuestManager Instance
    {
        get
        {
            return instance;
        }
    }

	void Awake()
	{
		if (QuestManager.Instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			Debug.Log("Trying to instantiate 2 Puzzle Manager");
		}
	}

    // Use this for initialization
    void Start()
    {
       
		if(AudioManager.instance != null)
		{
			AudioManager.instance.WindAudio(true);
		}
        if(warpCollider != null)
        {
            warpCollider.enabled = false;
        }
		
        //blink.StartBlinking(5);
        //StartCoroutine(WaitBlinkingAction(blink));

        currentStateBaloon = Controller2D.Player.HelpPopUp;
    }

    // Update is called once per frame
    void Update () {
		
	}



    public void OpenWall()
    {
        GameObject tempColliders = GameObject.FindGameObjectWithTag("ParentLimit");
        Destroy(tempColliders);
    }

    public void StageRadio()
    {
        OpenWall();
        AudioManager.instance.InterfAudio(true);
        IconsManagerUI.Instance.ShowObjective(1);


    }

    public void TodosOsEquips()
    {
        Controller2D.Player.teleportEnabled = true;
        StopIdleState();
        currentStateBaloon.SetActive(false);
        currentStateBaloon = Controller2D.Player.TeleportBaloon;
        warpCollider.enabled = true;
		disable_idle = false;
		
    }

    public void IdleState()
    {
        if(currentStateBaloon != null && !disable_idle)
        {
            currentStateBaloon.SetActive(true);
        }
    }

    public void StopIdleState()
    {
        if (currentStateBaloon != null)
        {
            currentStateBaloon.SetActive(false);
        }
    }

    public void Levantar()
    {
        currentStateBaloon.SetActive(false);
        currentStateBaloon = Controller2D.Player.EquipBaloon;

    }



    private IEnumerator WaitBlinkingAction(BlinkEffect blink)
    {
        while (blink.Is_Blinking)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("done blinking");
        FinishBlinking();
    }

    private void FinishBlinking()
    {
        PlayerInput.Instance.UnfreezePlayer();
        currentStateBaloon.SetActive(false);
        currentStateBaloon = Controller2D.Player.PipBaloon;
        StageRadio();
		IconsManagerUI.instance.pecasUI.enabled = true;

	}

    public void HandleWarpCollision()
    {
        warp_check = true;
        warpCollider.enabled = false;
        PlayerInput.Instance.FreezePlayer();
        blink.StartBlinking(3);
        StartCoroutine(WaitBlinkingAction(blink));
        DisableIdle();
		
    }

    public void DisableIdle()
    {
        disable_idle = true;
        currentStateBaloon.SetActive(false);
    }

    public void EnableIdle()
    {
        disable_idle = false;
    }
}
