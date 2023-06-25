using Desafio4Devs.Domain.Entities;
using Desafio4Devs.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Desafio4Devs.Infra.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {

        }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Avaliacao> Avaliacao { get; set; }

        public DbSet<ClienteAvaliacao> ClienteAvaliacao { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Obtém todas as classes de configuração das entidades, através da interface IEntityConfig, criada exclusivamente para isso
            // Dessa forma não é necessário confiurar entidade por entidade.
            var typesToRegister = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEntityConfig).IsAssignableFrom(x) && !x.IsAbstract).ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }

            builder.Entity<Cliente>().HasQueryFilter(p => p.Ativo);
        }
    }
}