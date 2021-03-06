﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sneaker_Bar.Models
{
    public class ArticleRepository
    {
        private readonly ApplicationDbContext context;
        public ArticleRepository(ApplicationDbContext _context)
        {
            context = _context; 
        }
        public List<Article> getArticles()
        {
            List<Article> articles = context.Articles.OrderBy(x => x.Date).ToList<Article>();
            articles.Reverse();
            return articles;
        }
        public void AddViewById(int Id)
        {
            Article article = getArticleById(Id);
            article.Views++;
            SaveArticle(article);
        }
        public List<Article> getLatestArticles()
        {
            List<Article> articles = getArticles();
            List<Article> latestArticles = new List<Article>();
            int index = articles.Count > 3 ? 3 : articles.Count;

            for (int i = 0; i < index; i++)
            {
                latestArticles.Add(articles[i]);
            }
            return latestArticles;
        }

        public Article getArticleById(int Id) {
            return context.Articles.Single(x => x.Id == Id);
        }

        public int SaveArticle(Article article)
        {
            if (article.Id == default)
            {
                context.Entry(article).State = EntityState.Added;
            }
            else
            {
                context.Entry(article).State = EntityState.Modified;
            }
            context.SaveChanges();
            return article.Id;
        }

        public void DeleteArticle(Article article)
        {
            context.Articles.Remove(article);
            context.SaveChanges();
        }
    }
}
