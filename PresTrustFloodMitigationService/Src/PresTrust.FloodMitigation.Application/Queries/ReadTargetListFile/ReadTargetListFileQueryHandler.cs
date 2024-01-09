using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Formats.Asn1;

namespace PresTrust.FloodMitigation.Application.Queries;

public class ReadTargetListFileQueryHandler : IRequestHandler<ReadTargetListFileQuery, Unit>
{
    public IWebHostEnvironment _webHostEnvironment;
    private HttpContext _content;

    public ReadTargetListFileQueryHandler
        (
        IWebHostEnvironment _webHostEnvironment,
        HttpContext _content
        )
    {
        this._webHostEnvironment = _webHostEnvironment;
        this._content = _content;
    }
    public async Task<Unit> Handle(ReadTargetListFileQuery request, CancellationToken cancellationToken)
    {
        var file = request.file;
        var fileextension = Path.GetExtension(file.FileName);
        var filename = Guid.NewGuid().ToString() + fileextension;
        string path = @"C:\\Downloads\";//static path

        var filepath = Path.Combine(path, file.FileName);
        using (FileStream fs = File.Create(filepath))
        {
            file.CopyTo(fs);
        }
        var csvData = File.ReadAllText(filepath).ToString();

        DataTable dt = new DataTable();
        bool firstRow = true;
        List<FloodParcelEntity> parcels = new List<FloodParcelEntity>();

        if (csvData.Length > 0)
        {
            foreach( var row in csvData.Split('\n')) {
                if (!string.IsNullOrEmpty(row))
                {
                    if (firstRow)
                    {
                        foreach(string cell in row.Split(','))
                        {
                            dt.Columns.Add(cell);
                        }
                        firstRow = false;
                    }else
                    {
                        dt.Rows.Add();
                        int i = 0;
                        foreach (string cell in row.Split(','))
                        {
                            if (i < dt.Columns.Count)
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Trim();
                               
                                i++;
                            }
                            
                        }
                    }
                }
            }
            //set data table to entity
            for (var i =0; i< dt.Rows.Count; i++)
            {
                parcels.Add(new FloodParcelEntity()
                {
                    PamsPin = dt.Rows[i]["PamsPin"].ToString(),
                    DateOfFLAP = new DateTime(),
                    IsFLAP = true,
                    StartNo = (int)dt.Rows[i]["StartNo"],
                    StreetAddress = dt.Rows[i]["StreetAddress"].ToString(),
                    LandOwner = dt.Rows[i]["LandOwner"].ToString(),
                    IsElevated = (bool)dt.Rows[i]["IsElevated"],
                    TargetArea = dt.Rows[i]["TargetArea"].ToString()
                });
            }
            List<int> ls = new List<int>() { 1, 2, 3 };
            _content.Session.SetString("key", "value"); // Set Session  
            //_content.Session.Set("key", ls);

            Console.WriteLine(parcels);
        }

            return Unit.Value;
    }
}
