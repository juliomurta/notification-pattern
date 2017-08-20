using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murta.Validation
{
    public interface INotification
    {
        bool IsValid { get; }

        void Add(string propertyName, string message);

        void Remove(string propertyName);

        Dictionary<string, string> GetMessages();
    }
}
