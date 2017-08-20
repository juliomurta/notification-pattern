using Murta.Validation;
using System.Collections.Generic;

namespace Loja.Dominio.Entidades
{
    public class EntidadeBase
    {
        protected INotification notification = null;       

        public EntidadeBase(INotification notification)
        {
            this.notification = notification;
        }

        public bool IsValid
        {
            get
            {
                return this.notification.IsValid;
            }
        }

        public Dictionary<string, string> Errors
        {
            get
            {
                return this.notification.GetMessages();
            }
        }
    }
}
