using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace PetrofexSystem.Messaging
{
    public class MessageQueue
    {
        private readonly List<Message> _messages;

        public MessageQueue()
        {
            this._messages = new List<Message>();
        }

        public void Put(Message message)
        {
            this._messages.Add(message);
            this._messages.Sort((x, y) => x.Timestamp.CompareTo(y.Timestamp));
        }
            
        /// <summary>
        ///  Looks at a message without consuming it.
        /// </summary>
        /// <returns>The last message added to the queue. Returns null if the queue was empty.</returns>
        public Message Peek()
        {
            return this._messages.LastOrDefault();
        }

        /// <summary>
        /// Gets a message from the queue. The message will be consumed, meaning that other clients will not be able to access it.
        /// </summary>
        /// <returns>The last message added to the queue, or null if the queue was empty.</returns>
        public Message Get()
        {
            Message message = this._messages.LastOrDefault();
            if (message != null)
            {
                this._messages.Remove(message);
            }
            return message;
        }
    }
}
