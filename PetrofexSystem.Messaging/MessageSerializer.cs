using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace PetrofexSystem.Messaging
{
    public class MessageSerializer : IMessageSerializer
    {
        public string Serialize(Message message)
        {
            var serializer = new DataContractJsonSerializer(typeof(Message));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, message);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public Message Deserialize(string deserialized)
        {
            var serializer = new DataContractJsonSerializer(typeof(Message));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(deserialized)))
            {
                return serializer.ReadObject(stream) as Message;
            }
        }
    }
}
