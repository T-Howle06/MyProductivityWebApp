#nullable disable
using System;
using System.Collections.Generic;

namespace MyProductivityWebApp.Data.Data
{
    public partial class ToDoList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string UserName { get; set; }
    }
}