﻿using UnityEngine;

public class AnimatorStateReporter : StateMachineBehaviour
{
    private IAnimationStateReader _stateReader;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        FundReader(animator);

        _stateReader.ExitedState(stateInfo.shortNameHash);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        FundReader(animator);

        _stateReader.EnteredState(stateInfo.shortNameHash);
    }

    private void FundReader(Animator animator)
    {
        if (_stateReader != null)
            return;
        _stateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
    }
}