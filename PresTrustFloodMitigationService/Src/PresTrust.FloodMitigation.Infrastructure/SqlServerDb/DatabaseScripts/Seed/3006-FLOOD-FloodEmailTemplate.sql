DELETE FROM [Flood].[FloodEmailTemplate];

INSERT INTO [Flood].[FloodEmailTemplate] ([Id] , [TemplateCode], [Title], [Subject], [Description], [IsActive]) VALUES  (
    1, 
    'CHANGE_STATUS_FROM_DOI_DRAFT_TO_DOI_SUBMITTED', 
    'Change status from DOI Draft to DOI Submitted', 
    'Morris County Flood Mitigation Program – Declaration Of Intent Submitted', 
    '<p>Dear {{PrimaryContactName}},</p><p>This email is to inform you that the Declaration of Intent for {{ApplicationName}} has been submitted and is under review by County staff. Some additional documents or steps may be necessary to complete your submission. If so, you&rsquo;ll be notified by email.</p>
<p>As Morris County staff reviews the Declaration of Intent, staff will prepare feedback comments for any section(s) that require further clarification or attention. Once approved, an email will be sent. From there, you&rsquo;ll be able to log back into the Flood Mitigation Program Program Portal and complete the application.</p>
<p>Please contact me if you have any questions or concerns.<br>
 <br>
Sincerely,</p>
<p>{{ProgramAdmin}}</p>
<p>Flood Mitigation Program Coordinator</p>
<p>Morris County Office of Planning &amp; Preservation</p>
<p>P.O. Box 900</p>
<p>Morristown, NJ 07963-0900</p>
<p>(973) 829-8120 (O)</p>
<p>E-Mail: mdigiulio@co.morris.nj.us</p>
<p>Website: https://www.morriscountynj.gov/flood</p>' , 
    1);
GO
