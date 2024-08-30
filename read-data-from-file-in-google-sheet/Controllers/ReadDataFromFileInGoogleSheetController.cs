using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Mvc;

namespace read_data_from_file_in_google_sheet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadDataFromFileInGoogleSheetController : ControllerBase
    {
        private readonly ILogger<ReadDataFromFileInGoogleSheetController> _logger;

        static readonly string[] scopes = {SheetsService.Scope.Spreadsheets};
        static readonly string ApplicationName = "name ApplicationName";
        static readonly string SpreadsheetId = "asdasdasdasdasdasdasdasdasdasdasdasd";
        static readonly string Sheet = "testAAAA";
        static readonly string jsonConfigFile = "third-arcadia-432506-p4-093f7f41dd68.json";

        static SheetsService service;

        public ReadDataFromFileInGoogleSheetController (ILogger<ReadDataFromFileInGoogleSheetController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Get")]
        public IActionResult Get()
        {
            GoogleCredential credential;
            using (var stream = new FileStream(jsonConfigFile, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(scopes);
            }

            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // read data
            var range = $"{Sheet}!A1:F57";
            var request = service.Spreadsheets.Values.Get(SpreadsheetId, range);

            var response = request.Execute();
            var values = response.Values;
            if (values != null && values.Count > 0)
            {
                //foreach (var item in values)
                //{
                //    //var row = item;
                //    //var data1 = row[1] ?? ""; // column 1
                //    //var data2 = row[2] ?? ""; // column 2

                //    // get data it !!
                //}
            }

            return Ok(values);
        }

    }
}
