TRUNCATE TABLE [Flood].[FloodParcel];

WITH CoreParcelCTE AS
(
	SELECT
		*
	FROM
	(
		SELECT
			ROW_NUMBER() OVER(ORDER BY PAMS_PIN ASC, OBJECTID ASC) AS RowNo,
			[PAMS_PIN] AS PamsPin,
			MunicipalID AS AgencyID,
			Block AS Block,
			Lot AS Lot,
			QualificationCode AS QualificationCode,
			ISNULL(PropertyLocation, '') AS StreetNoStreetAddress,
			CalculatedAcreage AS Acreage,
			OwnersName AS OwnersName,
			NULL AS OwnersAddress1,
			NULL AS OwnersAddress2,
			BuildingSquareFeet AS SquareFootage,
			YearConstructed AS YearOfConstruction,
			NetTaxableValue AS TotalAssessedValue,
			LandValue AS LandValue,
			ImprovementValue AS ImprovementValue,
			LastYearTotalTaxes AS AnnualTaxes,
			0 AS IsFLAP,
			NULL AS DateOfFLAP,
			'flood-admin' AS LastUpdatedBy,
			GetDate() AS LastUpdatedOn,
			1 AS IsActive
		FROM		[Core].[Parcels]
	) CoreParcels
)
INSERT INTO [Flood].[FloodParcel]
(
	PamsPin,
	AgencyID,
	Block,
	Lot,
	QualificationCode,
	StreetNo,
	StreetAddress,
	Acreage,
	OwnersName,
	OwnersAddress1,
	OwnersAddress2,
	SquareFootage,
	YearOfConstruction,
	TotalAssessedValue,
	LandValue,
	ImprovementValue,
	AnnualTaxes,
	IsFLAP,
	DateOfFLAP,
	LastUpdatedBy,
	LastUpdatedOn,
	IsActive
)
SELECT
	PamsPin,
	AgencyID,
	Block,
	Lot,
	QualificationCode,
	TRIM(StreetNo),
	TRIM(StreetAddress),
	Acreage,
	OwnersName,
	OwnersAddress1,
	OwnersAddress2,
	SquareFootage,
	YearOfConstruction,
	TotalAssessedValue,
	LandValue,
	ImprovementValue,
	AnnualTaxes,
	IsFLAP,
	DateOfFLAP,
	LastUpdatedBy,
	LastUpdatedOn,
	IsActive
FROM
(
	SELECT
		ROW_NUMBER() OVER(PARTITION BY RowNo,AnnualTaxes ORDER BY RowNo ASC) AS StreetNoRow,
		value AS StreetNo,
		SUBSTRING(StreetNoStreetAddress, (LEN(value) + 1), LEN(StreetNoStreetAddress)) AS StreetAddress,
		*
	FROM CoreParcelCTE
	CROSS APPLY STRING_SPLIT(StreetNoStreetAddress, ' ')
) Parcel
WHERE Parcel.StreetNoRow = 1;
