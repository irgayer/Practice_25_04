using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUp;
using Dapper;

namespace Practice_25_04
{
    public class NewsApp
    {
        const int MAX_MAINMENU_CNT = 3;
        const int MAX_INNERMENU_CNT = 2;
        readonly string connectionString;

        private List<Comment> comments;
        private List<New> news;

        public NewsApp(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Run()
        {
            comments = new List<Comment>();
            news = new List<New>();

            bool flag = true;
            while (flag)
            {
                switch (MainMenu())
                {
                    case 1:
                        {
                            PrintNews();
                            switch (InnerMenu())
                            {
                                case 1:
                                    {
                                        AddComment();
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:
                        {
                            AddNew();
                            break;
                        }
                    case 3:
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }
        private int MainMenu()
        {
            Console.WriteLine("Выберите действие: ");
            Console.WriteLine("1) Просмотреть новости");
            Console.WriteLine("2) Добавить новость");
            Console.WriteLine("3) Выйти");

            if(int.TryParse(Console.ReadLine(), out int menu))
            {
                if(menu > 0 && menu <= MAX_MAINMENU_CNT)
                {
                    return menu;
                }
            }
            return -1;
        }

        private void PrintNews()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                news = connection.Query<New>("select * from News").ToList();
                comments = connection.Query<Comment>("select * from comments").ToList();
                foreach (var n in news)
                {
                    Console.WriteLine(n);
                    Console.WriteLine("Комментарии: ");
                    var commentsToNew = comments.Where(comment => comment.IdNew == n.Id).ToList();
                    foreach(var comment in commentsToNew)
                    {
                        Console.WriteLine(comment);
                    }
                    Console.WriteLine("-----------------------");
                }
            }
        }
        private void AddNew()
        {    
            New newNew = new New();
            string authorNew, titleNew, maintextNew;
            
            Console.WriteLine("Введите свое имя: ");
            authorNew = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(authorNew))
            {
                Console.WriteLine("Введите заголовок: ");
                titleNew = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(titleNew))
                {
                    Console.WriteLine("Введите главный текст: ");
                    maintextNew = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(maintextNew))
                    {
                        Console.WriteLine("Введите детальный текст: ");

                        newNew.DetailText = Console.ReadLine();
                        newNew.PublishTime = DateTime.Now;
                        newNew.Author = authorNew;
                        newNew.MainText = maintextNew;
                        newNew.Title = titleNew;
                        
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Execute("insert into New values(@Id,@Title,@MainText,@DetailText,@Author,@PublishTime)", newNew);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Обязательно должен быть главный текст");
                    }

                }
                else
                {
                    Console.WriteLine("Обязательно должен быть заголовок!");
                }
            }
            else
            {
                Console.WriteLine("Автор пустой!");
            }      
        }
        private int InnerMenu()
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1) Оставить комментарий");
            Console.WriteLine("2) Закрыть\n");

            if(int.TryParse(Console.ReadLine(), out int menu))
            {
                if(menu > 0 && menu <= MAX_INNERMENU_CNT)
                {
                    return menu;
                }
            }
            return -1;
        }
        private void AddComment()
        {
            for(int i = 0; i < news.Count; i++)
            {
                Console.WriteLine($"{i + 1})");
                Console.WriteLine(news[i]);
                Console.WriteLine("-----------------------");
            }
            Console.WriteLine("Введите индекс новости: ");

            if(int.TryParse(Console.ReadLine(), out int index))
            {
                if(index > 0 && index <= news.Count)
                {
                    string commentAuthor, commentText;
                    Guid id = news[index - 1].Id;

                    Console.WriteLine("Введите свое имя: ");
                    commentAuthor = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(commentAuthor))
                    {
                        Console.WriteLine("Введите текст: ");
                        commentText = Console.ReadLine();

                        using (var connection = new SqlConnection(connectionString))
                        {
                            Comment newComment = new Comment();
                            newComment.Id = id;
                            newComment.NickName = commentAuthor;
                            newComment.Text = commentText;
                            newComment.PublishTime = DateTime.Now;

                            connection.Execute("insert into comment values(@Id,@NickName,@Text,@PublishTime,@IdNew)", newComment);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Автор пустой!");
                    }
                }
            }
        }
    }
}
