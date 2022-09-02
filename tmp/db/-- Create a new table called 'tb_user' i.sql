-- Create a new table called 'tb_user' in schema 'SchemaName'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.tb_User', 'U') IS NOT NULL
DROP TABLE SchemaName.tb_user
GO
-- Create the table in the specified schema
CREATE TABLE dbo.tb_User
(
    UserId bigint NOT NULL PRIMARY KEY IDENTITY(1,1), -- primary key column
    Username [NVARCHAR](50) NOT NULL,
    q_in_index INT NOT NULL DEFAULT(0)
    -- specify more columns here
);
GO