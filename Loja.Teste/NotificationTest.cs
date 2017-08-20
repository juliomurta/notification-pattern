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

        const string DESCRICAO_PRODUTO_NAO_PODE_SER_NULO = "A descrição do produto não pode ser nulo ou vazio.";
        const string PRECO_NAO_PODE_SER_NEGATIVO = "Preço não pode ser zero ou negativo.";
        const string IDENTIFICACAO_CARACTERES_INSUFICIENTES = "A identificação do lote não pode ter menos que seis caracteres.";
        const string QUANTIDADE_MENOR_IGUAL_ZERO = "Quantidade não pode ser menor ou igual a zero.";
        const string PRODUT_NAO_PODE_SER_ASSOCIADO = "Produto inválido não pode ser associado ao lote.";
        const string IDENTIFICACAO_OBRIGATORIA = "Identificação do lote é obrigatória.";
        const string PRODUTO_NAO_PODE_SER_NULO = "Produto não pode ser nulo.";

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
            Assert.AreEqual(DESCRICAO_PRODUTO_NAO_PODE_SER_NULO, produto.Errors[nameof(produto.Descricao)]);
            Assert.AreEqual(PRECO_NAO_PODE_SER_NEGATIVO, produto.Errors[nameof(produto.Preco)]);

            Assert.IsFalse(lote.IsValid);
            Assert.AreEqual(IDENTIFICACAO_CARACTERES_INSUFICIENTES, lote.Errors[nameof(lote.Identificacao)]);
            Assert.AreEqual(QUANTIDADE_MENOR_IGUAL_ZERO, lote.Errors[nameof(lote.Quantidade)]);
            Assert.AreEqual(PRODUT_NAO_PODE_SER_ASSOCIADO, lote.Errors[nameof(lote.Produto)]);

            lote.Identificacao = string.Empty;
            lote.Produto = null;

            Assert.IsFalse(lote.IsValid);
            Assert.AreEqual(IDENTIFICACAO_OBRIGATORIA, lote.Errors[nameof(lote.Identificacao)]);
            Assert.AreEqual(PRODUTO_NAO_PODE_SER_NULO, lote.Errors[nameof(lote.Produto)]);
        }

        [TestMethod]
        public void Corrigindo_Instancias_Invalidas_Test()
        {
            var produto = this.kernel.Get<Produto>();
            produto.Descricao = string.Empty;
            produto.Preco = -100;

            Assert.IsFalse(produto.IsValid);
            Assert.AreEqual(DESCRICAO_PRODUTO_NAO_PODE_SER_NULO, produto.Errors[nameof(produto.Descricao)]);
            Assert.AreEqual(PRECO_NAO_PODE_SER_NEGATIVO, produto.Errors[nameof(produto.Preco)]);

            produto.Descricao = "Produto B";
            produto.Preco = 200;

            Assert.IsTrue(produto.IsValid);
            Assert.AreEqual(0, produto.Errors.Count);
        }
    }
}
