namespace toi444
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Inverse> Inverses { get; set; }
        public virtual DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Inverse>()
                .Property(e => e.docs)
                .IsUnicode(false);

            modelBuilder.Entity<Word>()
                .Property(e => e.word1)
                .IsUnicode(false);

            modelBuilder.Entity<Word>()
                .HasMany(e => e.Inverses)
                .WithRequired(e => e.Word)
                .WillCascadeOnDelete(false);
        }
    }
}
