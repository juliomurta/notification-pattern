using System;
using System.Collections.Generic;
using System.Linq;

namespace Murta.Validation
{
    public class Notification : INotification
    {
        protected Dictionary<string, string> messages = new Dictionary<string, string>();

        public bool IsValid
        {
            get
            {
                return this.messages.Count == 0;
            }
        }

        public void Add(string propertyName, string message)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(nameof(propertyName));
            }

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(nameof(message));
            }

            if (!this.messages.Keys.Any(x => x == propertyName))
            {
                this.messages.Add(propertyName, message);
            }
            else
            {
                this.messages[propertyName] = message;
            }
        }

        public void Remove(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(nameof(propertyName));
            }

            if (this.messages.Keys.Any(x => x == propertyName))
            {
                this.messages.Remove(propertyName);
            }
        }

        public Dictionary<string, string> GetMessages()
        {
            return this.messages;
        }
    }
}
