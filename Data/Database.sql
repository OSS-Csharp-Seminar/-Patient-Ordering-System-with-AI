-- Create Specialization table
CREATE TABLE Specialization (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

-- Create Doctor table with foreign key constraint to Specialization
CREATE TABLE Doctor (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Surname NVARCHAR(100) NOT NULL,
    SpecializationId INT NOT NULL,
    Email NVARCHAR(100),
    ContactNumber NVARCHAR(20),
    FOREIGN KEY (SpecializationId) REFERENCES Specialization(Id)
);

-- Create Patient table
CREATE TABLE Patient (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Surname NVARCHAR(100) NOT NULL,
    ContactNumber NVARCHAR(20),
    Email NVARCHAR(100)
);


CREATE TABLE Orders(
    DoctorId INT,
    PatientId INT,
    Diagnosis NVARCHAR(255),
    DateTime DATETIME,
    FOREIGN KEY (DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY (PatientId) REFERENCES Patient(Id)
);