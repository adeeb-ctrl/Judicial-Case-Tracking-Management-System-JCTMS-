-- JCTMS Database Schema
-- Compatible with SQL Server

-- 1. Sequences Table (For ID Generation Pattern)
CREATE TABLE Sequences (
    SequenceName NVARCHAR(50) PRIMARY KEY,
    CurrentValue INT NOT NULL
);

-- Initialize default sequence for Case Reference Numbers
INSERT INTO Sequences (SequenceName, CurrentValue) VALUES ('CaseRef', 0);

-- 2. Judges Table
CREATE TABLE Judges (
    JudgeID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    RoomNumber NVARCHAR(20) NULL
);

-- 3. Cases Table (Corresponds to Errands)
CREATE TABLE Cases (
    CaseID INT PRIMARY KEY IDENTITY(1,1),
    RefNumber NVARCHAR(50) NOT NULL UNIQUE, -- Format: YYYY-45-XXXX
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL, -- Observation
    OpenDate DATETIME NOT NULL DEFAULT GETDATE(),
    StatusID INT NOT NULL, -- Mapped to Status Enum
    JudgeID INT NULL,
    CourtRoom NVARCHAR(50) NULL,
    CONSTRAINT FK_Cases_Judges FOREIGN KEY (JudgeID) REFERENCES Judges(JudgeID)
);

-- 4. Parties Table (Plaintiffs and Defendants)
CREATE TABLE Parties (
    PartyID INT PRIMARY KEY IDENTITY(1,1),
    CaseID INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL, -- 'Plaintiff', 'Defendant'
    ContactInfo NVARCHAR(200) NULL,
    CONSTRAINT FK_Parties_Cases FOREIGN KEY (CaseID) REFERENCES Cases(CaseID) ON DELETE CASCADE
);

-- 5. Hearings Table
CREATE TABLE Hearings (
    HearingID INT PRIMARY KEY IDENTITY(1,1),
    CaseID INT NOT NULL,
    HearingDate DATETIME NOT NULL,
    Remarks NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Hearings_Cases FOREIGN KEY (CaseID) REFERENCES Cases(CaseID) ON DELETE CASCADE
);
