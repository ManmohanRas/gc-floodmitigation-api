namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class TransferParcelSqlCommand
{
    private readonly string _sqlCommand =
            @"  UPDATE Flood.FloodApplicationParcel
				SET IsSubmitted = (SELECT IsSubmitted FROM Flood.FloodApplicationParcel WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin),
					IsApproved = (SELECT IsApproved FROM Flood.FloodApplicationParcel WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin)
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelComment
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelComment
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					Comment,
					CommentTypeId,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelComment
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelFeedback
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelFeedback
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					SectionId,
					Feedback,
					RequestForCorrection,
					CorrectionStatus,
					MarkRead,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelFeedback
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelProperty
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelProperty
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					Priority,
					ValueEstimate,
					EstimatedPurchasePrice,
					BRV,
					NfipPolicyNo,
					SourceOfValueEstimate,
					FirstFloorElevation,
					StructureType,
					FoundationType,
					OccupancyClass,
					PercentageOfDamage,
					HasContaminants,
					IsLowIncomeHousing,
					HasHistoricSignificance,
					IsRentalProperty,
					RentPerMonth,
					NeedSoftCost,
					IsPreIrenePropertyOwner,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelProperty
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelSoftCost
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelSoftCost
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					SoftCostTypeId,
					VendorName,
					InvoiceAmount,
					PaymentAmount,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelSoftCost
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelFinance
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelFinance
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					EstimatePurchasePrice,
					AdditionalSoftCostEstimate,
					AppraisedValue,
					AMV,
					TotalFEMABenifits,
					DOBAffidavitType,
					DOBAffidavitAmt,
					HardCostFMPAmt,
					SoftCostFMPAmt,
					AppraisersFee,
					SurveyorsFee,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelFinance
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelTech
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelTech
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					FEMASevereRepetitiveLossList,
					FEMARepetitiveLossList,
					IsthepropertywithinthePassaicRiverBasin,
					IsthepropertywithinFloodway,
					IsthepropertywithinFloodplain,
					Claim10Years,
					TotalOfClaims,
					BenefitCostRatio,
					FEMACommunityId,
					FirmEffectiveDate,
					FirmPanel,
					FirmPanelFinal,
					FloodZoneDesignation,
					BaseFloodElevation,
					BaseFloodElevationFinal,
					RiverId,
					RiverIdFinal,
					FisEffectiveDate,
					FloodProfile,
					FloodProfileFinal,
					FloodSource,
					FirstFloodElevation,
					FirstFloodElevationFinal,
					StreambedElevation,
					StreambedElevationFinal,
					ElevationBeforeMitigation,
					ElevationBeforeMitigationFinal,
					FloodType,
					TenPercent,
					TwoPercent,
					OnePercent,
					PointOnePercent,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelTech
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelPayment
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelPayment
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					HardCostPaymentTypeId,
					HardCostPaymentDate,
					HardCostPaymentStatusId,
					SoftCostPaymentTypeId,
					SoftCostPaymentDate,
					SoftCostPaymentStatusId,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelPayment
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelDocument
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelDocument
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					DocumentTypeId,
					FileName,
					Title,
					Description,
					ShowCommittee,
					UseInReport,
					HardCopy,
					Approved,
					ReviewComment,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelDocument
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelSurvey
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelSurvey
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					Surveyor,
					SurveyDate,
					LastRevision,
					DateCorrected,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelSurvey
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelTracking
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelTracking
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					ClosingDate,
					DeedBook,
					DeedPage,
					DeedDate,
					DemolitionDate,
					SiteVisitConfirmDate,
					PublicPark,
					RainGarden,
					CommunityGarden,
					ActiveRecreation,
					NaturalHabitat,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelTracking
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;

				DELETE FROM Flood.FloodParcelAdminDetails
				WHERE ApplicationId = @p_TransferApplicationId AND PamsPin = @p_PamsPin;
				INSERT INTO Flood.FloodParcelAdminDetails
				SELECT
					@p_TransferApplicationId AS ApplicationId,
					PamsPin,
					DOBDocumentsMissingDate,
					FMCFinalApprovalDate,
					FMCFinalNumber,
					BCCFinalApprovalDate,
					BCCFinalNumber,
					MunicipalPurchaseDate,
					MunicipalPurchaseNumber,
					GrantAgreementDate,
					GrantAgreementExpirationDate,
					DueDiligenceDocumentsMissingDate,
					ScheduleClosingDate,
					SoftCostReimbursementRequestDate,
					FMCSoftCostReimbApprovalDate,
					FMCSoftCostReimbApprovalNumber,
					BCCSoftCostReimbApprovalDate,
					BCCSoftCostReimbApprovalNumber,
					DoesHomeOwnerHaveNFIPInsurance,
					IsDEPInvolved,
					IsPARRequestedbyFunder,
					LastUpdatedBy,
					LastUpdatedOn
				FROM Flood.FloodParcelAdminDetails
				WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;";

    public TransferParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
