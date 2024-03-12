using MyProductivityWebApp.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace MyProductivityWebApp.Data
{
    public class ToDoListService
    {
        private readonly MyProductivityWebAppContext _myProductivityWebAppContext;

        public ToDoListService(MyProductivityWebAppContext myProductivityWebAppContext)
        {
            _myProductivityWebAppContext = myProductivityWebAppContext;
        }

        public async Task<List<ToDoList>> GetToDoListsAsync(string currentUser)
        {
            // Get todo lists
            return await _myProductivityWebAppContext.ToDoLists
                .Where(u => u.UserName == currentUser)
                .AsNoTracking().ToListAsync();
        }

        public Task<ToDoList> CreateToDoListAsync(ToDoList toDoList)
        {
            _myProductivityWebAppContext.ToDoLists.Add(toDoList);
            _myProductivityWebAppContext.SaveChanges();
            return Task.FromResult(toDoList);
        }

        public Task<bool> UpdateToDoListAsync(ToDoList toDoList)
        {
            var ExistingToDoList = _myProductivityWebAppContext.ToDoLists
                .Where(t => t.Id == toDoList.Id)
                .FirstOrDefault();

            if (ExistingToDoList != null)
            {
                ExistingToDoList.Title = toDoList.Title;
                ExistingToDoList.Description = toDoList.Description;
                ExistingToDoList.DueDate = toDoList.DueDate;
                _myProductivityWebAppContext.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteToDoListAsync(ToDoList toDoList)
        {
            var ExistingToDoList = _myProductivityWebAppContext.ToDoLists
                .Where(t => t.Id == toDoList.Id)
                .FirstOrDefault();

            if (ExistingToDoList != null)
            {
                _myProductivityWebAppContext.ToDoLists.Remove(ExistingToDoList);
                _myProductivityWebAppContext.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
