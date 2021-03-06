using iTechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Surveys.Repositories
{
    public sealed class SurveysDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }


        public SurveysDbContext(DbContextOptions<SurveysDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.HasMany(e => e.Roles)
                    .WithMany(e => e.Users);

                b.HasMany(e => e.Surveys)
                    .WithOne(e => e.Author)
                    .IsRequired()
                    .HasForeignKey(e => e.AuthorId);

                b.HasMany(e => e.Answers)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.NoAction);;
                
                b.Property(user => user.UserName).IsRequired();
                b.Property(user => user.DisplayName).IsRequired();
                b.Property(user => user.PasswordHash).IsRequired();
                b.Property(user => user.CreationTime).IsRequired();
            });
            
            modelBuilder.Entity<Role>(b =>
            {
                b.Property(role => role.Name).IsRequired();
            });

            modelBuilder.Entity<Survey>(b =>
            {
                b.HasMany(e => e.Questions)
                    .WithOne(e => e.Survey)
                    .IsRequired()
                    .HasForeignKey(e => e.SurveyId);
                
                b.Property(survey => survey.CreationDate).IsRequired();
                b.Property(survey => survey.Name).IsRequired();
            });
            
            modelBuilder.Entity<Question>(b =>
            {
                b.HasMany(e => e.Options)
                    .WithOne(e => e.Question)
                    .HasForeignKey(e => e.QuestionId);
                
                b.Property(question => question.Title).IsRequired();
            });

            modelBuilder.Entity<SurveyAnswer>(b =>
            {
                b.HasMany(e => e.QuestionAnswers)
                    .WithOne(e => e.SurveyAnswer)
                    .HasForeignKey(e => e.SurveyAnswerId);

                b.HasOne(e => e.Survey)
                    .WithMany(e => e.Answers)
                    .HasForeignKey(e => e.SurveyId);
            });

            modelBuilder.Entity<QuestionAnswer>(b =>
            {
                b.HasMany(e => e.Options)
                    .WithOne(e => e.QuestionAnswer)
                    .HasForeignKey(e => e.QuestionAnswerId);

                b.HasOne(e => e.FileAnswer)
                    .WithOne(e => e.QuestionAnswer)
                    .HasForeignKey<FileAnswer>(e => e.QuestionAnswerId);
            });

            modelBuilder.Entity<QuestionAnswerOption>(b =>
            {
                b.Property(option => option.QuestionOptionId).IsRequired();
            });
            
            modelBuilder.Entity<FileAnswer>(b =>
            {
                b.HasKey(fileAnswer => fileAnswer.Id);
            });
        }
    }
}