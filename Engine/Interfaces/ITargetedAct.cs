﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Interfaces
{
    public interface ITargetedAct : IAct
    {
        IActor Target { get; set; }
    }
}
