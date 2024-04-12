using System.Linq;
using System.Reflection;

namespace PresTrust.FloodMitigation.Application.Queries;

public class ReadTargetListFileQueryHandler : IRequestHandler<ReadTargetListFileQuery, bool>
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
    public async Task<bool> Handle(ReadTargetListFileQuery request, CancellationToken cancellationToken)
    {
        var file = request.file;
        var fileextension = Path.GetExtension(file.FileName);
        var filename = Guid.NewGuid().ToString() + fileextension;
        string path = @"C:\\Downloads\";//static path
        bool hasError = false;

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
                    TargetArea = dt.Rows[i]["Target Area"].ToString() ?? string.Empty,
                    Block = dt.Rows[i]["Block"].ToString() ?? string.Empty,
                    Lot = dt.Rows[i]["Lot"].ToString() ?? string.Empty,
                    QCode = dt.Rows[i]["QCode"].ToString() ?? string.Empty,
                    StreetNo = dt.Rows[i]["House #"].ToString() ?? string.Empty,
                    StreetAddress = dt.Rows[i]["Street"].ToString() ?? string.Empty,
                    LandOwner = dt.Rows[i]["Homeowner"].ToString() ?? string.Empty,

                    AgencyName = dt.Rows[i]["Municipality"].ToString() ?? string.Empty,
                    PamsPin = string.Join('_', request.AgencyId, dt.Rows[i]["Block"], dt.Rows[i]["Lot"]),
                    DateOfFLAP = new DateTime(),
                    IsFLAP = true
                });
                if (!string.IsNullOrEmpty(dt.Rows[i]["QCode"].ToString()))
                {
                    parcels[i].PamsPin = string.Join('_', parcels[i].PamsPin, dt.Rows[i]["QCode"]);
                }
            }

            hasError =  CustomValidator(parcels, request.AgencyName);

            if (!hasError)
            {
                var importParcels = mapper.Map<List<ReadTargerListParcels>, List<FloodParcelEntity>>(parcels);
                //appending AgencyId to parcels
                importParcels.ForEach(parcel =>
                {
                    parcel.AgencyId = request.AgencyId;
                });

                //Set in cache
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set("ParcelsCache", importParcels, cacheEntryOptions);
            }

        }

            return !hasError;
    }

    public bool CustomValidator(List<ReadTargerListParcels> parcels, string agencyName)
    {
        string[] Errors = { "Value can't be null"};
        bool hasError = false;

        foreach (var myObject in parcels)
        {
            foreach (PropertyInfo property in myObject.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(string) && property.Name != "QCode")
                {
                    string value = (string)property.GetValue(myObject);
                    if (string.IsNullOrEmpty(value))
                    {
                        hasError = true;
                        throw new Exception("No data is empty");
                    }
                    else if (myObject.AgencyName != agencyName)
                    {
                        hasError = true;
                        throw new Exception("Not a valid Agency");
                    }
                }
            }
        }
        
        return hasError;
    }
}
