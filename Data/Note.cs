#nullable disable
using System;
using System.Collections.Generic;

namespace MyProductivityWebApp.Data.Data
{
    public partial class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string UserName { get; set; }
    }
}