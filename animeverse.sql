IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Series] (
    [SeriesID] int NOT NULL IDENTITY,
    [Title] varchar(255) NOT NULL,
    [Genre] varchar(50) NOT NULL,
    [Year] int NOT NULL,
    CONSTRAINT [PK_Series] PRIMARY KEY ([SeriesID])
);
GO

CREATE TABLE [Character] (
    [CharacterID] int NOT NULL,
    [Name] varchar(55) NOT NULL,
    [Age] int NOT NULL,
    [Gender] varchar(10) NOT NULL,
    [Description] text NOT NULL,
    [SeriesID] int NOT NULL,
    CONSTRAINT [PK_Character] PRIMARY KEY ([CharacterID]),
    CONSTRAINT [FK_Character_Series] FOREIGN KEY ([CharacterID]) REFERENCES [Series] ([SeriesID])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231129054122_Initial', N'7.0.14');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231201000140_Identity', N'7.0.14');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Series].[SeriesID]', N'ID', N'COLUMN';
GO

EXEC sp_rename N'[Character].[CharacterID]', N'ID', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231215022147_RenameID', N'7.0.14');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Series]') AND [c].[name] = N'genre');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Series] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Series] DROP COLUMN [genre];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Character]') AND [c].[name] = N'description');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Character] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Character] DROP COLUMN [description];
GO

ALTER TABLE [Character] ADD [Demographic] varchar(50) NOT NULL DEFAULT '';
GO

EXEC sp_rename N'[Character]', N'Characters';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231215044107_NewChanges', N'7.0.14');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Characters] DROP CONSTRAINT [FK_Character_Series];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Characters]') AND [c].[name] = N'Demographic');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Characters] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Characters] DROP COLUMN [Demographic];
GO

ALTER TABLE [Series] ADD [Demographic] nvarchar(max) NOT NULL DEFAULT N'';
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Characters]') AND [c].[name] = N'Age');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Characters] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Characters] ALTER COLUMN [Age] int NULL;
GO

CREATE INDEX [IX_Character_SeriesID] ON [Characters] ([SeriesID]);
GO

ALTER TABLE [Characters] ADD CONSTRAINT [FK_Characters_Series] FOREIGN KEY ([SeriesID]) REFERENCES [Series] ([ID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231215053606_Genres', N'7.0.14');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Genres] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([ID])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231215055413_Genre', N'7.0.14');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Series]') AND [c].[name] = N'Demographic');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Series] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Series] ALTER COLUMN [Demographic] varchar(255) NOT NULL;
GO

CREATE TABLE [SeriesGenres] (
    [SeriesId] int NOT NULL,
    [GenreId] int NOT NULL,
    CONSTRAINT [PK_SeriesGenres] PRIMARY KEY ([SeriesId], [GenreId]),
    CONSTRAINT [FK_SeriesGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_SeriesGenres_Series_SeriesId] FOREIGN KEY ([SeriesId]) REFERENCES [Series] ([ID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_SeriesGenres_GenreId] ON [SeriesGenres] ([GenreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231215063413_SeriesGenreRelationship', N'7.0.14');
GO

COMMIT;
GO

