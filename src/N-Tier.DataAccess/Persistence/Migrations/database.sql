CREATE TABLE [Specializations] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_Specializations] PRIMARY KEY ([Id])
);

CREATE TABLE [Patients] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(MAX) NULL,
    [Surname] NVARCHAR(MAX) NULL,
    [Password] NVARCHAR(MAX) NULL,
    [Contact] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY ([Id])
);

CREATE TABLE [Doctors] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(MAX) NULL,
    [Surname] NVARCHAR(MAX) NULL,
    [Password] NVARCHAR(MAX) NULL,
    [SpecializationId] INT NOT NULL,
    [Contact] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Doctors_Specializations_SpecializationId] FOREIGN KEY ([SpecializationId]) REFERENCES [Specializations] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Orders] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [DoctorId] INT NOT NULL,
    [PatientId] INT NOT NULL,
    [DateOfAppointment] DATETIME2 NOT NULL,
    [Diagnosis] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [Doctors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Orders_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Doctors_SpecializationId] ON [Doctors] ([SpecializationId]);
CREATE INDEX [IX_Orders_DoctorId] ON [Orders] ([DoctorId]);
CREATE INDEX [IX_Orders_PatientId] ON [Orders] ([PatientId]);
