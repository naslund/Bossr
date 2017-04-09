﻿CREATE TABLE [dbo].[Users] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Username]       NVARCHAR (30)  NOT NULL,
    [HashedPassword] NVARCHAR (100) NOT NULL,
    [Salt]           NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

CREATE TABLE [dbo].[Worlds] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

CREATE TABLE [dbo].[Creatures] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

CREATE TABLE [dbo].[Spawns] (
    [Id]         INT                IDENTITY (1, 1) NOT NULL,
    [WorldId]    INT                NOT NULL,
    [CreatureId] INT                NOT NULL,
    [TimeMin]    DATETIMEOFFSET (7) NOT NULL,
    [TimeMax]    DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Spawns_Worlds] FOREIGN KEY ([WorldId]) REFERENCES [dbo].[Worlds] ([Id]),
    CONSTRAINT [FK_Spawns_Creatures] FOREIGN KEY ([CreatureId]) REFERENCES [dbo].[Creatures] ([Id])
);