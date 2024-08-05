namespace Library.Infrastructure.Data.Migrations
{
    using Library.Domain.Entities;
    using Library.Domain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Library.Infrastructure.Data.Context.LibraryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Library.Infrastructure.Data.Context.LibraryDbContext context)
        {
            if (context.Books.Any() || context.Authors.Any() || context.Publishers.Any())
                return;

            var publishers = new[]
            {
                new Publisher("Eclipse Books"),
                new Publisher("Aurora Ink"),
                new Publisher("Luminous Literary"),
                new Publisher("The Book Company"),
                new Publisher("New Dawn Books")
            };

            context.Publishers.AddRange(publishers);
            context.SaveChanges();

            var authors = new[]
            {
                new Author("Michael Jackson"),
                new Author("Neil Armstrong"),
                new Author("Silvio Santos"),
                new Author("Linus Torvalds"),
                new Author("Steve Jobs"),
                new Author("Bill Gates"),
                new Author("James Madison"),
                new Author("George Washington"),
                new Author("John Adams"),
                new Author("Thomas Jefferson"),
                new Author("Alexander Hamilton"),
                new Author("John Jay"),
                new Author("Benjamin Franklin"),
                new Author("John Frusciante"),
                new Author("Kanye West"),
                new Author("Ayrton Senna"),
                new Author("Carmen Miranda"),
                new Author("Jorge Amado"),
                new Author("Machado de Assis"),
                new Author("Mário Quintana"),
            };

            context.Authors.AddRange(authors);
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book("Gabriela, Cravo e Canela", "Romance que se passa na cidade de Ilhéus, Bahia, nos anos 1920, explorando o contraste entre a simplicidade da vida rural e a modernização da cidade.", "978-8520916525", BookLanguageEnum.Portuguese, new DateTime(1958, 1, 1), authors[17].Id, publishers[0].Id),
                new Book("Dona Flor e Seus Dois Maridos", "História de Dona Flor, uma mulher que se vê dividida entre o amor por seu falecido marido e o novo casamento com um homem muito diferente.", "978-8520918550", BookLanguageEnum.Portuguese, new DateTime(1966, 1, 1), authors[17].Id, publishers[0].Id),
                new Book("O Capitão de Longa Caminhada", "Narrativa sobre a vida do capitão de uma expedição pelo interior da Bahia, destacando a cultura e os conflitos da região.", "978-8520919007", BookLanguageEnum.Portuguese, new DateTime(1992, 1, 1), authors[17].Id, publishers[0].Id),

                new Book("Dom Casmurro", "Clássico da literatura brasileira, narra a história de Bentinho e sua obsessão com a suspeita de traição de sua esposa Capitu.", "978-8520914386", BookLanguageEnum.Portuguese, new DateTime(1899, 1, 1), authors[18].Id, publishers[1].Id),
                new Book("Memórias Póstumas de Brás Cubas", "A obra é um romance do ponto de vista do defunto Brás Cubas, que narra sua vida e reflexões após a morte.", "978-8520911378", BookLanguageEnum.Portuguese, new DateTime(1881, 1, 1), authors[18].Id, publishers[1].Id),
                new Book("Quincas Borba", "Continuação de 'Memórias Póstumas de Brás Cubas', o livro explora a vida de Quincas Borba, personagem secundário do romance anterior.", "978-8520913297", BookLanguageEnum.Portuguese, new DateTime(1891, 1, 1), authors[18].Id, publishers[1].Id),

                new Book("A Rua dos Cataventos", "Coleção de poesias que capturam a simplicidade e a melancolia da vida, característicos da obra de Quintana.", "978-8520912560", BookLanguageEnum.Portuguese, new DateTime(1966, 1, 1), authors[19].Id, publishers[2].Id),
                new Book("Poemas Escolhidos", "Uma seleção dos melhores poemas de Mário Quintana, destacando seu estilo único e sua sensibilidade poética.", "978-8520913338", BookLanguageEnum.Portuguese, new DateTime(1972, 1, 1), authors[19].Id, publishers[2].Id),
                new Book("Bôto: Poemas do Amor e da Morte", "Antologia poética que explora temas de amor e morte, com a marca distintiva da poesia de Quintana.", "978-8520913574", BookLanguageEnum.Portuguese, new DateTime(1987, 1, 1), authors[19].Id, publishers[2].Id)
            };           

            for (int i = 1; i <= 4; i++)
            {
                var authorIndex = (i - 1) % authors.Length;
                var publisherIndex = (i - 1) % publishers.Length;

                var book = new Book(
                    $"Book Title {i}",
                    $"This is a summary for book {i}.",
                    $"ISBN-{i:0000000000}",
                    BookLanguageEnum.English,
                    DateTime.Now.AddYears(-i),
                    authors[authorIndex].Id,
                    publishers[publisherIndex].Id
                );

                books.Add(book);
            }

            context.Books.AddRange(books);

            context.SaveChanges();
        }
    }
}
