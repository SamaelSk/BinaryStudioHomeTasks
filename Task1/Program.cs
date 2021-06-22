using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1
{
    class Program
    {
        private static readonly HttpClient client = new();
        
        static async Task Main(string[] args)
        {
            await GetUserTasksCountPerProject();
        }

        private static async Task GetUserTasksCountPerProject()
        {
            List<ProjectTask> tasks = await GetTasks();
            
            Dictionary<int, int> grouped = tasks
                .Where(t => t.performerId == 109)
                .GroupBy(t => t.projectId)
                .ToDictionary(g => g.Key, g => g.Count());
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
