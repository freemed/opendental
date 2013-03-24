Deployment
----------
In Visual studio select the WebForms project, right click and select Publish. Use the published files as the website files.
To create the the db in mysql use the file db/sheetsdb.sql

In the website files change the following in Web.config 
---------------------------------------------------------
-The connectionString attibute of ODWebServiceEntities must be changed to refelect the database settings.
-The value attibute of the key LogFile must refelect the appropiate path for the log file.

Testing to see if the website is working
-----------------------------------------
Key in an address which looks similar to this: http://localhost/WebForms/Sheets.aspx?DentalOfficeID=6566&WebSheetDefID=1012
Assumption - the database has rows in both the webforms_sheetdef and webforms_sheetfielddef
The DentalOfficeID and WebSheetDefID parameter values for the url can be gleaned form the webforms_sheetdef  table.

Errors while installing
--------------------------
- A possible error seen while installing on Windows 2008 say: Could not load file or assembly 'CodeBase' or one of its dependencies. An attempt was made to load a program with an incorrect format. 
Fix for this error: Select the application pool ASP.NET v4.0 in IIS manager, click "Advanced settings", and change "Enable 32-Bit Applications" from False to True
