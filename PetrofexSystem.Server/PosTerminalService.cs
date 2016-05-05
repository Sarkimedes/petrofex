using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PetrofexSystem.Common;

namespace PetrofexSystem.PosTerminals
{
    public class PosTerminalService
    {
        private static PosTerminalService _instance = new PosTerminalService();

        public static PosTerminalService Instance { get { return _instance; } }

        private PosTerminalService() { }



        public void HandlePumpProgress(Transaction transaction)
        {
            
        }
    }
}
