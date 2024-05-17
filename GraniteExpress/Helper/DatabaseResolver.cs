using Blazored.LocalStorage;
using GraniteExpress.Infrastructure;
using GraniteExpress.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace TeamWorkServer.Services
{
    public interface IDatabaseResolver
    {
        //string GetConnectionString(string databaseName);
    }

    public class DatabaseResolver : IDatabaseResolver
    {
        //public string GetConnectionString(string databaseName)
        //{
        //    string connectionString = $"Server={AppSettings.Settings.ServerName};Database={databaseName};";
        //    if (!string.IsNullOrEmpty(AppSettings.Settings.DatabaseUserId))
        //    {
        //        connectionString += $"User Id={AppSettings.Settings.DatabaseUserId};";
        //    }
        //    if (!string.IsNullOrEmpty(AppSettings.Settings.DatabasePassword))
        //    {
        //        connectionString += $"Password={AppSettings.Settings.DatabasePassword};";
        //    }

        //    connectionString += "Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
        //    return connectionString;
        //}
    }
}
