CREATE PROCEDURE [dbo].[DosenAddOrEdit]
	@id INT,
	@nip VARCHAR(12),
	@namaDosen VARCHAR(25)
AS
BEGIN
	SET NOCOUNT ON;

	IF @id=0
	BEGIN
		INSERT INTO dosen(nip,nama_dosen)
		VALUES (@nip,@namaDosen)
	END
	ELSE
	BEGIN
		UPDATE dosen
		SET
			nip=@nip,
			nama_dosen=@namaDosen
		WHERE Id = @id
	END
END