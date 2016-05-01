using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.Messaging
{
    interface IMessageSerializer
    {
        string Serialize(Message message);
    }

}
