using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using FluentNHibernate;

namespace TODO.Entities
{
    public class TaskToDo
    {
        public virtual int Id { get; set; }

        public virtual String Date { get; set; }

        public virtual String Description { get; set; }

        public virtual int Status { get; set; }


    }

}