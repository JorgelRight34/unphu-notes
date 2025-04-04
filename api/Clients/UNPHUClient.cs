using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using api.DTOs.UNPHUClient;
using api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace api.Clients;

public class UNPHUClient : IUNPHUClient
{
    private readonly HttpClient _httpClient;

    public UNPHUClient(IHttpClientFactory httpClientFactory)
    {
        if (httpClientFactory == null) throw new Exception("Client factory can't be null");
        _httpClient = httpClientFactory.CreateClient("UNPHU");
    }

    public async Task<PeriodDto?> GetCurrentPeriodAsync()
    {
        var response = await _httpClient.GetAsync("legacy/get-current-period");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<PeriodsDto>(content);

        return result?.Data?[0];
    }

    public async Task<StudentDataDto?> GetStudentAsync(string username)
    {
        var response = await _httpClient.GetAsync($"legacy/student-data/{username}");
        if (response.IsSuccessStatusCode)
        {
            var student = await response.Content.ReadAsAsync<dynamic>();
            var data = student.data;
            if (data != null)
            {
                return ((JObject)data).ToObject<StudentDataDto>();
            }
        }
        return null;
    }

    public async Task<StudentCareerDto?> GetStudentCareerAsync(int id)
    {
        var response = await _httpClient.GetAsync($"legacy/get-student-careers/?IdPersona={id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<StudentCareersDto>(content);

        return result?.Data?.FirstOrDefault(x => x.PensumPrimario == true);
    }

    public async Task<List<SubjectGroup>?> GetStudentEnrolledSubjectsAsync(AppUser user)
    {
        int year = DateTime.Now.Year;
        var period = await GetCurrentPeriodAsync() ?? throw new Exception("Period wasn't found");

        string url = $"legacy/officially-enrolled-subjects/?Ano={year}&IdPersona={user.UnphuId}&IdPeriodo={period?.Id}&IdCarrera={user.CareerId}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<StudentEnrolledSubjectsDto>(content);

        var subjectGroups = result?.Data?.Select(x => new SubjectGroup
        {
            Code = x.GroupSubjectCode,
            Name = x.SubjectName,
            Credits = x.Credits,
        }).ToList();

        return subjectGroups;
    }
}
