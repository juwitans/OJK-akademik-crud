CREATE PROCEDURE [dbo].[DosenViewById]
	@id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id AS id, nip AS nip, nama_dosen AS namaDosen
	FROM dosen
	WHERE Id = @id
END