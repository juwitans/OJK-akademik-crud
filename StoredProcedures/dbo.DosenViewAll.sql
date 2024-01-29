CREATE PROCEDURE [dbo].[DosenViewAll]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id AS id, nip AS nip, nama_dosen AS namaDosen
	FROM dosen
END