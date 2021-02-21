using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VyasApi.Repository.Data;

namespace VyasApi.Repository.Contexts
{
	public class UserDbContext : DbContext
	{
		private readonly ILoggerFactory loggerFactory;

		public UserDbContext(
			DbContextOptions<UserDbContext> options,
			ILoggerFactory loggerFactory) 
			: base(options)
		{
			this.loggerFactory = loggerFactory;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.HasSequence<long>("UserIdSequence");

			modelBuilder.Entity<User>()
				.Property(x => x.CreatedDate)
				.HasDefaultValueSql("getdate()");

			modelBuilder.Entity<User>()
				.Property(x => x.Id)
				.HasDefaultValueSql("NEXT VALUE FOR UserIdSequence");

			modelBuilder.Entity<User>()
				.HasKey(x => x.Id);			
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLoggerFactory(loggerFactory);
		}

		public DbSet<User> User {get;set;}
	}

}