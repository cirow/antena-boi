using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour {

    [SerializeField]
    private float time_between_blinks;
  
    public int total_blinks = 3;
    public int number_of_blinks = 0;

    private float time_passed = 0f;
    private bool is_blinking = false;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        time_passed = 0f;
        number_of_blinks = 0;

    }
	
	// Update is called once per frame
	void Update () {
        if(is_blinking && number_of_blinks < total_blinks*2)
        {
            time_passed += Time.deltaTime;

            if ((time_passed >= time_between_blinks))
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                number_of_blinks++;


                time_passed = 0f;
            }

        }
        else
        {
            StopBlinking();
        }

    }

    public void StartBlinking(int total_blink)
    {
        total_blinks = total_blink;
        number_of_blinks = 0;
        is_blinking = true;
    }

    public void StopBlinking()
    {
        is_blinking = false;
        number_of_blinks = 0;
        spriteRenderer.enabled = false;
    }

    public bool Is_Blinking
    {
        get {
            return is_blinking;
        }
    }
}
