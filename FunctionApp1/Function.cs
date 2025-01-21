using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ValidateCpf
{
    public class Function
    {
        private readonly ILogger<Function> _logger;

        public Function(ILogger<Function> logger)
        {
            _logger = logger;
        }

        [Function("ValidateCpf")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("Processing request to validate CPF.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string cpf = data?.cpf;

            if (string.IsNullOrEmpty(cpf))
            {
                return new BadRequestObjectResult("Please provide a CPF.");
            }

            if (IsValidCPF(cpf))
            {
                return new OkObjectResult(new { valid = true, message = "CPF is valid." });
            }
            else
            {
                return new BadRequestObjectResult(new { valid = false, message = "CPF is invalid." });
            }
        }

        private static bool IsValidCPF(string cpf)
        {
            // Remove non-numeric characters
            cpf = Regex.Replace(cpf, "[^0-9]", "");

            if (cpf.Length != 11 || Regex.IsMatch(cpf, "^(\\d)\\1{10}$"))
            {
                return false;
            }

            int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
            }

            int remainder = sum % 11;
            int firstDigit = remainder < 2 ? 0 : 11 - remainder;

            tempCpf += firstDigit;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            }

            remainder = sum % 11;
            int secondDigit = remainder < 2 ? 0 : 11 - remainder;

            return cpf.EndsWith(firstDigit.ToString() + secondDigit.ToString());
        }
    }
}
