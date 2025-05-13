using Microsoft.EntityFrameworkCore;
using SistemaAPI.Dominio.Entidades;


namespace SistemaAPI.Data.Contexto;

public class ApiContexto : DbContext
{

    public ApiContexto(DbContextOptions<ApiContexto> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    //Definir as entidades do banco de dados.
    //****************************************************************************
    public DbSet<Banco> Bancos { get; set; }
	public DbSet<Classe> Classes { get; set; }
	public DbSet<Usuario> Usuarios { get; set; }


	//Definições dos campos das tabelas 
	//para que não seja utilizada DataAnnotations nos models no domain
	//****************************************************************************
	//protected override void OnModelCreating(ModelBuilder modelBuilder)
	//{
	//    base.OnModelCreating(modelBuilder);
	//    //Definições das sequence de cada tabela - apenas no PostgreSQL
	//    //****************************************************************************
	//    //modelBuilder.HasSequence<int>("usuario_seq").StartsAt(1000).IncrementsBy(1);
	//    //modelBuilder.HasSequence<int>("convenio_seq").StartsAt(1000).IncrementsBy(1);
	//    //modelBuilder.HasSequence<int>("cliente_seq").StartsAt(1000).IncrementsBy(1);
	//    //modelBuilder.HasSequence<int>("beneficio_seq").StartsAt(1000).IncrementsBy(1);
	//    //Definições das configurações de cada tabela.
	//    //****************************************************************************
	//    modelBuilder.ApplyConfiguration(new BancoEntidadeConfig());
	//}

}

