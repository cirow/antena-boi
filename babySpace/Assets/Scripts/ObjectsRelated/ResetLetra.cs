using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLetra : MonoBehaviour {
    [SerializeField]
    private Sprite spriteApertado;
    [SerializeField]
    private Sprite spriteNormal;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private List<Letra> lista_letras;


    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteNormal;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            spriteRenderer.sprite = spriteApertado;
            ResetPuzzleLetras();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            spriteRenderer.sprite = spriteNormal;
        }
    }

    public void ResetPuzzleLetras()
    {
        foreach (Letra letra in lista_letras)
        {
            letra.ResetPosition();
        }
    }
}
