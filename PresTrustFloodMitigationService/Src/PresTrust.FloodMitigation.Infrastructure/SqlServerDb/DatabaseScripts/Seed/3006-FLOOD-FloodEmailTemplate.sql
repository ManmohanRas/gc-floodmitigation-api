DELETE FROM [Flood].[FloodEmailTemplate];

--Project area flow
INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    1, 
    'CHANGE_STATUS_FROM_DOI_DRAFT_TO_DOI_SUBMITTED', 
    'Change status from DOI Draft to DOI Submitted', 
    'Morris County Flood Mitigation Program – Declaration Of Intent Submitted', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>This email is to inform you that the Declaration of Intent for {{ApplicationName}} has been submitted and is under review by County staff. Some additional documents or steps may be necessary to complete your submission. If so, you&rsquo;ll be notified by email.</p>
<p>As Morris County staff reviews the Declaration of Intent, staff will prepare feedback comments for any section(s) that require further clarification or attention. Once approved, an email will be sent. From there, you&rsquo;ll be able to log back into the Flood Mitigation Program Program Portal and complete the application.</p>
<p>Please contact me if you have any questions or concerns.<br>
 <br>
Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>', 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    2, 
    'CHANGE_STATUS_FROM_DOI_SUBMITTED_TO_DOI_APPROVED', 
    'Change status from DOI Submitted to Draft Application', 
    'Morris County Flood Mitigation Program – Declaration Of Intent Approved', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>This email is to inform you that the Declaration of Intent for {{ApplicationName}} has been approved by County staff.</p>
<p>The draft application is now open. Please begin to fill out mandatory fields and upload any required documentation. Morris County staff will be working in conjunction with you to complete the application. If clarification or more information is required for any section(s) of the application, you will receive an email to review the feedback and attend to these requests.</p>
<p>Please contact me if you have any questions or concerns.<br>
 <br>
Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>', 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    3, 
    'CHANGE_STATUS_FROM_DOI_APPROVED_TO_SUBMITTED', 
    'Change status from Draft Application to Application Submitted', 
    'Morris County Flood Mitigation Program – Application Submitted', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>This email is to inform you that the {{ApplicationName}} application has been submitted and is under review by County staff. Some additional steps may be necessary to complete your submission. If so, you&rsquo;ll be notified by email.</p>
<p>As Morris County staff reviews the application, staff will prepare feedback comments for any section(s) of the application that require further clarification or attention. Once completed, an email will be sent. From there, you&rsquo;ll be able to log back into the Flood Mitigation Program Portal, review the feedback and attend to any additional requirements.</p>
<p>Please contact me if you have any questions or concerns.<br>
 <br>
Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood</p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    4, 
    'CHANGE_STATUS_FROM_SUBMITTED_TO_IN_REVIEW', 
    'Change status from Application Submitted to In Review', 
    'Morris County Flood Mitigation Program – Application In Review', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>This email is to inform you that the {{ApplicationName}} application is complete and will be under review by the Flood Mitigation Committee. Upon a determination from the committee, your application will be reviewed by the Morris County Board of County Commissioners. You will be notified via email of both determinations.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood</p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    5, 
    'CHANGE_STATUS_FROM_IN_REVIEW_TO_REJECTED', 
    'Change status from In Review to Reject', 
    'Morris County Flood Mitigation Program – Application Rejected', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>We are sorry to inform you that the Morris County Flood Mitigation Committee have denied the {{ApplicationName}} application for grant funding at this time.</p>
<p>If you wish to apply for funding from the Morris County Flood Mitigation Program for this area in the future, a new application that meets all current application requirements will need to be completed.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    6, 
    'CHANGE_STATUS_FROM_IN_REVIEW_TO_ACTIVE', 
    'Change status from In Review to Active', 
    'Morris County Flood Mitigation Program – Application Approved', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>We are pleased to inform you that the Morris County Flood Mitigation Committee and Board of County Commissioners has approved the {{ApplicationName}} application in the amount of {{TotalEncumbered}}.</p>
<p>Please begin the process of obtaining the property appraisals and all required documentation. You can log into the Flood Mitigation Program portal and upload each document as it is completed. Attached you will find copies of the resolutions, the Flood Mitigation Program&rsquo;s rules and regulations, and additional information to assist you in the remainder of this process.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    7, 
    'CHANGE_STATUS_FROM_ACTIVE_TO_WITHDRAWN', 
    'Project Area Withdrawn', 
    'Morris County Flood Mitigation Program - Project Area Withdrawn', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>As per our discussion, the {{ApplicationName}} Project Area has been withdrawn. Funds for the project area will be held until {{CurrentExpirationDate}}. After this date, the project area will be closed and funds cannot be released.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    8, 
    'CHANGE_STATUS_FROM_ACTIVE_TO_CLOSED', 
    'Project Area Closed', 
    'Morris County Flood Mitigation Program - Project Area Closed', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>This email is to inform you the {{ApplicationName}} Project Area has been closed. Funds for this project area can no longer be released.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

--Project area flow


--Feedback
INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    9, 
    'FEEDBACK_EMAIL', 
    'Feedback Sent', 
    'Morris County Flood Mitigation Program - Feedback', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>After careful review of the {{ApplicationName}} Project Area, there are items that require your attention prior to deeming the application complete. Please login to Morris County Flood Mitigation program portal and select the project area to view provided feedback.</p>
<p>Instructions:</p>
<p>The items in Feedback indicate the section(s) of the application that require your attention. Navigate to that section, and you&rsquo;ll find that the application has been re-enabled for your completion. Please make the necessary modifications, then Save Changes. Once you&rsquo;ve completed all the feedback requirements, click the RESUBMIT button. You&rsquo;ll be able to provide your own feedback or questions to us in the Resubmit dialog box that pops up.</p>
<p>Please contact me if you have any questions or concerns.</p>
<p>Sincerely,</p>
<p>{{ProgramAdmin}}<br>
Flood Mitigation Program Coordinator<br>
Morris County Office of Planning &amp; Preservation<br>
P.O. Box 900<br>
Morristown, NJ 07963-0900<br>
(973) 829-8120 (O)<br>
E-Mail: mdigiulio@co.morris.nj.us<br>
Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    10, 
    'FEEDBACK_RESPONSE_EMAIL', 
    'Feedback Complete', 
    'Morris County Flood Mitigation Program - Feedback Complete', 
    '<p>{{ContactName}} has completed the required feedback item(s) for the {{ApplicationName}} Project Area. Review the project area to ensure feedback was adequately completed.</p>
	<p>Sincerely,</p>
     <p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO


--Feedback


--Property
INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    11, 
    'CHANGE_PROPERTY_STATUS_FROM_PENDING_TO_APPROVED', 
    'Property Receives Final Approval', 
    'Morris County Flood Mitigation Program - Final Approval', 
    '<p>Dear {{PrimaryContactName}},</p>
    <p>We are pleased to inform you that the Morris County Flood Mitigation Committee and Board of County Commissioners have granted final approval for {{PropertyName}} in the {{ApplicationName}} Project Area in the amount of {{MCHardCostShare}}.</p>
    <p>Attached is the Grant Agreement for {{PropertyName}}. Please sign and seal two copies of the signature page and return to our office prior to the Board of County Commissioner&rsquo;s meeting on {{BCCDate}}.</p>
    <p>Morris County staff will review the property&rsquo;s documents to ensure all due diligence documents (environmental analysis, title search, survey and legal description, etc.) have been received and approved. If any documents are outstanding, you will receive an email requesting them be uploaded. Once all documents have been approved, you will receive an email informing you that you can schedule a closing date with the homeowner.</p>
    <p>Please contact me if you have any questions or concerns.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    12, 
    'CHANGE_PROPERTY_STATUS_FROM_APPROVED_TO_PRESERVED', 
    'Change Property Status from Approved to Preserved', 
    'Morris County Flood Mitigation Program - Property Preserved', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>{{PropertyName}} in the {{ApplicationName}} Project Area has been preserved. When a demolition date has been determined, please provide us the date.</p>
	<p>Please contact me if you have any questions or concerns.</p>
    <p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    13, 
    'PROPERTY_SCHEDULE_CLOSING', 
    'Municipality Can Schedule Closing', 
    'Morris County Flood Mitigation Program - Closing Date Confirmation', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>We have received all required documentation in order for our program to release funds for {{PropertyName}} in the {{ApplicationName}} Project Area. You may work to schedule a closing date with the homeowner. Please provide at least 7 days notice for the closing to ensure funds are transferred to the muncipality&rsquo;s account in time.</p>
	<p>Please contact me if you have any questions or concerns.</p>
    <p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO
--Property

--Tab wise
INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    14, 
    'SUBMIT_SOFTCOST', 
    'Soft Cost Reimbursement Confirmation', 
    'Morris County Flood Mitigation Program - Soft Cost Reimbursement Confirmation', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>This email is to inform you the soft cost reimbursement request for {{PropertyName}} in the {{ApplicationName}} Project Area has been submitted. Morris County staff will review and you will receive more information regarding the reimbursement via email.</p>
	<p>Please contact me if you have any questions or concerns.</p>
    <p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    15, 
    'APPROVE_SOFTCOST', 
    'Soft Cost Reimbursement Approval', 
    'Morris County Flood Mitigation Program - Soft Cost Reimbursement Approved', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>This email is to inform you the soft cost reimbursement request for {{PropertyName}} in the {{ApplicationName}} Project Area has been approved in the total of {{FMPSoftCostReimbursed}}. Funds will be released to your municipality&rsquo;s account within the next week.</p>
	<p>Please contact me if you have any questions or concerns.</p>
    <p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    16, 
    'REMINDER_ABOUT_EXPIRATION_OF_FIRST_GRANT_EXTENSION', 
    'First Project Area Extension Approval', 
    'Morris County Flood Mitigation Program - First Project Area Extension Approval', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>This email is to inform you that your request for an extension for the {{ApplicationName}} Project Area was approved. It will now expire on {{FirstProjectAreaExtensionDate}}. As a reminder, you have an additional six-month extension remaining. Please reach out within the next three months if you wish to request this second extension.</p>
    <p>Please contact me if you have any questions or concerns.</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    17, 
    'REMINDER_ABOUT_EXPIRATION_OF_SECOND_GRANT_EXTENSION', 
    'Second Project Area Extension Approval', 
    'Morris County Flood Mitigation Program - Second Project Area Extension Approval', 
    '<p>Dear {{PrimaryContactName}},</p>
	<p>This email is to inform you that your request for an extension for the {{ApplicationName}} Project Area was approved. It will now expire on {{SecondProjectAreaExtensionDate}}. After this date, the project area will not be eligible for another extension.</p>
	<p>As a reminder, you will be unable to receive reimbursement for any properties that were not yet preserved at the time of expiration. You will need to reapply with a new project area for these properties if you wish to pursue them again. If there are any outstanding soft cost reimbursement requests at the time of expiration, those can still be released for preserved properties.</p>
	<p>Please contact me if you have any questions or concerns.<br></p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    18, 
    'DUE_DILIGIENCE_DOCUMENTS', 
    'Due Diligience Documents Missing', 
    'Morris County Flood Mitigation Program - Due Diligience Documentation Requested', 
    '<p>Dear {{PrimaryContactName}},</p>
    <p>We are missing one of the following documents for {{PropertyName}} in the {{ApplicationName}} Project Area:</p>
	<p>1. Environmental Analysis<br>
    2. Title Search<br>
    3. Survey and Legal Description</p>
	<p>These documents must be uploaded before funds will be released for the closing. Please log into the Flood Mitigation Program and upload these documents. Once these are uploaded and Morris County staff reviews and approves them, you can then schedule a closing date for the property. Please provide at least 7 days notice for the closing to ensure funds are transferred to the muncipality&rsquo;s account in time.</p>
	<p>Please contact me if you have any questions or concerns.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     19, 
    'SOFTCOST_REIMBURSEMENT_INFORMATION', 
	'Soft Cost Reimbursement Information',
    'Morris County Flood Mitigation Program - Soft Cost Reimbursement Request', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>This is a reminder that you had requested for soft cost reimbursement for {{PropertyName}} in the {{ApplicationName}} Project Area. Morris County staff has confirmed the demolition of the property, and you are now able to request for soft cost reimbursement. Please log into the Flood Mitigation Program portal and fill out the Soft Cost Reimbursement tab for this property. The following documents are required for each of the reimbursable activities:</p>
	<p>1. Purchase Order<br>
	2. Invoice<br>
	3. Copy of check(s)</p>
	<p>Please contact me if you have any questions or concerns.<br>
	</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO


INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     20, 
    'FLAP_UPDATE', 
    'FLAP Update', 
    'Morris County Flood Mitigation Program - FLAP Update', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>This email is to inform you that your municipality&rsquo;s FLAP has been updated. Please contact me if you have any questions or concerns.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     21, 
    'PROJECT_AREA_EXPIRATION_REMINDER', 
    'Project Area Expiration Reminder', 
    'Morris County Flood Mitigation Program - Project Area Extension', 
    '<p>Dear {{PrimaryContactName}},</p>
     <p>This email is to remind you that the {{ApplicationName}} Project Area will expire on {{ProjectAreaExpirationDate}}. After expiration, you will be unable to receive reimbursement for any properties that were not yet preserved at the time of expiration. You will need to reapply with a new project area for these properties if you wish to pursue them again. If there are any outstanding soft cost reimbursement requests at the time of expiration, those can still be released for preserved properties.</p>
	<p>You may request for two separate six-month extensions. The second extension can only be requested once the first extension is reviewed and approved by the Flood Mitigation Committee. Please reach out with any questions or with your extension request.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     22, 
    'GRANT_EXPIRATION_REMINDER', 
    'Grant Expiration Reminder', 
    'Morris County Flood Mitigation Program - Grant Expiration Reminder', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>This is a reminder that funds for {{PropertyName}} in the {{ApplicationName}} Project Area have not been released. The grant will expire in 3 months on {{GrantExpirationDate}}. Please work to schedule a closing with the homeowner, and provide at least 7 days notice for the closing to ensure funds are transferred to the municipality&rsquo;s account in time. If a closing will not occur, let us know and the property will be withdrawn.</p>
	 <p>Please contact me if you have any questions or concerns.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     23, 
    'UPLOAD_CLOSING_DOCUMENTS_REMINDER', 
    'Upload Closing Documents', 
    'Morris County Flood Mitigation Program - Closing Documents Requested', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>This is a reminder to upload the following closing documents for {{PropertyName}} in the {{ApplicationName}} Project Area:</p>
	<p>1. Recorded Deed<br>
	   2. HUD-1 Form<br>
	   3. Title Insurance Policy</p>
	<p>If you requested soft cost reimbursement once demolition of the home occurs, these documents must be uploaded before funds will be released.</p>
	<p>Please contact me if you have any questions or concerns.<br></p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
     24, 
    'DEMOLITION_REMINDER', 
    'Demolition Reminder', 
    'Morris County Flood Mitigation Program - Demolition Reminder', 
    '<p>Dear {{PrimaryContactName}},</p>
	 <p>This is a reminder that a demolition date for {{PropertyName}} in the {{ApplicationName}} Project Area has not been scheduled. If the home has already been demolished, provide us the date of demoltion and proof of demolition. If it has not, provide us the anticipated date of demolition.</p>
	<p>Please contact me if you have any questions or concerns.</p>
	<p>Sincerely,</p>
	<p>{{ProgramAdmin}}<br>
	Flood Mitigation Program Coordinator<br>
	Morris County Office of Planning &amp; Preservation<br>
	P.O. Box 900<br>
	Morristown, NJ 07963-0900<br>
	(973) 829-8120 (O)<br>
	E-Mail: mdigiulio@co.morris.nj.us<br>
	Website: https://www.morriscountynj.gov/flood </p>' , 
    1);
GO


--Tab wise
