using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TODO.Entities;

namespace TODO.Mappings
{
    public class TaskToDoMap : ClassMap<TaskToDo>
    {
        public TaskToDoMap()
        {
            Id(x => x.Id);
            Map(x => x.Date);
            Map(x => x.Description);
            Map(x => x.Status);
            Table("tasks");

        }
    }
}
