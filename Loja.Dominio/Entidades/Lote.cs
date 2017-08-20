using Loja.Dominio.Entidades;
using Murta.Validation;
using Murta.Validation;

namespace Loja.Dominio.Entidades
{
    public class Lote : EntidadeBase
    {
        protected string identificacao;
        protected Produto produto;
        protected int quantidade;

        public Lote(INotification notifciation) : base(notifciation)
        {

        }

        public string Identificacao
        {
            get
            {
                return this.identificacao;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    base.notification.Add(nameof(this.Identificacao), "Identificação do lote é obrigatória.");
                    return;
                }

                if (value.Length < 6)
                {
                    base.notification.Add(nameof(this.Identificacao), "A identificação do lote não pode ter menos que seis caracteres.");
                    return;
                }

                this.identificacao = value;
                base.notification.Remove(nameof(this.Identificacao));
            }
        }

        public int Quantidade
        {
            get
            {
                return this.quantidade;
            }
            set
            {
                if (value <= 0)
                {
                    this.notification.Add(nameof(this.Quantidade), "Quantidade não pode ser menor ou igual a zero.");
                    return;
                }

                this.quantidade = value;
                base.notification.Remove(nameof(this.Quantidade));
            }
        }

        public Produto Produto
        {
            get
            {
                return this.produto;
            }
            set
            {
                if (value == null)
                {
                    base.notification.Add(nameof(this.Produto), "Produto não pode ser nulo.");
                    return;
                }

                if (!value.IsValid)
                {
                    base.notification.Add(nameof(this.Produto), "Produto inválido não pode ser associado ao lote.");
                    return;
                }

                this.produto = value;
                base.notification.Remove(nameof(this.Produto));
            }
        }
    }
}
    