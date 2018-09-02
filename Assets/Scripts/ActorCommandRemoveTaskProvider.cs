﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActorCommandRemoveTaskProvider : ActorCommand
{
    public override void Execute(Actor actor)
    {
        actor.RemoveTaskProvider();
    }
}

