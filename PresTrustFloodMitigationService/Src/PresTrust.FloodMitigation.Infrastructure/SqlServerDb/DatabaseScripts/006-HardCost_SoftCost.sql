--Run One After another while migrating------------------------- 
update [Flood].[FloodApplicationOverview] 
set NatlDisaster = 1 where NatlDisasterId is not null

update [Flood].[FloodApplicationOverview]
set FEMA_OR_NJDEP_Applied =1 where 
                   [FEMAApplied] =1
				   or [GreenAcresApplied] =1
				   or [BlueAcresApplied] =1


				
---------To Update SoftCost and HardCost-------------------------

UPDATE [Flood].[FloodParcelPayment]
  SET HardCostPaymentStatusId = 1, HardCostPaymentTypeId = 2

  FROM [Flood].[FloodParcelPayment] P
  INNER JOIN [Flood].[FloodApplication] A ON A.Id = P.ApplicationId
  INNER JOIN [Flood].[FloodApplicationStatus] S ON S.Id = A.StatusId
  INNER JOIN [Flood].[FloodParcelFinance] F ON F.PamsPin = p.PamsPin AND F.ApplicationId = P.ApplicationId
  WHERE (HardCostFMPAmt is not null AND HardCostFMPAmt <> 0) AND HardCostPaymentDate is not null


    --soft costs
  UPDATE [Flood].[FloodParcelPayment]
  SET SoftCostPaymentStatusId = 1, SoftCostPaymentTypeId = 2

  FROM [Flood].[FloodParcelPayment] P
  INNER JOIN [Flood].[FloodApplication] A ON A.Id = P.ApplicationId
  INNER JOIN [Flood].[FloodApplicationStatus] S ON S.Id = A.StatusId
  INNER JOIN [Flood].[FloodParcelFinance] F ON F.PamsPin = p.PamsPin AND F.ApplicationId = P.ApplicationId
  WHERE (SoftCostFMPAmt is not null AND SoftCostFMPAmt <> 0) AND SoftCostPaymentDate is not null