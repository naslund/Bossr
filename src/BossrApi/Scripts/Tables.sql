CREATE TABLE [dbo].[Users] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Username]       NVARCHAR (30)  NOT NULL,
    [HashedPassword] NVARCHAR (100) NOT NULL,
    [Salt]           NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

CREATE TABLE [dbo].[Worlds] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (30) NOT NULL,
    [IsMonitored] BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

CREATE TABLE [dbo].[Creatures] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (30) NOT NULL,
    [SpawnRateHoursMin] INT           DEFAULT ((0)) NOT NULL,
    [SpawnRateHoursMax] INT           DEFAULT ((0)) NOT NULL,
    [IsMonitored]       BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

CREATE TABLE [dbo].[Scrapes] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [TimeMinUtc] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Spawns] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [WorldId]    INT NOT NULL,
    [CreatureId] INT NOT NULL,
    [ScrapeId]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Spawns_Scrapes] FOREIGN KEY ([ScrapeId]) REFERENCES [dbo].[Scrapes] ([Id]),
    CONSTRAINT [FK_Spawns_Worlds] FOREIGN KEY ([WorldId]) REFERENCES [dbo].[Worlds] ([Id]),
    CONSTRAINT [FK_Spawns_Creatures] FOREIGN KEY ([CreatureId]) REFERENCES [dbo].[Creatures] ([Id])
);