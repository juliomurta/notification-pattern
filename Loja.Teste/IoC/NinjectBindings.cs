using Loja.Dominio;
using Loja.Dominio.Entidades;
using Murta.Validation;
using Ninject.Modules;

namespace Loja.Teste.IoC
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            base.Bind<INotification>()
                .To<Notification>();

            base.Bind<Produto>()
                .ToSelf()
                .WithConstructorArgument<INotification>(new Notification());

            base.Bind<Lote>()
                .ToSelf()
                .WithConstructorArgument<INotification>(new Notification());
        }
    }
}
