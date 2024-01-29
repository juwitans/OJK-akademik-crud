CREATE PROCEDURE [dbo].[MKViewAll]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT id AS id, kode_mk AS kodeMatkul, nama_mk AS namaMatkul, sks AS sks
	FROM matakuliah
END