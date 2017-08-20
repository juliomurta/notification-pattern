using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Reflection;
using Loja.Dominio.Entidades;
using Loja.Dominio;

namespace Loja.Teste
{
    [TestClass]
    public class NotificationTest
    {
        IKernel kernel = null;

        [TestInitialize]
        public void InicializarTestes()
        {
            this.kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        [TestMethod]
        public void Criando_Instancias_Validas_Test()
        {
            var produto = this.kernel.Get<Produto>();
            produto.Descricao = "Produto A";
            produto.Preco = 100;

            var lote = this.kernel.Get<Lote>();
            lote.Identificacao = "ABCDEFG";
            lote.Quantidade = 10;
            lote.Produto = produto;

            Assert.IsTrue(produto.IsValid);
            Assert.IsTrue(lote.IsValid);
        }

        [TestMethod]
        public void Criando_Instancias_Invalidas_Test()
        {
            var produto = this.kernel.Get<Produto>();
            produto.Descricao = string.Empty;
            produto.Preco = -100;

            var lote = this.kernel.Get<Lote>();
            lote.Identificacao = "ABC";
            lote.Quantidade = 0;
            lote.Produto = produto;

            Assert.IsFalse(produto.IsValid);
            Assert.AreEqual("A descrição do produto não pode ser nulo ou vazio.", produto.Errors[nameof(produto.Descricao)]);
            Assert.AreEqual("Preço não pode ser zero ou negativo.", produto.Errors[nameof(produto.Preco)]);

            Assert.IsFalse(lote.IsValid);
            Assert.AreEqual("A identificação do lote não pode ter menos que seis caracteres.", lote.Errors[nameof(lote.Identificacao)]);
            Assert.AreEqual("Quantidade não pode ser menor ou igual a zero.", lote.Errors[nameof(lote.Quantidade)]);
            Assert.AreEqual("Produto inválido não pode ser associado ao lote.", lote.Errors[nameof(lote.Produto)]);

            lote.Identificacao = string.Empty;
            lote.Produto = null;

            Assert.IsFalse(lote.IsValid);
            Assert.AreEqual("Identificação do lote é obrigatória.", lote.Errors[nameof(lote.Identificacao)]);
            Assert.AreEqual("Produto não pode ser nulo.", lote.Errors[nameof(lote.Produto)]);
        }
    }
}
