using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using api.DTOs.UNPHUClient;
using api.Interfaces;
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

    /// <summary>
    /// Retrieves the current period by calling an external API.
    /// </summary>
    /// <returns>
    /// A <see cref="PeriodDto"/> representing the current period, or <c>null</c> if no period is found.
    /// </returns>
    /// <exception cref="HttpRequestException">
    /// Thrown if the API request fails or returns an error status.
    /// </exception>
    public async Task<PeriodDto?> GetCurrentPeriodAsync()
    {
        var response = await _httpClient.GetAsync("legacy/get-current-period");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<PeriodsDto>(content);

        return result?.Data?[0];
    }

    /// <summary>
    /// Asynchronously retrieves student data based on the provided username.
    /// </summary>
    /// <param name="username">The username of the student whose data is to be fetched.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. The task result is either
    /// a <see cref="StudentDataDto"/> containing the student's data, or <c>null</c> if no data is found.
    /// </returns>
    /// <exception cref="HttpRequestException">
    /// Thrown when the HTTP request fails, such as due to a network error or an unsuccessful status code.
    /// </exception>
    /// <exception cref="JsonException">
    /// Thrown if there is an error deserializing the response content into the expected <see cref="StudentDataDto"/> format.
    /// </exception>
    public async Task<StudentDataDto?> GetStudentAsync(string username)
    {
        var response = await _httpClient.GetAsync($"legacy/student-data/{username}");
        response.EnsureSuccessStatusCode();

        var student = await response.Content.ReadAsAsync<dynamic>();
        var data = student.data;

        if (data != null)
        {
            return ((JObject)data).ToObject<StudentDataDto>();
        }
        return null;
    }

    /// <summary>Gets a student's enrolled subject groups.</summary>
    /// <param name="username">Student ID to lookup</param>
    /// <returns>List of enrolled subjects or null if none found</returns>
    /// <remarks>Finds user, fetches memberships, and maps to DTOs.</remarks>
    public async Task<StudentCareerDto?> GetStudentCareerAsync(int id)
    {
        var response = await _httpClient.GetAsync($"legacy/get-student-careers/?IdPersona={id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<StudentCareersDto>(content);

        return result?.Data?.FirstOrDefault(x => x.PensumPrimario == true);
    }

    /// <summary>
    /// Retrieves a student's currently enrolled subjects from UNPHU's legacy system.
    /// </summary>
    /// <param name="user">The student user with valid UnphuId and CareerId</param>
    /// <returns>
    /// List of enrolled subjects as <see cref="SubjectGroup"/> objects, 
    /// or null if no enrollments found.
    /// </returns>
    /// <exception cref="Exception">
    /// Thrown when: 
    /// - Current academic period cannot be determined
    /// - API request fails
    /// </exception>
    /// <remarks>
    /// Fetches official enrollment data from UNPHU's legacy API for the current year/period.
    /// Requires valid UnphuId, CareerId and an active academic period.
    /// </remarks>
    public async Task<List<SubjectGroup>?> GetStudentEnrolledSubjectsAsync(AppUser user, int year, int periodId)
    {
        string url = $"legacy/officially-enrolled-subjects/?Ano={year}&IdPersona={user.UnphuId}&IdPeriodo={periodId}&IdCarrera={user.CareerId}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<StudentEnrolledSubjectsDto>(content);

        var subjectGroups = result?.Data?
            .Where(x => x.Observation?.ToUpper() != "R")
            .Select(x => new SubjectGroup
            {
                Code = x.GroupSubjectCode,
                Name = x.SubjectName,
                Credits = x.Credits,
            }).ToList();

        return subjectGroups;
    }
}
