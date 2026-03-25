-- =========================================
-- SQL SCRIPT: Clean Database for Testing
-- =========================================
-- Run this in SQL Server Management Studio if you want to start fresh

USE WelfareLinkDBVCF;
GO

-- View current data (before cleaning)
PRINT '=== CURRENT DATA ===';
SELECT COUNT(*) AS ProgramCount FROM Programs;
SELECT COUNT(*) AS ResourceCount FROM Resources;

-- Option 1: View all current data
SELECT * FROM Programs;
SELECT * FROM Resources;

-- Option 2: Delete all data (if you want to start completely fresh)
-- Uncomment the lines below to delete all data:

/*
DELETE FROM Resources;  -- Delete resources first (foreign key)
DELETE FROM Programs;   -- Then delete programmes

-- Reset identity seeds (so IDs start from 1 again)
DBCC CHECKIDENT ('Programs', RESEED, 0);
DBCC CHECKIDENT ('Resources', RESEED, 0);

PRINT '=== DATABASE CLEANED ===';
SELECT COUNT(*) AS ProgramCount FROM Programs;
SELECT COUNT(*) AS ResourceCount FROM Resources;
*/

GO
