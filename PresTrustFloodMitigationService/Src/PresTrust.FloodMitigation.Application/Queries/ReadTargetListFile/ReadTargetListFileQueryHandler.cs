using System.Reflection;

namespace PresTrust.FloodMitigation.Application.Queries;

public class ReadTargetListFileQueryHandler : IRequestHandler<ReadTargetListFileQuery, Unit>
{
    private IMemoryCache _cache;
    private IMapper mapper;

    public ReadTargetListFileQueryHandler
        (
        IMemoryCache _cache,
        IMapper mapper
        )
    {
        this._cache = _cache ?? throw new ArgumentNullException(nameof(_cache));
        this.mapper = mapper;
    }
    public async Task<Unit> Handle(ReadTargetListFileQuery request, CancellationToken cancellationToken)
    {
        var file = request.file;
        var fileextension = Path.GetExtension(file.FileName);
        var filename = Guid.NewGuid().ToString() + fileextension;
        string path = @"C:\\Downloads\";//static path

        var filepath = Path.Combine(path, file.FileName);
        string directory = Path.GetDirectoryName(filepath) ?? string.Empty;

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        using (FileStream fs = File.Create(filepath))
        {
            file.CopyTo(fs);
        }
        var csvData = File.ReadAllText(filepath).ToString();

        DataTable dt = new DataTable();
        bool firstRow = true;
        List<ReadTargerListParcels> parcels = new List<ReadTargerListParcels>();

        if (csvData.Length > 0)
        {
            foreach(string row in csvData.Split('\n')) {
                if (!string.IsNullOrEmpty(row))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        if (firstRow)
                        {
                            foreach (string cell in row.Split(','))
                            {
                                dt.Columns.Add(cell.Trim());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            dt.Rows.Add();
                            int i = 0;
                            foreach (string cell in Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
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
                parcels.Add(new ReadTargerListParcels()
                {
                    AgencyId = request.AgencyId.ToString(),
                    PamsPin = dt.Rows[i]["PamsPin"].ToString() ?? string.Empty,
                    Block = dt.Rows[i]["Block"].ToString() ?? string.Empty,
                    Lot = dt.Rows[i]["Lot"].ToString() ?? string.Empty,
                    DateOfFLAP = new DateTime(),
                    IsFLAP = true,
                    StreetNo = dt.Rows[i]["StreetNo"].ToString() ?? string.Empty,
                    StreetAddress = dt.Rows[i]["StreetAddress"].ToString() ?? string.Empty,
                    LandOwner = dt.Rows[i]["OwnersName"].ToString() ?? string.Empty,
                    TargetArea = dt.Rows[i]["TargetArea"].ToString() ?? string.Empty
                });
            }

            CustomValidator(parcels, request.AgencyId);

            var importParcels = mapper.Map<List<ReadTargerListParcels>, List<FloodParcelEntity>>(parcels);


            //Set in cache
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
            _cache.Set("ParcelsCache", importParcels, cacheEntryOptions);
        }

            return Unit.Value;
    }

    public bool CustomValidator(List<ReadTargerListParcels> parcels, int agencyId)
    {
        string[] Errors = { "Value can't be null"};
        bool check = false;

        foreach (var myObject in parcels)
        {
            foreach (PropertyInfo property in myObject.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(string))
                {
                    string value = (string)property.GetValue(myObject);
                    if (string.IsNullOrEmpty(value))
                    {
                        check = true;
                        throw new ApiModelValidationException(Errors);
                    }
                    //else if (myObject.AgencyId != agencyId.ToString())
                    //{
                    //    check = true;
                    //    throw new ApiModelValidationException(Errors);
                    //}
                }
            }
        }
        
        return check;
    }
}
