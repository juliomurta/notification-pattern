
using Murta.Validation;

namespace Loja.Dominio.Entidades
{
    public class Produto : EntidadeBase
    {
        protected double preco;
        protected string descricao;

        public Produto(INotification notification) : base(notification)
        {

        }

        public double Preco
        {
            get
            {
                return this.preco; 
            }
            set
            {
                if (value <= 0)
                {
                    base.notification.Add(nameof(this.Preco), "Preço não pode ser zero ou negativo.");
                    return;
                }

                this.preco = value;
                base.notification.Remove(nameof(this.Preco));
            }
        }

        public string Descricao
        {
            get
            {
                return this.descricao;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    base.notification.Add(nameof(this.Descricao), "A descrição do produto não pode ser nulo ou vazio.");
                    return;
                }

                this.descricao = value;
                base.notification.Remove(nameof(this.Descricao));
            }
        }
    }
}
