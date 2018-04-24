using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSlot : MonoBehaviour {

    [SerializeField]
    private TipoItem tipo;

    public TipoItem Tipo
    {
        get
        {
            return tipo;
        }
    }
}
