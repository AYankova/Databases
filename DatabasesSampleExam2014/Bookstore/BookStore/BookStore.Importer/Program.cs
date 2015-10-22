namespace BookStore.Importer
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using BookStore.Data;
    using BookStore.Models;

    public class Program
    {
        private static BookStoreDbContext db;

        public static void Main()
        {
            db = new BookStoreDbContext();

            // Import();
            Search();
        }

        private static void Search()
        {
            var xmlQueries = XElement.Load(@"..\..\..\reviews-queries.xml").Elements();
            var result = new XElement("search-results");

            foreach (var xmlQuery in xmlQueries)
            {
                var queryInReviews = db.Reviews.AsQueryable();

                if (xmlQuery.Attribute("type").Value == "by-period")
                {
                    var startDate = DateTime.Parse(xmlQuery.Element("start-date").Value);
                    var endDate = DateTime.Parse(xmlQuery.Element("end-date").Value);

                    queryInReviews = queryInReviews.Where(r => r.CreatedOn >= startDate && r.CreatedOn <= endDate);
                }
                else if (xmlQuery.Attribute("type").Value == "by-author")
                {
                    var authorName = xmlQuery.Element("author-name").Value;

                    queryInReviews = queryInReviews.Where(r => r.Author.Name == authorName);
                }

                var resultSet = queryInReviews.OrderBy(r => r.CreatedOn)
                              .ThenBy(r => r.Content)
                              .Select(r => new
                              {
                                  Date = r.CreatedOn,
                                  Content = r.Content,
                                  Book = new
                                  {
                                      Title = r.Book.Title,
                                      Authors = r.Book.Authors
                                                      .AsQueryable()
                                                      .OrderBy(a => a.Name)
                                                      .Select(a => a.Name),
                                      ISBN = r.Book.ISBN,
                                      URL = r.Book.WebSite
                                  },
                              }).ToList();
                var xmlResultSet = new XElement("result-set");

                foreach (var reviewInResult in resultSet)
                {
                    var xmlReview = new XElement("review");
                    xmlReview.Add(new XElement("date", reviewInResult.Date.ToString("d-MMM-yyyy")));
                    xmlReview.Add(new XElement("content", reviewInResult.Content.Trim()));

                    var xmlBook = new XElement("book");
                    xmlBook.Add(new XElement("title", reviewInResult.Book.Title));

                    var authors = reviewInResult.Book.Authors;
                    if (authors.ToList().Count() != 0)
                    {
                        xmlBook.Add(new XElement("authors", string.Join(", ", reviewInResult.Book.Authors)));
                    }

                    var isbn = reviewInResult.Book.ISBN;
                    if (isbn != null)
                    {
                        xmlBook.Add(new XElement("isbn", reviewInResult.Book.ISBN));
                    }

                    var url = reviewInResult.Book.URL;
                    if (url != null)
                    {
                        xmlBook.Add(new XElement("url", reviewInResult.Book.URL));
                    }

                    xmlReview.Add(xmlBook);
                    xmlResultSet.Add(xmlReview);
                }

                result.Add(xmlResultSet);
            }

            result.Save(@"..\..\..\reviews-search-results.xml");
        }

        private static void Import()
        {
            var xmlBooks = XElement.Load(@"..\..\..\complex-books.xml").Elements();

            foreach (var xmlBook in xmlBooks)
            {
                var currentBook = new Book();
                currentBook.Title = xmlBook.Element("title").Value;

                var isbn = xmlBook.Element("isbn");
                if (isbn != null)
                {
                    var bookWithSameIsbn = db.Books.Any(b => b.ISBN == isbn.Value);
                    if (bookWithSameIsbn)
                    {
                        throw new ArgumentException("Book with the same isbn already exists");
                    }

                    currentBook.ISBN = isbn.Value;
                }

                var price = xmlBook.Element("price");
                if (price != null)
                {
                    currentBook.Price = decimal.Parse(price.Value);
                }

                var webSite = xmlBook.Element("web-site");
                if (webSite != null)
                {
                    currentBook.WebSite = webSite.Value;
                }

                var xmlAuthors = xmlBook.Element("authors");

                if (xmlAuthors != null)
                {
                    foreach (var xmlAuthor in xmlAuthors.Elements("author"))
                    {
                        var authorName = xmlAuthor.Value;
                        var author = GetAuthor(authorName);

                        currentBook.Authors.Add(author);
                    }
                }

                var xmlReviews = xmlBook.Element("reviews");

                if (xmlReviews != null)
                {
                    foreach (var xmlReview in xmlReviews.Elements("review"))
                    {
                        var reviewDate = xmlReview.Attribute("date");
                        var authorName = xmlReview.Attribute("author");

                        var review = new Review
                        {
                            Content = xmlReview.Value,
                            CreatedOn = reviewDate != null ? DateTime.Parse(reviewDate.Value) : DateTime.Now
                        };

                        if (authorName != null)
                        {
                            review.Author = GetAuthor(authorName.Value);
                        }

                        currentBook.Reviews.Add(review);
                    }
                }

                db.Books.Add(currentBook);
                db.SaveChanges();
            }
        }

        private static Author GetAuthor(string authorName)
        {
            var author = db.Authors.FirstOrDefault(a => a.Name == authorName);
            if (author == null)
            {
                author = new Author
                {
                    Name = authorName
                };
            }

            return author;
        }
    }
}
