using System;
using System.Collections;
using System.Collections.Generic;
using HeroEditor.Common;
using UnityEngine;

public class Characters : MonoBehaviour
{
    protected string characterName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public String GetCharacterName()
    {
        return characterName;
    }

    protected void SetCharacterName(string name)
    {
        characterName = name;
    }
}
