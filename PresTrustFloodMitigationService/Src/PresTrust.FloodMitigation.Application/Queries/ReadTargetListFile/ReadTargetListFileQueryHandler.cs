namespace PresTrust.FloodMitigation.Application.Queries;

public class ReadTargetListFileQueryHandler : IRequestHandler<ReadTargetListFileQuery, Unit>
{
    private readonly IHttpContextAccessor accessor;

    public ReadTargetListFileQueryHandler
        (
        IHttpContextAccessor accessor
        )
    {
        this.accessor = accessor;
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
                    StreetNo = dt.Rows[i]["StreetNo"].ToString(),
                    StreetAddress = dt.Rows[i]["StreetAddress"].ToString(),
                    LandOwner = dt.Rows[i]["OwnersName"].ToString(),
                    //IsElevated = (bool)dt.Rows[i]["IsElevated"],
                    TargetArea = dt.Rows[i]["TargetArea"].ToString()
                });
            }
            accessor.HttpContext.Session.SetString("Parcels", JsonConvert.SerializeObject(parcels)); // Set Session  
        }

            return Unit.Value;
    }
}
