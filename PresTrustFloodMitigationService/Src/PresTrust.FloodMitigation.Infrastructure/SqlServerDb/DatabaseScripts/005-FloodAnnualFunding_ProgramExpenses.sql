Insert Into [Flood].FloodAnnualFunding
(
    AllocationYear,
    AllocationAmount,
    Interest,
    AddedOrOmittedAmount,
    Comment,
    LastUpdatedBy,																
    LastUpdatedOn
)
select Distinct
    AF.AllocationYear,
    AF.AllocationAmount,
    AF.AllocationInterest,
    AF.AllocationAddedOmitted,
    AF.Comment,
'flood-admin' AS LastUpdatedBy,
GetDate() AS LastUpdatedOn
FROM [FloodMitigation].[floodmp].AnnualFundingAmounts AS AF
Insert Into [Flood].FloodProgramExpenses
(
    ExpenseYear,
    ExpenseAmount,
    ExpenseDate,
    CategoryId,
    Comment,
    LastUpdatedBy,																
    LastUpdatedOn
)
select 

PE.FYear,
PE.ExpenseAmount,
PE.ExpenseDate,
CASE
WHEN PE.ExpenseCategory = 'Food' THEN 1
WHEN PE.ExpenseCategory = 'Appraisal Services' THEN 2
WHEN PE.ExpenseCategory = 'Legal Services' THEN 3
WHEN PE.ExpenseCategory = 'Meeting Expences' THEN 4
WHEN PE.ExpenseCategory = 'Surveyor Fee' THEN 5
ELSE 0
END AS ExpenseCategory,
PE.Description,
'flood-admin' AS LastUpdatedBy,
GetDate() AS LastUpdatedOn
FROM [FloodMitigation].[floodmp]. ProgramExpenses AS PE