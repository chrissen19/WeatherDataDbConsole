USE Master
GO

CREATE DATABASE WeatherDataDb
GO

Use WeatherDataDb
GO

DROP TABLE [dbo].[WeatherData]
GO

CREATE TABLE [dbo].[WeatherData] (
[Id] INT        IDENTITY (1,1) NOT NULL,
[Datum] DATETIME NOT NULL,
[Plats] varchar (10) NOT NULL,
[Temperatur] DECIMAL (4,1) NOT NULL,
[Luftfuktighet] INT NOT NULL);
GO

BULK INSERT WeatherData
FROM '...\\DatabaseData\\TempFuktData.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)
GO

DROP PROCEDURE [dbo].[SP_WEATHER_GET_LIST]
GO

CREATE PROCEDURE [dbo].[SP_WEATHER_GET_LIST]  
AS  
   BEGIN  
   SELECT Id  
                  ,Datum  
                 ,Plats
                 ,Temperatur
                 ,Luftfuktighet
   FROM weatherdata  
END 
GO

USE master;  
GO
DECLARE @root nvarchar(100);  
DECLARE @fullpath nvarchar(1000);  
  
SELECT @root = FileTableRootPath();  
SELECT @fullpath = @root + file_stream.GetFileNamespacePath()  
    FROM filetable_name  
    WHERE name = N'TempFuktData.csv';  
  
PRINT @fullpath;  
GO  