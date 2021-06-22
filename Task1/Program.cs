using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Task1.Models;
using System;

namespace Task1
{
    class Program
    {
        private static readonly HttpClient client = new();
        
        static async Task Main(string[] args)
        {
            await GetUserTasksCountPerProject(109);
        }

        private static async Task<Dictionary<int, int>> GetUserTasksCountPerProject(int id)
        {
            List<ProjectTask> tasks = await GetTasks();
            
            return tasks
                .Where(t => t.performerId == id)
                .GroupBy(t => t.projectId)
                .ToDictionary(g => g.Key, g => g.Count());
        }
        private static async Task<List<ProjectTask>> GetUserTasksByID(int id)
        {
            List<ProjectTask> allTasks = await GetTasks();
            return allTasks
                .Where(t => t.performerId == id)
                .Where(t => t.name.Length < 45)
                .ToList();
        }
        private static async Task<List<TaskIdToName>> GetUserFinishedTasks(int id)
        {
            var periodFrom = new DateTime(2021, 1, 1, 0, 0, 0);
            var periodTo = new DateTime(2021, 12, 31, 23, 59, 0);
            List<ProjectTask> allTasks = await GetTasks();
            return allTasks
                .Where(t => t.performerId == id)
                .Where(t => t.finishedAt > periodFrom)
                .Where(t => t.finishedAt < periodTo)
                .Select(t => new TaskIdToName { Id = t.id, Name = t.name})
                .ToList();
        }

        private static async Task<List<UserToTasks>> GetSortedUsersWithTasks()
        {
            List<ProjectTask> allProjectTasks = await GetTasks();
            return allProjectTasks
                .GroupBy(t => t.performerId)
                .Select( g => new UserToTasks { UserName =  GetUserById(g.Key).Result.firstName, ProjectTasks = g.OrderBy(t => t.name.Length).ToList() })
                .OrderBy(o => o.UserName)
                .ToList();
        }



        static async Task<List<Project>> GetProjects()
        {
            HttpResponseMessage response = await client.GetAsync("https://bsa21.azurewebsites.net/api/Projects");
            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Project>>(body);
        }

        static async Task<Project> GetProjectById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"https://bsa21.azurewebsites.net/api/Projects/{id}");
            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Project>(body);
        }

        static async Task<List<ProjectTask>> GetTasks()
        {
            HttpResponseMessage response = await client.GetAsync("https://bsa21.azurewebsites.net/api/Tasks");
            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<ProjectTask>>(body);
        }

        static async Task<ProjectTask> GetTaskById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"https://bsa21.azurewebsites.net/api/Tasks/{id}");
            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ProjectTask>(body);
        }

        static async Task<List<Team>> GetTeams()
        {
            HttpResponseMessage response = await client.GetAsync("https://bsa21.azurewebsites.net/api/Teams");
            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Team>>(body);
        }

        static async Task<Team> GetTeamById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"https://bsa21.azurewebsites.net/api/Team/{id}");
            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Team>(body);
        }

        static async Task<List<User>> GetUsers()
        {
            HttpResponseMessage response = await client.GetAsync("https://bsa21.azurewebsites.net/api/Users");
            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<User>>(body);
        }

        static async Task<User> GetUserById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"https://bsa21.azurewebsites.net/api/Users/{id}");
            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<User>(body);
        }
    }
}
