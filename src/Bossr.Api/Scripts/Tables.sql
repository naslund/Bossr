GO
CREATE TABLE [dbo].[Users] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Username]       NVARCHAR (30)  NOT NULL,
    [HashedPassword] NVARCHAR (100) NOT NULL,
    [Salt]           NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

GO
CREATE TABLE [dbo].[Creatures] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (30) NOT NULL,
    [IsMonitored] BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

GO
CREATE TABLE [dbo].[Worlds] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (30) NOT NULL,
    [IsMonitored] BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

GO
CREATE TABLE [dbo].[Scrapes] (
    [Id]   INT  IDENTITY (1, 1) NOT NULL,
    [Date] DATE NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Date] ASC)
);

GO
CREATE TABLE [dbo].[Statistics] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [WorldId]    INT NOT NULL,
    [CreatureId] INT NOT NULL,
    [ScrapeId]   INT NOT NULL,
    [Amount]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Statistics_Creatures] FOREIGN KEY ([CreatureId]) REFERENCES [dbo].[Creatures] ([Id]),
    CONSTRAINT [FK_Statistics_Scrapes] FOREIGN KEY ([ScrapeId]) REFERENCES [dbo].[Scrapes] ([Id]),
    CONSTRAINT [FK_Statistics_Worlds] FOREIGN KEY ([WorldId]) REFERENCES [dbo].[Worlds] ([Id])
);

GO
CREATE TABLE [dbo].[Raids] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [FrequencyMin] INT           NOT NULL,
    [FrequencyMax] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE TABLE [dbo].[Spawns] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [CreatureId] INT NOT NULL,
    [RaidId]     INT NOT NULL,
    [Amount]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Spawns_Creatures] FOREIGN KEY ([CreatureId]) REFERENCES [dbo].[Creatures] ([Id]),
    CONSTRAINT [FK_Spawns_Raids] FOREIGN KEY ([RaidId]) REFERENCES [dbo].[Raids] ([Id])
);

GO
CREATE TABLE [dbo].[Positions] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (30) NOT NULL,
    [X]       INT           NOT NULL,
    [Y]       INT           NOT NULL,
    [Z]       INT           NOT NULL,
    [SpawnId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Positions_Spawns] FOREIGN KEY ([SpawnId]) REFERENCES [dbo].[Spawns] ([Id])
);

GO
CREATE TABLE [dbo].[Categories] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

GO
CREATE TABLE [dbo].[Tags] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (30) NOT NULL,
    [CategoryId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC, [CategoryId] ASC),
    CONSTRAINT [FK_Tags_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id])
);

GO
CREATE TABLE [dbo].[Scopes] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

GO
CREATE TABLE [dbo].[CreatureTags] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [CreatureId] INT NOT NULL,
    [TagId]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([CreatureId] ASC, [TagId] ASC),
    CONSTRAINT [FK_CreatureTags_Creatures] FOREIGN KEY ([CreatureId]) REFERENCES [dbo].[Creatures] ([Id]),
    CONSTRAINT [FK_CreatureTags_Tags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id])
);

GO
CREATE TABLE [dbo].[PositionTags] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [PositionId] INT NOT NULL,
    [TagId]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([PositionId] ASC, [TagId] ASC),
    CONSTRAINT [FK_PositionTags_Positions] FOREIGN KEY ([PositionId]) REFERENCES [dbo].[Positions] ([Id]),
    CONSTRAINT [FK_PositionTags_Tags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id])
);

GO
CREATE TABLE [dbo].[RaidTags] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [RaidId] INT NOT NULL,
    [TagId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([RaidId] ASC, [TagId] ASC),
    CONSTRAINT [FK_RaidTags_Raids] FOREIGN KEY ([RaidId]) REFERENCES [dbo].[Raids] ([Id]),
    CONSTRAINT [FK_RaidTags_Tags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id])
);

GO
CREATE TABLE [dbo].[WorldTags] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [WorldId] INT NOT NULL,
    [TagId]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([WorldId] ASC, [TagId] ASC),
    CONSTRAINT [FK_WorldTags_Worlds] FOREIGN KEY ([WorldId]) REFERENCES [dbo].[Worlds] ([Id]),
    CONSTRAINT [FK_WorldTags_Tags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id])
);

GO
CREATE TABLE [dbo].[UserScopes] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [UserId]  INT NOT NULL,
    [ScopeId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([UserId] ASC, [ScopeId] ASC),
    CONSTRAINT [FK_UserScopes_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_UserScopes_Scopes] FOREIGN KEY ([ScopeId]) REFERENCES [dbo].[Scopes] ([Id])
);