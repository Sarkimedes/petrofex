﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.Pumps.UnitTests
{
    class FakeServer : IPumpActivationServer
    {
        public FakeServer()
        {
            this.ActivationRequested = false;
        }

        public bool ActivationRequested { get; set; }

        public void RequestActivation(string pumpId)
        {
            this.ActivationRequested = true;
        }
    }
}
