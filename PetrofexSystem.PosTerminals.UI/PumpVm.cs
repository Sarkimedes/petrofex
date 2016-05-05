using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;
using PetrofexSystem.PosTerminals.UI.Annotations;
using PumpLibrary;

namespace PetrofexSystem.PosTerminals.UI
{

    class PumpVm
    {
        public string Id { get; set; }
        public bool IsTransactionPending { get; set; }
        public bool IsCustomerWaiting { get; set; }
        public Transaction CurrenTransaction { get; set; }
    }
}
