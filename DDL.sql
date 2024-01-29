CREATE DATABASE akademikDB;

USE akademikDB;

CREATE TABLE [dbo].[dosen] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [nip]        VARCHAR (12) NOT NULL,
    [nama_dosen] VARCHAR (25) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([nip] ASC)
);

CREATE TABLE [dbo].[mahasiswa] (
    [id]            INT          IDENTITY (1, 1) NOT NULL,
    [nim]           VARCHAR (9)  NOT NULL,
    [nama_mhs]      VARCHAR (25) NOT NULL,
    [tgl_lahir]     DATE         NOT NULL,
    [alamat]        VARCHAR (50) NOT NULL,
    [jenis_kelamin] VARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([nim] ASC),
    CHECK ([jenis_kelamin]='Laki-Laki' OR [jenis_kelamin]='Perempuan')
);

CREATE TABLE [dbo].[matakuliah] (
    [id]      INT          IDENTITY (1, 1) NOT NULL,
    [kode_mk] VARCHAR (6)  NOT NULL,
    [nama_mk] VARCHAR (20) NOT NULL,
    [sks]     INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([kode_mk] ASC)
);

CREATE TABLE [dbo].[perkuliahan]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[nim]     VARCHAR (9)  NOT NULL,
    [kode_mk] VARCHAR (6)  NOT NULL,
    [nip]     VARCHAR (12) NOT NULL,
    [nilai]   CHAR (10)    NOT NULL,
    FOREIGN KEY ([nim]) REFERENCES [dbo].[mahasiswa] ([nim]),
    FOREIGN KEY ([kode_mk]) REFERENCES [dbo].[matakuliah] ([kode_mk]),
    FOREIGN KEY ([nip]) REFERENCES [dbo].[dosen] ([nip])
)