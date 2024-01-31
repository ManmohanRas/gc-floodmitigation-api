using Microsoft.Extensions.Caching.Memory;
using System.IO;

namespace PresTrust.FloodMitigation.Application.Queries;

public class ReadTargetListFileQueryHandler : IRequestHandler<ReadTargetListFileQuery, Unit>
{
    private IMemoryCache _cache;

    public ReadTargetListFileQueryHandler
        (
        IMemoryCache _cache
        )
    {
        this._cache = _cache ?? throw new ArgumentNullException(nameof(_cache));
    }
    public async Task<Unit> Handle(ReadTargetListFileQuery request, CancellationToken cancellationToken)
    {
        var file = request.file;
        var fileextension = Path.GetExtension(file.FileName);
        var filename = Guid.NewGuid().ToString() + fileextension;
        string path = @"C:\\Downloads\";//static path

        var filepath = Path.Combine(path, file.FileName);
        string directory = Path.GetDirectoryName(filepath);

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
        List<FloodParcelEntity> parcels = new List<FloodParcelEntity>();

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
                            foreach (string cell in row.Split(','))
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
                    AgencyId = request.AgencyId,
                    PamsPin = dt.Rows[i]["PamsPin"].ToString(),
                    DateOfFLAP = new DateTime(),
                    IsFLAP = true,
                    StreetNo = dt.Rows[i]["StreetNo"].ToString(),
                    StreetAddress = dt.Rows[i]["StreetAddress"].ToString(),
                    LandOwner = dt.Rows[i]["OwnersName"].ToString(),
                    TargetArea = dt.Rows[i]["TargetArea"].ToString()
                });
            }

            //Set in cache
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
            _cache.Set("ParcelsCache", parcels, cacheEntryOptions);
        }

            return Unit.Value;
    }
}
