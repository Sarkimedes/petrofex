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
        private readonly IPumpFactory _factory;


        public PosTerminalService(IPumpFactory factory)
        {
            this._factory = factory;
        }


        public void HandleActivationRequest(Action activatedCallback = null)
        {
            // TODO: call out to pos terminals
            if (activatedCallback != null) activatedCallback();
        }

        public void HandlePumpProgress(Transaction transaction)
        {
            // TODO: implement me
        }

        internal void HandlePaymentAwaiting(Transaction transaction)
        {
            var pump = this._factory.GetPumpById(transaction.PumpId);
            // TODO: add implement with pos terminals
            pump.PayCurrentTransaction();
        }
    }
}
