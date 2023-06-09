﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.Components
{
    public class Component:ViewModelBase
    {
        public GameEntity Owner { get; private set; }

        public Component(GameEntity owner) { 
            Debug.Assert(owner != null);
            Owner = owner;  
        }
    }
}
