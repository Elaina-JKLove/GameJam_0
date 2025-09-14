using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    public CharacterMove CharacterMove => characterMove;
    public CharacterAnimator CharacterAnimator => characterAnimator;
    public CharacterState CharacterState => characterState;



    protected CharacterMove characterMove;
    protected CharacterAnimator characterAnimator;
    protected CharacterState characterState;



    void Awake()
    {
        characterMove = GetComponent<CharacterMove>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        characterState = GetComponent<CharacterState>();
    }
}
