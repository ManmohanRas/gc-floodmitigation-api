DELETE FROM [Flood].[FloodEmailTemplate];

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    1, 
    'CHANGE_STATUS_FROM_DOI_DRAFT_TO_DOI_SUBMITTED', 
    'Change status from DOI Draft to DOI Submitted', 
    'Morris County Flood Mitigation Program – Declaration Of Intent Submitted', 
    '<p>Dear {{PrimaryContactName}},</p>
<p>This email is to inform you that the Declaration of Intent for {{ApplicationName}} has been approved by County staff.</p>
<p>The draft application is now open. Please begin to fill out mandatory fields and upload any required documentation. Morris County staff will be working in conjunction with you to complete the application. If clarification or more information is required for any section(s) of the application, you will receive an email to review the feedback and attend to these requests.</p>
<p>Please contact me if you have any questions or concerns.<br /><br />Sincerely,</p>
<p>{{ProgramAdmin}}</p>' , 
    1);
GO