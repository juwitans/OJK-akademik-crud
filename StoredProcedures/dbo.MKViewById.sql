CREATE PROCEDURE [dbo].[MKViewById]
	@id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT id AS id, kode_mk AS kodeMatkul, nama_mk AS namaMatkul, sks AS sks
	FROM matakuliah
	WHERE id=@id

END