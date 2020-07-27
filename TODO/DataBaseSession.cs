using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Entities;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Linq;
using System.Windows.Forms;
namespace TODO
{
    public class DataBaseSession
    {

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                  SQLiteConfiguration.Standard
                    .UsingFile("Tasks.db")
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<TaskToDo>())
                .BuildSessionFactory();

        }



        //Funkcja zwraca listę, która zawiera wszystkie zadania dla daty podanej jako argument
        public static List<TaskToDo> getListByDate(String date)
        {
            var sessionFactory = CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {

                var toDoList = session.Query<TaskToDo>()
                   .Where(x => x.Date == date).ToList();
                return toDoList;
            }

        }


        //Funkcja zmieniająca status dla konkretnego zadania w bazie danych
        public static void changeCheckStatus(int id, int status)
        {
            var sessionFactory = CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
                if (status == 1)
                {
                    session.Query<TaskToDo>()
                    .Where(x => x.Id == id)
                        .Update(x => new TaskToDo { Status = 1 });
                }
                else
                {
                    session.Query<TaskToDo>()
                    .Where(x => x.Id == id)
                        .Update(x => new TaskToDo { Status = 0 });
                }
        }

        //Funkcja dodająca zadanie do listy
        public static TaskToDo addTaskToDo(String date, String task)
        {
            var sessionFactory = CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                    var taskToDo = new TaskToDo { Date = date, Description = task, Status = 0 };
                    session.SaveOrUpdate(taskToDo);
                    return taskToDo;
            }
          
        }



        //Funkcja usuwająca konkretne zadanie z bazy
        public static void deleteTaskToDo(int id)
        {
            var sessionFactory = CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                session.Query<TaskToDo>()
                    .Where(x => x.Id == id)
                        .Delete();
            }
         
        }

        //Funkcja edytująca konkretne zadanie z bazy
        public static void editTaskToDo(int id, String description)
        {
            var sessionFactory = CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                session.Query<TaskToDo>()
                .Where(x => x.Id == id)
                    .Update(x => new TaskToDo { Description = description });
            }

        }

    }
    
}
