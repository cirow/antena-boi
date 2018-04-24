using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

    [SerializeField]
    private int[] rock_password = { 6, 1, 3, 4 };
    [SerializeField]
    private Collider2D rockPrize;
    [SerializeField]
    private GameObject rockPuzzlePrize;
	[SerializeField]
	private GameObject letraPuzzlePrize;
	[SerializeField]
    private AudioClip plim;
    [SerializeField]
    private AudioClip missSound;

    [SerializeField]
    private PuzzleButton[] buttons_letras;
    [SerializeField]
    private GameObject bushPrize;

    private AudioSource audioSource;
    [SerializeField]
    public int rock_password_index = 0;
    private static PuzzleManager instance = null;

    
    


    public static PuzzleManager Instance
    {
        get
        {
            return instance;
        }
    }

    public int[] Rock_password
    {
        get
        {
            return rock_password;
        }

        set
        {
            rock_password = value;
        }
    }

    // Use this for initialization
    void Start () {
        if(PuzzleManager.Instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Trying to instantiate 2 Puzzle Manager");
        }

        audioSource = GetComponent<AudioSource>();
        letraPuzzlePrize.SetActive(false);
        bushPrize.SetActive(false);


    }

    // Update is called once per frame
    void Update () {
		
	}

    public void CheckPuzzleLetras()
    {
        bool all_keys_correct = true;
        foreach(PuzzleButton button in buttons_letras)
        {
            if (button.IsKeyCorrect() == false)
            {
                all_keys_correct = false;
            }
        }

        if (all_keys_correct)
        {
			WinPuzzleLetras();

        }
    }



	void WinPuzzleLetras()
	{
		AudioManager.instance.ItemAudio();
        letraPuzzlePrize.SetActive(true);

    }
    //public bool RockPuzzle(int rock_index)
    //{
    //    if (rock_password_index < 4)
    //    {
    //        if (rock_index == rock_password[rock_password_index])
    //        {
    //            rock_password_index++;
    //            if (rock_password_index >= 4)
    //            {
    //                UnlockRockItem();
    //            }
    //            else
    //            {
    //                Plim();
    //            }

    //            return true;
    //        }
    //        else
    //        {
    //            RockMiss();
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        if(rock_index != 5)
    //        {
    //            LockRockItem();
    //            RockMiss();
    //        }

    //    }
    //    return false;

    //}

    public bool RockPuzzle(int rock_index)
    {
        return RockTry(rock_index);
        
    }

    private void RockMiss()
    {
        rock_password_index = 0;
        audioSource.PlayOneShot(missSound);
    }

    private bool RockTry(int rock_index)
    {
        if (rock_password_index < rock_password.Length)
        {
            //Debug.Log("Index is:" + rock_password_index + ", less than " + rock_password.Length);
            if(rock_index == rock_password[rock_password_index])
            {
                RockHit();
                return true;
            }
            else
            {
                RockMiss();
                return false;
            }
        }
        else if(rock_password_index >= rock_password.Length)
        {
            RockMiss();
            
        }
        return false;
    }

    private void RockHit()
    {
        rock_password_index++;

        if (rock_password_index >= rock_password.Length)
        {
            UnlockRockItem();
        }
        else
        {
            Plim();
        }
    }

    private void UnlockRockItem() 
    {
        Debug.Log("Rock item unlocked");
     //   rockPrize.enabled = false;
        rockPuzzlePrize.SetActive(true);
        AudioManager.instance.ItemAudio();
    }
    
    private void LockRockItem()
    {
        rockPrize.enabled = true;
        rockPuzzlePrize.SetActive(false);
    }

    public void Plim()
    {
        audioSource.PlayOneShot(plim);
    }

    public void PressBush()
    {
        bushPrize.SetActive(true);
        Plim();
    }
}
